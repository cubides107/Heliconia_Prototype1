using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Users
{
    public class RegisterManagerRequest
    {
        [Required]
        public string CompanyId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string IdentificationDocument { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string CellPhoneNumber { get; set; }
    }
}
