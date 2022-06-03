using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.StoresServices.GetAllStores
{
    public class GetAllStoresHandler : IRequestHandler<GetAllStoresQuery, List<GetAllStoresDTO>>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        private readonly IMapObject mapObject;

        public GetAllStoresHandler(IRepository repository, IUtility utility, ISecurity security, IMapObject mapObjet)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
            this.mapObject = mapObjet;
        }

        public async Task<List<GetAllStoresDTO>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
        {
            Manager manager;
            List<Store> listStores;

            //Verificar que la peticion no se encuentre nula y el acceso del usuario Manager
            Guard.Against.Null(request, nameof(request));

            if (Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);

            //Obtener el usuario manager mediante el id obtenido de los Claims
            manager = await repository.Get<Manager>(x => x.Id.ToString() == security.GetClaim(request.Claims, ISecurity.USERID));

            //Verificar que la compañia exista en la bd
            if (repository.Exists<Company>(x => x.Id.ToString() == manager.CompanyId.ToString()) is false)
                throw new Exception("La compañia no existe en la bd");

            //Obtener el listado de tiendas pertenecientes a la compañia del usuario manager
            listStores = await repository.GetAll<Store>(x => x.Name, request.page, request.pageSize,
                x => x.CompanyId.ToString() == manager.CompanyId.ToString());

            //Maper lista obtenida y retornar DTO
            return mapObject.Map<List<Store>, List<GetAllStoresDTO>>(listStores);

        }
    }
}
