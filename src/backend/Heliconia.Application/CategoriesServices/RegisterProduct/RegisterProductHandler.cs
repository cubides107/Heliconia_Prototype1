using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.RegisterProduct
{
    public class RegisterProductHandler : IRequestHandler<RegisterProductCommand, string>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public RegisterProductHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<string> Handle(RegisterProductCommand request, CancellationToken cancellationToken)
        {
            Category category;
            Product product;

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso de todos los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Verificar si la categoria existe y obtenerla
            if (repository.Exists<Category>(x => x.Id.ToString() == request.CategoryId) is false)
                throw new Exception("La categoria no existe");

            category = await repository.Get<Category>(x => x.Id.ToString() == request.CategoryId);

            //Crear producto y agregarlo a la categoria, luego aumentar la cantidad total de productos  a la categoria
            product = Product.Build(request.Product.Price, Guid.Parse(request.CategoryId));
            category.AddElement(product);
            category.IncrementTotalProducts();

            //Cambiar el ultimo estado del producto
            product.ChangeLastNameState(EstadoEnum.registrado.ToString());

            //Guardar producto en la db y actualizar la categoria
            await repository.Save<Product>(product);
            repository.Update<Category>(category);
            await repository.Commit();

            //Retornar codigo de barras del producto
            return product.CodigoBarras;
        }
    }
}
