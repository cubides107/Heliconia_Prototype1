using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.ModifyCategory
{
    public class ModifyCategoryHandler : IRequestHandler<ModifyCategoryCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public ModifyCategoryHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(ModifyCategoryCommand request, CancellationToken cancellationToken)
        {
            Category category;

            //Comprobar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso de todos los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Comprobar si la categoria existe y obtenerla
            if (repository.Exists<Category>(x => x.Id.ToString() == request.CategoryDataRequest.Id) is false)
                throw new Exception("La categoria no existe");

            category = await repository.Get<Category>(x => x.Id.ToString() == request.CategoryDataRequest.Id);

            //Cambiar atributos principales y actualizar entidad
            category.ChangeMainAttributes(request.CategoryDataRequest.Name);
            repository.Update<Category>(category);
            await repository.Commit();

            return 0;
        }

    }
}
