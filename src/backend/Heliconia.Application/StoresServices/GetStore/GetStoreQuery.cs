using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.StoresServices.GetStore
{
    public class GetStoreQuery : IRequest<GetStoreDTO>
    {
        public List<Claim> Claims { get; set; }
    }
}
