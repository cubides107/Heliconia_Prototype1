using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.StoresServices.ModifyStore
{
    public class ModifyStoreCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public StoreData StoreRequest { get; set; }
        
        public class StoreData
        {
            public string Id { get; set; }
            public string Name { get; set; }    
            public string Description { get; set; }
        }
    }
}
