using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Categories
{
    public class ModifyCategoryRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
