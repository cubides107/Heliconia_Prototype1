using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.AccountingServices.ManageLedger
{
    public class ManageLedgerCommand : IRequest<int>
    {
        public Vendor Vendor { get; set; }

        public DateTime Date { get; set; }

        public double TotalPurchasePrice { get; set; }

        public int TotalProductsPurchase { get; set; }
    }

    public class Vendor
    {
        public string Id { get; set; }
        
        public string Role { get; set; }
    }
}
