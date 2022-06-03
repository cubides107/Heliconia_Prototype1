using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.AddChildCategory
{
    public class AddChildCategoryHandler : IRequestHandler<AddChildCategoryCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public AddChildCategoryHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
        {
            Category parentCategory;
            Category childCategory;

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //verificar acceso de cualquir usuario
            await Access.CheckAccessToAll(request.Claims, this.repository, this.security, this.utility);

            //Verificar que exista la categoria padre
            if (this.repository.Exists<Category>(x => x.Id.ToString() == request.ParentCategoryId) is false)
                throw new Exception("La categoria padre no existe");

            //obtenemos la categoria padre, se crea la subcategoria y se agrega a la categoria padre
            parentCategory = await this.repository.Get<Category>(x => x.Id.ToString() == request.ParentCategoryId);
            childCategory = Category.Build(
                name: request.Name,
                storeId: parentCategory.StoreId,
                isMain: false);

            parentCategory.AddElement(childCategory);

            await repository.Save<Category>(childCategory);

            //actualizamos y retornamos
            this.repository.Update(parentCategory);
            await this.repository.Commit();
            return 0;
        }
    }
}
