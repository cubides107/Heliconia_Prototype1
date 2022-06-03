using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CompaniesServices.ModifyBasicAttributes
{
    public class ModifyBasicAttributesCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public CompanyData CompanyDataRequest { get; set; }

        public class CompanyData
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Descripcion { get; set; }
        }
    }
}
