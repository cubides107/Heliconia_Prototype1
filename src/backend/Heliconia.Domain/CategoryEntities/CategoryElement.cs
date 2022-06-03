using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.CategoryEntities
{
    public abstract class CategoryElement : Entity
    {
        public abstract void AddElement(CategoryElement element);

        public abstract void AddElements(IEnumerable<CategoryElement> Elements);
    }
}
