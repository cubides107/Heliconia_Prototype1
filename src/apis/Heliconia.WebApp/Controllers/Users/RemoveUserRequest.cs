using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Users
{
    public class RemoveUserRequest
    {
        [Required]
        public string Id { get; set; }
    }
}
