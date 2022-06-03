using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.RegisterManager
{
    public class RegisterManagerHandler : IRequestHandler<RegisterManagerCommand, int>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;


        public RegisterManagerHandler(IRepository repository, IUtility utility, ISecurity security)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
        }

        public async Task<int> Handle(RegisterManagerCommand request, CancellationToken cancellationToken)
        {
            Manager managerUser;

            //Verificar request
            Guard.Against.Null(request, nameof(request));

            //Verificar que la peticion la realice un heliconia user o manager
            if (Access.IsUserType<HeliconiaUser>(request.UserClaims, security))
                await Access.VerifyAccess<HeliconiaUser>(request.UserClaims, repository, security, utility);
            else if (Access.IsUserType<Manager>(request.UserClaims, security))
                await Access.VerifyAccess<Manager>(request.UserClaims, repository, security, utility);

            //Verificar que el usuario a registrar no este registrado en la db y que la compañia exista
            if (this.repository.Exists<Manager>(x => x.Mail == request.Mail))
                throw new Exception("El usuario ya esta registrado");
            else if (!this.repository.Exists<Company>(x => x.Id.ToString() == request.CompanyId))
                throw new Exception("La compañia no se encuentra registrada");

            //Crear usuario Manager
            managerUser = Manager.Build(
                companyId: utility.CreateId(request.CompanyId),
                name: request.Name,
                lasname: request.LastName,
                identificationDocument: request.IdentificationDocument,
                mail: request.Mail,
                encryptedPassword: security.EncryptPassword(request.Password),
                cellPhoneNumber: request.CellPhoneNumber);

            //Guardar usuario en la db y retornar
            await this.repository.Save<Manager>(managerUser);
            await repository.Commit();

            return 0;
        }
    }
}
