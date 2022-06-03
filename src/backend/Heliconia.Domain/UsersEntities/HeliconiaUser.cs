using Ardalis.GuardClauses;
using Heliconia.Domain.CompaniesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.UsersEntities
{
    public class HeliconiaUser : User
    {

        public List<Company> Companies { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private HeliconiaUser(): base()
        {

        }

        private HeliconiaUser(string name, string lasname, string identificationDocument, string mail,
            string encryptedPassword, string cellPhoneNumber) 
            : base(name, lasname, identificationDocument, mail, encryptedPassword, cellPhoneNumber)
        {
            this.Companies = new List<Company>(); 
        }

        public static HeliconiaUser Build(string name, string lasname, string identificationDocument, string mail,
            string encryptedPassword, string cellPhoneNumber)
        {
            return new HeliconiaUser(name, lasname, identificationDocument, mail, encryptedPassword, cellPhoneNumber);
        }
    }
}
