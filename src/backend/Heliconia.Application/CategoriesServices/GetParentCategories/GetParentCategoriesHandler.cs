using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.GetParentCategories
{
    public class GetParentCategoriesHandler : IRequestHandler<GetParentCategoriesQuery, List<GetParentCategoriesDTO>>
    {

        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        private readonly IMapObject mapObject;

        private const string CategoryFilterNameDefault = "DEFAULT";

        public GetParentCategoriesHandler(IRepository repository, ISecurity security, IUtility utility, IMapObject mapObject)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
            this.mapObject = mapObject;
        }

        public async Task<List<GetParentCategoriesDTO>> Handle(GetParentCategoriesQuery request, CancellationToken cancellationToken)
        {
            if (request.FilterName.Length <= 3)
                throw new Exception("El nombre debe tener minimo 4 caracteres");

            List<Category> parentCategories;

            //Verificar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso de los usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //Comprobar si la peticion es por default o no, obtener la lista segun corresponda
            if (request.FilterName.Equals(CategoryFilterNameDefault, StringComparison.OrdinalIgnoreCase))
                parentCategories = await repository.GetAll<Category>(x => x.Name, request.Page, request.PageSize, x => x.IsMain == true);
            else
                parentCategories = await repository.GetAll<Category>(x => x.Name, request.Page, request.PageSize, 
                    x => x.Name.Contains(request.FilterName), x => x.IsMain == true);

            //Retornar DTO
            return mapObject.Map<List<Category>, List<GetParentCategoriesDTO>>(parentCategories);

        }
    }
}
