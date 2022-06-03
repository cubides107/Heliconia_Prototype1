using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.LoginManager
{
    public class LoginManagerHandler : IRequestHandler<LoginManagerCommand, string>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        public LoginManagerHandler(IRepository repository, ISecurity security)
        {
            this.repository = repository;
            this.security = security;
        }

        public async Task<string> Handle(LoginManagerCommand request, CancellationToken cancellationToken)
        {
            Manager manager;

            //Verificar que la peticion no se encuentre nula
            Guard.Against.Null(request, nameof(request));

            //Verificar que el usuario exista en la db
            if (this.repository.Exists<Manager>(x => x.Mail == request.Mail) == false)
                throw new Exception("El usuario no se encuentra registrado, compruebe el correo");

            //Se obtiene el usuario y se verifica que la contraseña sea correcta
            manager = await this.repository.Get<Manager>(x => x.Mail == request.Mail);
            if (manager.EncryptedPassword != this.security.EncryptPassword(request.Password))
                throw new Exception("La constraseña no es correcta");

            manager.Login(this.security.CreateToken(
                id: manager.Id.ToString(),
                name: manager.Name,
                mail: manager.Mail,
                role: typeof(Manager).Name));

            this.repository.Update(manager);
            await this.repository.Commit();
            return manager.Token;
        }
    }
}
