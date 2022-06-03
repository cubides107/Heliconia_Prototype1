using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.UsersServices.GetTypeUser
{
    public class GetTypeUserQuery :  IRequest<GetTypeUserDTO>
    {
        public string Mail { get; set; }
    }
}
