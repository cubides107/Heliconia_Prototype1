using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.PurchasesEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.PurchasesServices.GeneratePurchaseReceipt
{
    internal class GeneratePurchaseReceiptHandler : IRequestHandler<GeneratePurchaseReceiptQuery, GeneratePurchaseReceiptDTO>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        private readonly IMapObject mapObject;

        public GeneratePurchaseReceiptHandler(IRepository repository, ISecurity security, IUtility utility, IMapObject mapObject)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
            this.mapObject = mapObject;
        }

        public async Task<GeneratePurchaseReceiptDTO> Handle(GeneratePurchaseReceiptQuery request, CancellationToken cancellationToken)
        {
            Customer customer;
            Purchase purchase;
            GeneratePurchaseReceiptDTO purchaseReceiptDTO = new();
            purchaseReceiptDTO.purchaseReceiptDTO = new();

            //Se verifica que el request no este nulo 
            Guard.Against.Null(request, nameof(request));

            //Verificar el acceso de los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Verificar si el comprado existe en la bd, si existe obtenerlo
            if (repository.Exists<Customer>(x => x.Id.ToString() == request.CustomerId) is false)
                throw new Exception("El comprador no existe");

            customer = await repository.Get<Customer>(x => x.Id.ToString() == request.CustomerId);

            //Obtener la ultima compra del cliente con sus productos, mapear entidades al DTO y retornar
            purchase = await repository.GetLastNested<Purchase>(x => x.DatePurchase, x => x.CustomerId == customer.Id, nameof(Purchase.Products));

            purchaseReceiptDTO.purchaseReceiptDTO.customer = mapObject.Map<Customer, GeneratePurchaseReceiptDTO.CustomerDTO>(customer);
            purchaseReceiptDTO.purchaseReceiptDTO.purchase = mapObject.Map<Purchase, GeneratePurchaseReceiptDTO.PurchaseDTO>(purchase);

            return purchaseReceiptDTO;

        }
    }
}
