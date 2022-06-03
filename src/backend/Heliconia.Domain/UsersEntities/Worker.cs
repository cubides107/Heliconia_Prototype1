using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.UsersEntities
{
    public class Worker : User
    {
        public Guid StoreId { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private Worker() : base()
        {

        }

        private Worker(Guid storeId, string name, string lasname, string identificationDocument, string mail,
            string encryptedPassword, string cellPhoneNumber)
            : base(name, lasname, identificationDocument, mail, encryptedPassword, cellPhoneNumber)
        {
            StoreId = Guard.Against.NullOrEmpty(storeId,
                nameof(storeId), "debe estar relacionado a una tienda");
        }

        public static Worker Build(Guid storeId, string name, string lasname, string identificationDocument, string mail,
            string encryptedPassword, string cellPhoneNumber)
        {
            return new Worker(storeId, name, lasname, identificationDocument, mail, encryptedPassword, cellPhoneNumber);
        }
    }
}
