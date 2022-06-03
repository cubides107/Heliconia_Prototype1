using Ardalis.GuardClauses;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.LoginWorker
{
    public class LoginWorkerHandler : IRequestHandler<LoginWorkerCommand, string>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        public LoginWorkerHandler(IRepository repository, ISecurity security)
        {
            this.repository = repository;
            this.security = security;
        }
        public async Task<string> Handle(LoginWorkerCommand request, CancellationToken cancellationToken)
        {
            Worker worker;

            //Verificar la peticion
            Guard.Against.Null(request, nameof(request));

            //Verificar que el usuario exista en la db
            if (this.repository.Exists<Worker>(x => x.Mail == request.Mail) == false)
                throw new Exception("El usuario no se encuentra registrado, compruebe el correo");

            //Se obtiene el usuario y se verifica que la contraseña sea correcta
            worker = await this.repository.Get<Worker>(x => x.Mail == request.Mail);
            if (worker.EncryptedPassword != this.security.EncryptPassword(request.Password))
                throw new Exception("La constraseña no es correcta");

            worker.Login(this.security.CreateToken(
                id: worker.Id.ToString(),
                name: worker.Name,
                mail: worker.Mail,
                role: typeof(Worker).Name));

            this.repository.Update(worker);
            await this.repository.Commit();
            return worker.Token;

        }
    }
}
