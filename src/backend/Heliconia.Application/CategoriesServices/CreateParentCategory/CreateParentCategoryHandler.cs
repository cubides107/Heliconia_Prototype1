using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.CreateParentCategory
{
    public class CreateParentCategoryHandler : IRequestHandler<CreateParentCategoryCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public CreateParentCategoryHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(CreateParentCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category;

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //verificar acceso de cualquir usuario
            await Access.CheckAccessToAll(request.Claims, this.repository, this.security, this.utility);

            //verificar que no exista la categoria en la tienda
            if (this.repository.Exists<Category>(x => x.Name.ToUpper() == request.Name.ToUpper(),
                x => x.IsMain == true, x => x.StoreId.ToString() == request.StoreId.ToString()) is true)
                throw new Exception("La categoria ya existe con ese mismo nombre");

            //crear y guardar la categoria 
            category = Category.Build(
                request.Name,
                Guid.Parse(request.StoreId),
                true);

            await this.repository.Save(category);
            await this.repository.Commit();

            return 0;
        }
    }
}
