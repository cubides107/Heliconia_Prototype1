using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.StoresServices.CreateStore
{
    public class CreateStoreCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string Descripcion { get; set; }

        public string CompanyId { get; set; }

        public List<Claim> Claims { get; set; }
    }
}
