using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Heliconia.Domain.PurchasesEntities
{
    public class Customer : Entity
    {
        public string Name { get; private set; }

        public string LastName { get; private set; }

        public string IdentificationDocument { get; private set; }

        public List<Purchase> Purchases { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private Customer()
        {

        }

        private Customer(string name, string lastName, string identificationDocument)
        {
            Name = Guard.Against.NullOrEmpty(name,
                nameof(name), "Nombre es obligatorio");

            LastName = Guard.Against.NullOrEmpty(lastName,
               nameof(lastName), "Apellido es obligatorio");

            IdentificationDocument = Guard.Against.NullOrEmpty(identificationDocument,
                nameof(identificationDocument), "Documento Obligatorio");
        }

        public static Customer Build(string name, string lastName, string identificationDocument)
        {
            return new Customer(name, lastName, identificationDocument);
        }
    }
}
