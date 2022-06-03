using System;

namespace Heliconia.Domain.AccountingEntities
{
    public class DailyLedger : Entity
    {
        public double MoneyTotalSales { get; private set; }

        public int TotalProductsSold { get; private set; }

        public DateTime Date { get; private set; }

        public Guid CompanyId { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private DailyLedger()
        {

        }

        private DailyLedger(Guid companyId, DateTime date)
        {
            CompanyId = companyId;
            Date = date;
        }

        public static DailyLedger Build(Guid companyId, DateTime date)
        {
            return new DailyLedger(companyId, date);
        }

        public void IncreaseTotalSales(double saleMoney)
        {
            MoneyTotalSales += saleMoney;
        }

        public void IncreaseTotalProductsSold(int productsSold)
        {
            TotalProductsSold += productsSold;
        }

        public void DecreaseTotalSales(double saleMoney)
        {
            MoneyTotalSales -= saleMoney;
        }

        public void DecreaseTotalProductsSold(int productsSold)
        {
            TotalProductsSold -= productsSold;
        }

    }


}
