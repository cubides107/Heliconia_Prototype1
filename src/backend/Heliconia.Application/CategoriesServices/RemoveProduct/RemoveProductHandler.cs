using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.RemoveProduct
{
    public class RemoveProductHandler : IRequestHandler<RemoveProductCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public RemoveProductHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            Product product;
            EstadoEnum state;

            //Comprobar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso de todos los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Comprobar si el producto existe y obtenerlo
            if (repository.Exists<Product>(x => x.Id.ToString() == request.Id) is false)
                throw new Exception("El producto no existe");

            //Obtener producto con los estados asociados
            product = await repository.GetNested<Product>(x => x.Id.ToString() == request.Id, nameof(Product.Estados));

            //Ordernar y obtener el ultimo estado, luego comparar si el estado pertenece a una compra
            state = product.Estados.OrderByDescending(x => x.Fecha).First().EstadoEnum;

            if (state == EstadoEnum.vendido || state == EstadoEnum.devueltoPorComprador)
                throw new Exception("No se puede eliminar el producto esta asociados a una compra");

            //Cambiar el estado del producto a removido y los estados del producto y actualizar entidad
            product.Estados.ForEach(x => x.GoToDeletedState());
            product.GoToDeletedState();

            repository.Update<Product>(product);
            await repository.Commit();

            return 0;
        }
    }
}
