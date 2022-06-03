using Ardalis.GuardClauses;
using Heliconia.Domain.AccountingEntities;
using Heliconia.Domain.UsersEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.CompaniesEntities
{
    public class Company : Entity
    {
        public string Name { get; private set; }

        public string Descripcion { get; private set; }

        public Guid HeliconiaUserId { get; private set; }

        public List<Store> Companies { get; private set; }

        public List<Manager> Managers { get; private set; }

        public List<DailyLedger> DailyLedgers { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private Company(): base()
        {

        }

        private Company(string name, string descripcion, Guid heliconiaUserId) : base()
        {
            Name = Guard.Against.NullOrEmpty(name,
                nameof(name), "Nombre es obligatorio");

            Descripcion = Guard.Against.NullOrEmpty(descripcion,
                nameof(descripcion), "descripion es obligatorio");

            HeliconiaUserId = Guard.Against.NullOrEmpty(heliconiaUserId,
                nameof(heliconiaUserId), "debe estar asociado a una heliconia");

            this.Companies = new List<Store>();
            this.Managers = new List<Manager>();
        }

        public static Company Build(string name, string descripcion, Guid heliconiaUserId)
        {
            return new Company(name, descripcion, heliconiaUserId);
        }

        public void ChangeMainAttributes(string name, string descripcion)
        {
            Name = Guard.Against.NullOrEmpty(name,
                nameof(name), "Nombre es obligatorio");

            Descripcion = Guard.Against.NullOrEmpty(descripcion,
                nameof(descripcion), "descripion es obligatorio");
        }
    }
}
