using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.StoresServices.RemoveStore
{
    public class RemoveStoreCommand: IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public string Id { get; set; }
    }
}
