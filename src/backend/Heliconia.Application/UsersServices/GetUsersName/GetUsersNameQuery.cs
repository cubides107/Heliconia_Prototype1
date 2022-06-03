using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.UsersServices.GetUsersName
{
    public class GetUsersNameQuery : IRequest<GetUsersNameDTO>
    {
        public List<Claim> Claims { get; set; }
        public string FilterName { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
