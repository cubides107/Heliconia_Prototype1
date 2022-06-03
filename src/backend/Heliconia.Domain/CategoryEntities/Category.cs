using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.CategoryEntities
{
    public class Category : CategoryElement
    {
        public List<CategoryElement> Elements { get; private set; }

        public string Name { get; private set; }

        public bool IsMain { get; private set; }

        public Guid StoreId { get; private set; }

        public Guid CategoryId { get; private set; }

        public int TotalProducts { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private Category() : base()
        {
            this.Elements = new();
        }

        private Category(string name, Guid storeId, bool isMain) : base()
        {
            if (name.Length <= 3)
                throw new Exception("La categoria debe tener minimo 4 caracteres");

            //relacion agregacion
            this.Name = Guard.Against.NullOrEmpty(name, nameof(name), "Debe tener un nombre");
            this.StoreId = storeId;
            this.IsMain = isMain;

            //relacion de composicion
            this.Elements = new();
        }

        public static Category Build(string name, Guid storeId, bool isMain)
        {
            return new Category(name, storeId, isMain);
        }

        public void ChangeMainAttributes(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name), "Debe tener un nombre");
        }

        public override void AddElement(CategoryElement element)
        {
            this.Elements.Add(element);
        }

        public override void AddElements(IEnumerable<CategoryElement> Elements)
        {
            this.Elements = Elements.ToList();
        }

        public void IncrementTotalProducts()
        {
            TotalProducts++;
        }

        public void reduceTotalProducts()
        {
            TotalProducts--;
        }
    }
}
