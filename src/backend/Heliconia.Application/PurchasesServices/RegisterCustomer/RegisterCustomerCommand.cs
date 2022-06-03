using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.PurchasesServices.RegisterCustomer
{
    public class RegisterCustomerCommand: IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public CustomerData Customer { get; set; }

        public class CustomerData
        {
            public string Name { get; set; }

            public string LastName { get; set; }

            public string IdentificationDocument { get; set; }

        }
    }
}
