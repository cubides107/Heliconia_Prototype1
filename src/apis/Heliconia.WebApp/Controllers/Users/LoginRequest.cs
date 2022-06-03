using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Users
{
    public class LoginRequest
    {
        [Required]
        public string Mail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}