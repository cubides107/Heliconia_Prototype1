using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CompaniesServices.ModifyBasicAttributes
{
    public class ModifyBasicAttributesHandler : IRequestHandler<ModifyBasicAttributesCommand, int>
    {

        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        public ModifyBasicAttributesHandler(IRepository repository, IUtility utility, ISecurity security)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
        }

        public async Task<int> Handle(ModifyBasicAttributesCommand request, CancellationToken cancellationToken)
        {
            Company company;

            //Verifiar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso del usuario Heliconia y Manager
            if (Access.IsUserType<HeliconiaUser>(request.Claims, security))
                await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);
            else if(Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);

            //Comprobar que la compañia exite, si existe obtenerla
            if (repository.Exists<Company>(x => x.Id.ToString() == request.CompanyDataRequest.Id) is false)
                throw new Exception("La compañia no existe");

            company = await repository.Get<Company>(x => x.Id.ToString() == request.CompanyDataRequest.Id);

            //Cambiar atributos basicos y actualizar compañia en bd
            company.ChangeMainAttributes(request.CompanyDataRequest.Name, request.CompanyDataRequest.Descripcion);

            repository.Update<Company>(company);
            await repository.Commit();
            
            return 0;
        }
    }
}
