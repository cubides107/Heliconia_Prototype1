using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.RegisterHeliconiaUser
{
    public class RegisterHeliconiaUserCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Lasname { get; set; }

        public string IdentificationDocument { get; set; }

        public string Mail { get; set; }

        public string Password { get; set; }

        public string CellPhoneNumber { get; set; }
    }
}
