using Ardalis.GuardClauses;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.RegisterHeliconiaUser
{
    public class RegisterHeliconiaUserHandler : IRequestHandler<RegisterHeliconiaUserCommand, int>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        public RegisterHeliconiaUserHandler(IRepository repository, IUtility utility, ISecurity security)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
        }

        public async Task<int> Handle(RegisterHeliconiaUserCommand request, CancellationToken cancellationToken)
        {
            HeliconiaUser heliconiaUser;
            var id = utility.CreateId();

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //verificar que la heliconia user este en el web config y no permitirle registrarse si ya lo esta
            if (this.utility.CheckValueInlistJson("heliconiasMailsJson", request.Mail) == false)
                throw new Exception("No tiene permiso");

            else if (this.repository.Exists<HeliconiaUser>(x => x.Mail == request.Mail))
                throw new Exception("Ya esta registrado, debe loquearse");

            //crear, guardar heliconia user y retornar
            heliconiaUser = HeliconiaUser.Build(
                name: request.Name, 
                lasname: request.Lasname, 
                identificationDocument: request.IdentificationDocument, 
                mail: request.Mail, 
                encryptedPassword: this.security.EncryptPassword(request.Password),
                cellPhoneNumber: request.CellPhoneNumber);

            await this.repository.Save<HeliconiaUser>(heliconiaUser);
            await repository.Commit();

            return 0;
        }
    }
}
