using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.LogoutUser
{
    public class LogoutUserCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }
    }
}
