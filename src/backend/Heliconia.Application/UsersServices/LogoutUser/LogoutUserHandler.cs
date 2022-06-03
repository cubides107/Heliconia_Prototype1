using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.LogoutUser
{
    public class LogoutUserHandler : IRequestHandler<LogoutUserCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        public LogoutUserHandler(IRepository repository, ISecurity security)
        {
            this.repository = repository;
            this.security = security;
        }

        public async Task<int> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //des-loguear el usaurio segun el tipo de usuario
            if (await Logout<HeliconiaUser>(request) == false)
                if (await Logout<Manager>(request) == false)
                    if (await Logout<Worker>(request) == false)
                        throw new Exception("No se pudo desloquear ya que no pertenecese a ningun usuario");

            return 0;
        }

        private async Task<bool> Logout<T>(LogoutUserCommand request) where T : User
        {
            //verificamos si existe
            T user;
            var isLogout = false;
            var id = this.security.GetClaim(request.Claims, ISecurity.USERID);

            if (repository.Exists<T>(x => x.Id.ToString() == id))
            {
                user = await repository.Get<T>(x => x.Id.ToString() == id);
                if (user.Token == string.Empty)
                    throw new Exception("El usuario ya esta deslogeado");

                user.Logout();
                repository.Update<T>(user);
                await repository.Commit();
                isLogout = true;
            }

            return isLogout;
        }
    }
}
