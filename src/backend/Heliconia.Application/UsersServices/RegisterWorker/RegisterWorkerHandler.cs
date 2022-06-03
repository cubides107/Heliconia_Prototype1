using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.RegisterWorker
{
    public class RegisterWorkerHandler : IRequestHandler<RegisterWorkerCommand, int>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        public RegisterWorkerHandler(IRepository repository, IUtility utility, ISecurity security)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
        }

        public async Task<int> Handle(RegisterWorkerCommand request, CancellationToken cancellationToken)
        {
            Worker worker;

            //Verificar la petición
            Guard.Against.Null(request, nameof(request));

            //Verificar que la peticion la realice un heliconia user o manager
            if (Access.IsUserType<HeliconiaUser>(request.UserClaims, security))
                await Access.VerifyAccess<HeliconiaUser>(request.UserClaims, repository, security, utility);
            else if (Access.IsUserType<Manager>(request.UserClaims, security))
                await Access.VerifyAccess<Manager>(request.UserClaims, repository, security, utility);

            //Verificar que el usuario worker a registrar no este registrado en la db y que la tienda exista
            if (this.repository.Exists<Worker>(x => x.Mail == request.Mail))
                throw new Exception("El usuario ya esta registrado");
            else if (!this.repository.Exists<Store>(x => x.Id.ToString() == request.StoreId))
                throw new Exception("La compañia no se encuentra registrada");

            //Crear usuario Worker
            worker = Worker.Build(
                storeId: utility.CreateId(request.StoreId),
                name: request.Name,
                lasname: request.LastName,
                identificationDocument: request.IdentificationDocument,
                mail: request.Mail,
                encryptedPassword: security.EncryptPassword(request.Password),
                cellPhoneNumber: request.CellPhoneNumber);

            //Guardar usuario en la db y retornar
            await this.repository.Save<Worker>(worker);
            await repository.Commit();

            return 0;
        }
    }
}
