using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Companies
{
    public class ModifyBasicAttributesRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}
