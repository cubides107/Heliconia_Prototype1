using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.ModifyProduct
{
    public class ModifyProductHandler : IRequestHandler<ModifyProductCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public ModifyProductHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(ModifyProductCommand request, CancellationToken cancellationToken)
        {
            Product product;

            //Comprobar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso de todos los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Comprobar si el producto existe y obtenerlo
            if (repository.Exists<Product>(x => x.Id.ToString() == request.ProductDataRequest.Id) is false)
                throw new Exception("El producto no existe");

            product = await repository.Get<Product>(x => x.Id.ToString() == request.ProductDataRequest.Id);

            //Cambiar atributos principales y actualizar entidad
            product.ChangeMainAttributes(request.ProductDataRequest.Price);
            repository.Update<Product>(product);

            await repository.Commit();

            return 0;
        }
    }
}
