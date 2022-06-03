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

namespace Heliconia.Application.UsersServices.LoginHeliconiaUser
{
    public class LoginHeliconiaUserHandler : IRequestHandler<LoginHeliconiaUserCommand, string>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public LoginHeliconiaUserHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<string> Handle(LoginHeliconiaUserCommand request, CancellationToken cancellationToken)
        {
            HeliconiaUser heliconiaUser;

            //verificamos la peticion
            Guard.Against.Null(request, nameof(request));

            //verificar que este en el web config y que el usuario existe en la db
            if (this.utility.CheckValueInlistJson("heliconiasMailsJson", request.Mail) == false)
                throw new Exception("El usuario no tiene permisos");

            else if (this.repository.Exists<HeliconiaUser>(x => x.Mail == request.Mail) == false)
                throw new Exception("El usuario no existe, compruebe el correo");

            //entonces, obtenemos el usuario, verificamos que la contraseña corresponda y le actualizamos el token
            heliconiaUser = await this.repository.Get<HeliconiaUser>(x => x.Mail == request.Mail);
            if (heliconiaUser.EncryptedPassword != this.security.EncryptPassword(request.Password))
                throw new Exception("La contraseña no es correcta");

            heliconiaUser.Login(this.security.CreateToken(
                id: heliconiaUser.Id.ToString(), 
                name: heliconiaUser.Name, 
                mail: heliconiaUser.Mail, 
                role: typeof(HeliconiaUser).Name));

            //actualizmos cambios
            this.repository.Update(heliconiaUser);
            await this.repository.Commit();
            return heliconiaUser.Token;
        }
    }
}
