using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.PurchasesServices.RegisterPurchase
{
    public class RegisterPurchaseCommand: IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public PurchaseData Purchase { get; set; }  

        public class PurchaseData
        {
            public DateTime DatePurchase { get; set; }
            
            public string CustomerId { get; set; }

            public List<string> ProductsId { get; set; }
        }

    }
}
