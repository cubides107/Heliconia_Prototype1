using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.RemoveCategory
{
    public class RemoveCategoryHandler : IRequestHandler<RemoveCategoryCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public RemoveCategoryHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category;

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso de todos los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Verificar si la categoria existe
            if (repository.Exists<Category>(x => x.Id.ToString() == request.Id) is false)
                throw new Exception("La categoria no existe");

            //Obtener la categoria y cambiar todo el arbol de la categoria a removido
            category = await repository.Get<Category>(x => x.Id.ToString() == request.Id);
            await RemoveTreeCategories(category);
            category.GoToDeletedState();

            //Actualizar la entidad en la bd
            repository.Update<Category>(category);
            await repository.Commit();

            return 0;
        }

        private async Task RemoveTreeCategories(Category parentCategory)
        {
            List<Category> childsCategory;
            List<Product> productsChilds;

            //Obtener hijos de la categoria y productos de la categoria
            childsCategory = await repository.GetAll<Category>(x => x.CategoryId == parentCategory.Id);

            productsChilds = await repository.GetAllNested<Product>(x => x.CategoryElementId.ToString() == parentCategory.Id.ToString(), nameof(Product.Estados));

            //cambiar estado para categoria y para los productos
            childsCategory.ForEach(x => { x.GoToDeletedState(); });

            productsChilds.ForEach(x =>
            {
                EstadoEnum state = x.Estados.OrderByDescending(x => x.Fecha).First().EstadoEnum;

                if (state == EstadoEnum.vendido || state == EstadoEnum.devueltoPorComprador)
                    throw new Exception("No se puede eliminar la categoria tiene productos asociados a compras");
                x.GoToDeletedState();
            });

            //Realizar recursividad para cada categoria
            foreach (var category in childsCategory)
            {
               await RemoveTreeCategories(category);
            }

        }
    }
}
