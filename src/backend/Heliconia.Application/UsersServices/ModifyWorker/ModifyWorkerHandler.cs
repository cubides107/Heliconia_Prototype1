using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.ModifyWorker
{
    public class ModifyWorkerHandler : IRequestHandler<ModifyWorkerCommand, int>
    {

        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public ModifyWorkerHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(ModifyWorkerCommand request, CancellationToken cancellationToken)
        {
            //Comprobar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //El usuario Heliconia realiza la modificacion de un usuario Worker
            await UserShared.ModifyUser<HeliconiaUser, Worker>(request,security,repository,utility);

            //El usuario Manager Realiza la modificacion de un usuario Worker
            await UserShared.ModifyUser<Manager, Worker>(request, security, repository, utility);

            //El usuario Worker realiza la modificaicon de los datos a si mismo
            await UserShared.ModifyUser<Worker>(request, security, repository, utility);

            return 0;
        }
    }
}
