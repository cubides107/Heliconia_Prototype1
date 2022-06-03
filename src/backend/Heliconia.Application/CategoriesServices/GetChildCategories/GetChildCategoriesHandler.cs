using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.GetChildCategories
{
    public class GetChildCategoriesHandler : IRequestHandler<GetChildCategoriesQuery, GetChildCategoriesDTO>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        private readonly IMapObject mapObject;

        public GetChildCategoriesHandler(IRepository repository, ISecurity security, IUtility utility, IMapObject mapObject)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
            this.mapObject = mapObject;
        }

        public async Task<GetChildCategoriesDTO> Handle(GetChildCategoriesQuery request, CancellationToken cancellationToken)
        {
            Category parentCategory;
            GetChildCategoriesDTO childCategory = new();

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //verificar acceso de cualquier usuario
            await Access.CheckAccessToAll(request.Claims, this.repository, this.security, this.utility);

            //Verificar que la categoria padre exista 
            if (repository.Exists<Category>(x => x.Id.ToString() == request.ParentCategoryId) is false)
                throw new Exception("La categoria padre no existe");

            //Obtener el arbol, mapear al DTO y retornar
            parentCategory = await BuildCategoriesTree(request.ParentCategoryId, null);

            childCategory.Category = mapObject.Map<Category, GetChildCategoriesDTO.CategoryDTO>(parentCategory);

            return childCategory;

        }

        /// <summary>
        /// Obtiene el arbol de un nodo padre
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="parentCategory"></param>
        /// <returns></returns>
        private async Task<Category> BuildCategoriesTree(string parentId, Category parentCategory)
        {
            IEnumerable<Category> childs;

            if (parentCategory == null)
                parentCategory = await repository.Get<Category>(x => x.Id.ToString() == parentId);

            //Obtener hijos de la categoria padre y añadirlos a la misma
            childs = await repository.GetAll<Category>(x => x.CategoryId == parentCategory.Id);

            parentCategory.AddElements(childs);

            //Por cada hijo realizar recursividad
            foreach (var hijo in childs)
            {
                await BuildCategoriesTree(hijo.Id.ToString(), hijo);
            }

            return parentCategory;
        }

    }
}
