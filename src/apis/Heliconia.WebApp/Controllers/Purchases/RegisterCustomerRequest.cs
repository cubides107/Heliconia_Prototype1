using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Purchases
{
    public class RegisterCustomerRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdentificationDocument { get; set; }
    }
}
