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

namespace Heliconia.Application.CategoriesServices.GetAllProducts
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, List<GetAllProductsDTO>>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        private readonly IMapObject mapObject;

        public GetAllProductsHandler(IRepository repository, IUtility utility, ISecurity security, IMapObject mapObject)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
            this.mapObject = mapObject;
        }

        public async Task<List<GetAllProductsDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            List<Product> products;

            //Comprobar que la peticion no este nula y acceso de los usuarios
            Guard.Against.Null(request, nameof(request));

            //Obtener lista de estados del enumerado y Comprobar si el filtro de enumerado es un EstadoEnum
            List<string> listEstates = Enum.GetNames(typeof(EstadoEnum)).ToList();
            if (listEstates.Contains(request.FilterState) is false)
                throw new Exception("El filtro de Estado no es valido");

            //verificamos el acceso a todos lo usuarios
            await Access.CheckAccessToAll(request.Claims, repository, security, utility);

            //obtener los productos y estados por el filtro de precio
            products = await repository.GetAll<Product>(
                page: request.Page,
                pageSize: request.PageSize,
                condition: x => x.Price >= request.StartPrice && x.Price <= request.EndPrice);

            //Eliminar todos los productos donde el ultimo estado sea diferente al del request
            products.RemoveAll(x => x.NameLastState != request.FilterState);
            
            return mapObject.Map<List<Product>, List<GetAllProductsDTO>>(products);
        }
    }
}
