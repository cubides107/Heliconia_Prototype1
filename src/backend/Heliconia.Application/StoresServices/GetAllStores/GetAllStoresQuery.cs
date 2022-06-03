using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.StoresServices.GetAllStores
{
    public class GetAllStoresQuery : IRequest<List<GetAllStoresDTO>>
    {
        public int page { get; set; }

        public int pageSize { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
