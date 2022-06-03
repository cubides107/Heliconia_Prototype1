using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.UsersEntities
{
    public class Manager : User
    {
        public Guid CompanyId { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private Manager(): base()
        {

        }

        private Manager(Guid companyId, string name, string lasname, string identificationDocument, string mail,
            string encryptedPassword, string cellPhoneNumber)
            : base(name, lasname, identificationDocument, mail, encryptedPassword, cellPhoneNumber)
        {
            CompanyId = Guard.Against.NullOrEmpty(companyId,
                nameof(companyId), "Nombre es obligatorio");
        }

        public static Manager Build(Guid companyId, string name, string lasname, string identificationDocument, string mail,
            string encryptedPassword, string cellPhoneNumber)
        {
            return new Manager(companyId, name, lasname, identificationDocument, mail, encryptedPassword, cellPhoneNumber);
        }
    }
}
