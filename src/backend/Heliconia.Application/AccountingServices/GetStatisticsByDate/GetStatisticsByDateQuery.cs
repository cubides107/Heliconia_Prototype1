using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.AccountingServices.GetStatisticsByDate
{
    public class GetStatisticsByDateQuery :  IRequest<GetStatisticsByDateDTO>
    {
        public List<Claim> Claims { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }   

        public string CompanyId { get; set; }  
    }
}
