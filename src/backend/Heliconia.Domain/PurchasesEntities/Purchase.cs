using Ardalis.GuardClauses;
using Heliconia.Domain.CategoryEntities;
using System;
using System.Collections.Generic;

namespace Heliconia.Domain.PurchasesEntities
{
    public class Purchase : Entity
    {
        public DateTime DatePurchase { get; private set; }

        public double TotalPurchasePrice { get; private set; }

        public int TotalUnits { get; private set; }

        public Guid CustomerId { get; private set; }

        public List<Product> Products { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private Purchase()
        {

        }

        private Purchase(DateTime datePurchase, Guid customerId)
        {

            DatePurchase = datePurchase;

            CustomerId = Guard.Against.NullOrEmpty(customerId,
               nameof(customerId), "Debe estar asosiado a una compra");

            Products = new();
        }

        public void AddProduct(Product product)
        {
            this.Products.Add(product);
        }

        public static Purchase Build(DateTime datePurchase, Guid customerId)
        {
            return new Purchase(datePurchase, customerId);
        }

        public void changeMainAttributes(double totalPurchasePrice, int totalUnits)
        {
            TotalPurchasePrice = totalPurchasePrice;
            TotalUnits = totalUnits;
        }
    }

}
