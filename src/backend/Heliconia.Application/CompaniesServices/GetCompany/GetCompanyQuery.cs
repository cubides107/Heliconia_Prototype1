using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CompaniesServices.GetCompany
{
    public class GetCompanyQuery : IRequest<GetCompanyDTO>
    {
        public List<Claim> Claims { get; set; }
    }
}
