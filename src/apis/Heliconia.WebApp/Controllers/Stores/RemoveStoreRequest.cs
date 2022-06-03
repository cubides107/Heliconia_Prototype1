using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Stores
{
    public class RemoveStoreRequest
    {
        [Required]
        public string Id { get; set; }
    }
}
