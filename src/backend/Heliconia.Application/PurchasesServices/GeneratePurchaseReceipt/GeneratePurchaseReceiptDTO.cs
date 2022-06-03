using System;
using System.Collections.Generic;

namespace Heliconia.Application.PurchasesServices.GeneratePurchaseReceipt
{
    public class GeneratePurchaseReceiptDTO
    {
        public PurchaseReceiptDTO purchaseReceiptDTO { get; set; }

        public class ProductDTO
        {
            public string Id { get; set; }

            public double Price { get; set; }

            public string CodigoBarras { get; set; }

        }

        public class PurchaseDTO
        {
            public string Id { get; set; }

            public DateTime DatePurchase { get; set; }

            public double TotalPurchasePrice { get; set; }

            public int TotalUnits { get; set; }

            public List<ProductDTO> Products { get; set; }
        }

        public class CustomerDTO
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string LastName { get; set; }

            public string IdentificationDocument { get; set; }
        }

        public class PurchaseReceiptDTO
        {
            public CustomerDTO customer { get; set; }

            public PurchaseDTO purchase { get; set; }
        }


    }
}
