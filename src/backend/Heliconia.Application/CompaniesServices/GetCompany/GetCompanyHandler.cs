using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CompaniesServices.GetCompany
{
    public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, GetCompanyDTO>
    {
        private readonly IRepository repository;

        private readonly IUtility utility;

        private readonly ISecurity security;

        private readonly IMapObject mapObject;

        public GetCompanyHandler(IRepository repository, IUtility utility, ISecurity security, IMapObject mapObjet)
        {
            this.repository = repository;
            this.utility = utility;
            this.security = security;
            this.mapObject = mapObjet;
        }

        public async Task<GetCompanyDTO> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
        {
            Company company;
            string id;

            //Verifiar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            //Verificar Acceso del usuario manager
            if (Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);
          
            //Obtener id del usuario que realiza la peticion
            id = this.security.GetClaim(request.Claims, ISecurity.USERID);

            //Obtener compañia
            company = await this.repository.Get<Company>((x) => x.Managers.Any((x) => x.Id.ToString() == id));

            //Mapear compañia y retornar DTO
            return mapObject.Map<Company, GetCompanyDTO>(company);
        }
    }
}
