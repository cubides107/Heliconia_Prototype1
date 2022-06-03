using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.LoginHeliconiaUser
{
    public class LoginHeliconiaUserCommand : IRequest<string>
    {
        public string Mail { get; set; }

        public string Password { get; set; }
    }
}
