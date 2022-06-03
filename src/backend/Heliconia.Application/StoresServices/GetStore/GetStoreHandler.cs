
using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.StoresServices.GetStore
{

    public class GetStoreHandler : IRequestHandler<GetStoreQuery, GetStoreDTO>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        private readonly IMapObject mapObject;

        public GetStoreHandler(IRepository repository, IUtility utility, ISecurity security, IMapObject mapObjet)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
            this.mapObject = mapObjet;
        }

        public async Task<GetStoreDTO> Handle(GetStoreQuery request, CancellationToken cancellationToken)
        {
            Worker worker;
            Store store;
            string idWorker;

            //Verificar que la peticion no se encuentre nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso del usuario Worker y se obtiene de la bd
            if (Access.IsUserType<Worker>(request.Claims, security))
                await Access.VerifyAccess<Worker>(request.Claims, repository, security, utility);

            idWorker = security.GetClaim(request.Claims, ISecurity.USERID);

            worker = await repository.Get<Worker>(x => x.Id.ToString() == idWorker);

            //Comprobar que la tienda a obtener exista en la db
            if(repository.Exists<Store>(x => x.Id.ToString() == worker.StoreId.ToString()) is false)
                throw new Exception ("La tienda a obtener no existe en la db");

            //Obtener tienda del usuario Worker, mapear  la tienda y retornar DTO  
            store = await repository.Get<Store>(x => x.Id.ToString() == worker.StoreId.ToString());
            
            return mapObject.Map<Store, GetStoreDTO>(store);
        }
    }
}
