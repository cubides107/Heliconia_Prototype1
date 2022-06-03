using Heliconia.Domain.CategoryEntities;
using System.Collections.Generic;

namespace Heliconia.Application.CategoriesServices.GetChildCategories
{
    public class GetChildCategoriesDTO
    {

        public CategoryDTO Category { get; set; }

        public class CategoryDTO
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string StoreId { get; set; }

            public int TotalProducts { get; set; }

            public List<CategoryDTO> Elements { get; set; }

        }
    }
}