using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Categories
{
    public class CreateParentCategoryRequest
    {
        public string Name { get; set; }

        public string StoreId { get; set; }
    }
}