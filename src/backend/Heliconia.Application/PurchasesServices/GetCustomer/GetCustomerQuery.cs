using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.PurchasesServices.GetCustomer
{
    public class GetCustomerQuery : IRequest<GetCustomerDTO>
    {
        public List<Claim> Claims { get; set; }

        public string CustomerDocument { get; set; }

    }
}
