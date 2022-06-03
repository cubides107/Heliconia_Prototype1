using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CompaniesServices.CreateCompany
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, int>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public CreateCompanyHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<int> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            Company company;

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //verificamos acceso
            await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);

            //Crear y guardar campaña
            company = Company.Build(
                name: request.Name, 
                descripcion: request.Descripcion,
                heliconiaUserId: Guid.Parse(this.security.GetClaim(request.Claims, ISecurity.USERID)));

            await repository.Save<Company>(company);
            await repository.Commit();

            return 0;
        }
    }
}
