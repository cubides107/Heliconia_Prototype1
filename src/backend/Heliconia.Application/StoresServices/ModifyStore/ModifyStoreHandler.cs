using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.StoresServices.ModifyStore
{
    public class ModifyStoreHandler : IRequestHandler<ModifyStoreCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public ModifyStoreHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(ModifyStoreCommand request, CancellationToken cancellationToken)
        {
            Store store;

            //Comprobar que la peticion no se encuentre nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso de los usaurio que realizan la peticion
            if (Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);
            else if (Access.IsUserType<HeliconiaUser>(request.Claims, security))
                await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);

            //Verificar si existe la tienda en la bd, si existe obtenerla
            if (repository.Exists<Store>(x => x.Id.ToString() == request.StoreRequest.Id.ToString()) is false)
                throw new Exception("La tienda a modificar no se encuentra registrada");

            store = await repository.Get<Store>(x => x.Id.ToString() == request.StoreRequest.Id.ToString());

            //Cambiar atributos de la tienda y actualizar en db
            store.ChangeMainAttributes(
                name: request.StoreRequest.Name,
                descripcion: request.StoreRequest.Description);

            repository.Update<Store>(store);
            await this.repository.Commit();

            return 0;
        }
    }
}
