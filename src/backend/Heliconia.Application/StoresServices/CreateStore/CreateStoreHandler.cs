using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.StoresServices.CreateStore
{
    public class CreateStoreHandler : IRequestHandler<CreateStoreCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public CreateStoreHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(CreateStoreCommand request, CancellationToken cancellationToken)
        {
            Store store;

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //verificamos acceso
            if(Access.IsUserType<HeliconiaUser>(request.Claims, security) is true)
                await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);
            else if(Access.IsUserType<Manager>(request.Claims, security) is true)
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);

            //verificar si existe la compañia
            if (repository.Exists<Company>(x => x.Id.ToString() == request.CompanyId) is false)
                throw new Exception("Compañia no existe");

            //crear y guardar una tienda
            store = Store.Build(name: request.Name, descripcion: request.Descripcion,
                companyId: Guid.Parse(request.CompanyId));

            await repository.Save<Store>(store);
            await repository.Commit();

            return 0;
        }
    }
}
