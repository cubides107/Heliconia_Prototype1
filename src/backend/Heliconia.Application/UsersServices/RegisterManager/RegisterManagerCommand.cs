using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.UsersServices.RegisterManager
{
    public class RegisterManagerCommand : IRequest<int>
    {
        public List<Claim> UserClaims { get; set; }

        public string CompanyId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string IdentificationDocument { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string CellPhoneNumber { get; set; }
    }
}
