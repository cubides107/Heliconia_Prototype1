using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.PurchasesServices.GeneratePurchaseReceipt
{
    public class GeneratePurchaseReceiptQuery : IRequest<GeneratePurchaseReceiptDTO>
    {
        public List<Claim> Claims { get; set; }

        public string CustomerId { get; set; }
    }
}
