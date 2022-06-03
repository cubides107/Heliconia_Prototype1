using System;
using System.ComponentModel.DataAnnotations;

namespace Heliconia.WebApp.Controllers.Accounts
{
    public class GetStatisticsByDateRequest
    {
        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string CompanyId { get; set; }
    }
}
