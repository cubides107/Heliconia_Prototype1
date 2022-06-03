using Ardalis.GuardClauses;
using Heliconia.Domain.CategoryEntities;
using Heliconia.Domain.UsersEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.CompaniesEntities
{
    public class Store : Entity
    {
        public string Name { get; private set; }

        public string Descripcion { get; private set; }

        public Guid CompanyId { get; private set; }

        public List<Worker> Workers { get; private set; }

        public List<Category> Categories { get; private set; }

        private Store() : base()
        {

        }

        private Store(string name, string descripcion, Guid companyId) : base()
        {
            Name = Guard.Against.NullOrEmpty(name,
                nameof(name), "Nombre es obligatorio");

            Descripcion = Guard.Against.NullOrEmpty(descripcion,
                nameof(descripcion), "descripion es obligatorio");

            CompanyId = companyId;

            this.Workers = new List<Worker>();
            this.Categories = new List<Category>();
        }

        public static Store Build(string name, string descripcion, Guid companyId)
        {
            return new Store(name, descripcion, companyId);
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
