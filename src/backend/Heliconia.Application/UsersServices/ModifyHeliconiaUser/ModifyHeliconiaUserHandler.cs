
using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.ModifyHeliconiaUser
{
    public class ModifyHeliconiaUserHandler : IRequestHandler<ModifyHeliconiaUserCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;


        public ModifyHeliconiaUserHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(ModifyHeliconiaUserCommand request, CancellationToken cancellationToken)
        {
            //Comprobar que la peticion no se encuentre nula
            Guard.Against.Null(request, nameof(request));
            
            //El usuario Heliconia modifica los datos de otro usuario heliconia o de si mismo
            await UserShared.ModifyUser<HeliconiaUser, HeliconiaUser>(request, security, repository, utility);

            return 0;
        }
    }
}
