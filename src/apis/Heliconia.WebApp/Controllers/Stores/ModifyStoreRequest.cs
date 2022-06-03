using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Heliconia.WebApp.Controllers.Stores
{
    public class ModifyStoreRequest
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Descripcion { get; set; }
    }
}