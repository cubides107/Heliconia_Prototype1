using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.RemoveUser
{
    public class RemoveUserHandler : IRequestHandler<RemoveUserCommand, int>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        public RemoveUserHandler(IRepository repository, IUtility utility, ISecurity security)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
        }

        public async Task<int> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            //Comprobar que la peticion no se encutre nula
            Guard.Against.Null(request, nameof(request));

            //Verificar que la peticion la realice un heliconia user o manager
            if (Access.IsUserType<HeliconiaUser>(request.Claims, security))
                await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);
            else if (Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);

            //Verificar en que tabla existe el usuario a eliminar y cambiar el estado del usuario
            if (repository.Exists<Manager>(x => x.Id.ToString() == request.Id))
                await ChangeStateRemove<Manager>(request.Id);
            else if (repository.Exists<Worker>(x => x.Id.ToString() == request.Id))
                await ChangeStateRemove<Worker>(request.Id);
            else
                throw new Exception("El usuario a Eliminar no se encontro");

            return 0;
        }

        /// <summary>
        /// Obtiene el usuario y cambia el estado del mismo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        /// <exception cref="System.Exception"></exception>
        private async Task ChangeStateRemove<T>(string Id) where T : User
        {
            T user;

            //Obtener usuario de la bd
            user = await repository.Get<T>(x => x.Id.ToString() == Id);

            //Cambiar el estado a removido y actualizar usuario en la bd
            user.GoToDeletedState();

            repository.Update<T>(user);
            await repository.Commit();
        }
    }
}
