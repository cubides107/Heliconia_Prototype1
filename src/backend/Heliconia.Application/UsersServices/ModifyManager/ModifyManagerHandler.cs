using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.ModifyManager
{
    public class ModifyManagerHandler : IRequestHandler<ModifyManagerCommand, int>
    {

        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;


        public ModifyManagerHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(ModifyManagerCommand request, CancellationToken cancellationToken)
        {
            //Comprobar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //El usuario Heliconia Modifica el usuario manager
            await UserShared.ModifyUser<HeliconiaUser, Manager>(request, security, repository, utility);
            
            //El usuario Manager modifica los datos de si mismo 
            await UserShared.ModifyUser<Manager>(request,security,repository,utility);
           
            return 0;
        }
    }
}
