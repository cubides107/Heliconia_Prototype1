using Ardalis.GuardClauses;
using Heliconia.Application.AccountingServices.ManageLedger;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using Heliconia.Domain.PurchasesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.PurchasesServices.RegisterPurchase
{
    public class RegisterPurchaseHandler : IRequestHandler<RegisterPurchaseCommand, int>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        private readonly IMediator mediator;

        private Purchase purchase;

        private double totalPurchasePrice;

        public RegisterPurchaseHandler(IRepository repository, IUtility utility, ISecurity security, IMediator mediator)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
            this.mediator = mediator;

            this.purchase = null;
            this.totalPurchasePrice = 0;
        }

        public async Task<int> Handle(RegisterPurchaseCommand request, CancellationToken cancellationToken)
        {
            //Comprobar que la peticion no se encuentre nula 
            Guard.Against.Null(request, nameof(request));

            //Comprobar que el comprador se encuentre registrado
            if (repository.Exists<Customer>(x => x.Id.ToString() == request.Purchase.CustomerId) is false)
                throw new Exception("El comprador no esta registrado");

            //verificar acceso de Usuarios
            if (Access.IsUserType<Worker>(request.Claims, security))
                await Access.VerifyAccess<Worker>(request.Claims, repository, security, utility);
            else if (Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);

            //Crear Compra
            purchase = Purchase.Build(
                       datePurchase: request.Purchase.DatePurchase,
                       customerId: Guid.Parse(request.Purchase.CustomerId));

            //procesar productos
            await this.ProcessProduct(request);

            //actualizamos el precio total y el total de unidades de la compra
            purchase.changeMainAttributes(totalPurchasePrice, purchase.Products.Count);

            //cambiar estado a insertado de la compra y comitiars
            await repository.Save<Purchase>(purchase);

            //Crear comando para actualizar o crear libro mayor diario y enviarlo mediante el mediator
            ManageLedgerCommand commad = new()
            {
                Date = purchase.DatePurchase.Date,
                TotalPurchasePrice = totalPurchasePrice,
                TotalProductsPurchase = purchase.Products.Count,
                Vendor = new()
                {
                    Id = security.GetClaim(request.Claims, ISecurity.USERID),
                    Role = security.GetClaim(request.Claims, ISecurity.ROLE),
                },
            };

            await mediator.Send(commad);

            await repository.Commit();
            return 0;
        }

        /// <summary>
        /// obtenemos los productos, le cambiamos el estado a vendido y los agregamos a la compra
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task ProcessProduct(RegisterPurchaseCommand request)
        {
            Product product;
            Estado lastState;

            foreach (var productId in request.Purchase.ProductsId)
            {
                //Verificar que el producto este registrado en bd y obtenerlo
                if (!repository.Exists<Product>(x => x.Id.ToString() == productId))
                    throw new Exception("El producto no se encuentra registrado");

                //Obtener la compra con el listado de estados
                product = await repository.GetNested<Product>(x => x.Id.ToString() == productId, nameof(Product.Estados));

                //obtenemos el ultimo estado
                lastState = product.Estados.OrderByDescending(x => x.Fecha).FirstOrDefault();

                //Comprobar que el ultimo estado del producto sea registrado o devuelto
                if (!(lastState.EstadoEnum == EstadoEnum.registrado || lastState.EstadoEnum == EstadoEnum.devueltoPorComprador))
                    throw new Exception("El producto no se encuentra en un estado optimo para la venta");

                //Agregar un estado al producto de vendido y agregar el producto a la compra
                product.AddEstado(request.Purchase.DatePurchase, EstadoEnum.vendido);
                purchase.AddProduct(product);

                //Actualizar el ultimo estado del producto
                product.ChangeLastNameState(EstadoEnum.vendido.ToString());

                //Actualizar le producto y guardar el estado agregado al producto
                repository.Update<Product>(product);
                await repository.Save<Estado>(product.Estados.Last());

                //calcular el precio total de la compra
                totalPurchasePrice += product.Price;
            }
        }

    }
}
