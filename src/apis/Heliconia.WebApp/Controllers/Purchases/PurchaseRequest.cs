using System;
using System.Collections.Generic;

namespace Heliconia.WebApp.Controllers.Purchases
{
    public class PurchaseRequest
    {
        public DateTime DatePurchase { get; set; }

        public string CustomerId { get; set; }

        public List<string> ProductsId { get; set; }
    }
}
