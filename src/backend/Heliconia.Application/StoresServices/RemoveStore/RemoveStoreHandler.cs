using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.StoresServices.RemoveStore
{
    public class RemoveStoreHandler : IRequestHandler<RemoveStoreCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public RemoveStoreHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(RemoveStoreCommand request, CancellationToken cancellationToken)
        {
            Store store;
            List<Worker> listWorkers;
            List<Category> listCategories;

            //Comprobar que la peticion no se encuentre nula
            Guard.Against.Null(request, nameof(request));

            //Verificar que la peticion la realice un heliconia user o manager
            if (Access.IsUserType<HeliconiaUser>(request.Claims, security))
                await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);
            else if (Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);

            //Verificar si existe la tienda 
            if (repository.Exists<Store>(x => x.Id.ToString() == request.Id) is false)
                throw new Exception("La tienda a eliminar no esta registrada");

            //Obtener todos los usuario registrados en la tienda y cambiar el estado a removido de los mismos
            listWorkers = await repository.GetAll<Worker>(x => x.StoreId.ToString() == request.Id);
            listWorkers.ForEach(x => x.GoToDeletedState());

            //Obtener todas las categorias de la tienda y cambiar a estado removidas
            listCategories = await repository.GetAll<Category>(x => x.StoreId.ToString() == request.Id);
            listCategories.ForEach(x => x.GoToDeletedState());

            //Obtener la tienda, cambiar el estado a removida y actualizar en bd
            store = await repository.Get<Store>(x => x.Id.ToString() == request.Id);
            store.GoToDeletedState();

            repository.Update(store);
            await this.repository.Commit();
            return 0;
        }
    }
}
