using Ardalis.GuardClauses;
using Heliconia.Domain;
using Heliconia.Domain.AccountingEntities;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Domain.UsersEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.AccountingServices.ManageLedger
{
    public class ManageLedgerHandler : IRequestHandler<ManageLedgerCommand, int>
    {

        private readonly IRepository repository;

        public ManageLedgerHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<int> Handle(ManageLedgerCommand request, CancellationToken cancellationToken)
        {
            DailyLedger dailyLedger;
            Worker worker;
            Company company = null;

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //Obtener compañia a traves del usuario que atiende la compra
            switch (request.Vendor.Role)
            {
                case "Manager":
                    company = await this.repository.Get<Company>((x) => x.Managers.Any((x) => x.Id.ToString() == request.Vendor.Id));
                    break;
                case "Worker":
                    worker = await repository.Get<Worker>(x => x.Id.ToString() == request.Vendor.Id);
                    company = await this.repository.Get<Company>((x) => x.Companies.Any((x) => x.Id.ToString() == worker.StoreId.ToString()));
                    break;
                default:
                    break;
            }

            //Obtener o crear el Libro mayor diario, actualizando campos de precio y de total de productos
            if (repository.Exists<DailyLedger>(x => x.CompanyId.ToString() == company.Id.ToString(), x => x.Date == request.Date))
            {
                dailyLedger = await repository.Get<DailyLedger>(x => x.CompanyId.ToString() == company.Id.ToString(), x => x.Date == request.Date);
                ManageLedgerHandler.IncrementAttributesDailyLedger(request.TotalPurchasePrice, request.TotalProductsPurchase, dailyLedger);
                repository.Update<DailyLedger>(dailyLedger);
            }
            else
            {
                dailyLedger = DailyLedger.Build(
                    companyId: company.Id,
                    date: request.Date);

                ManageLedgerHandler.IncrementAttributesDailyLedger(request.TotalPurchasePrice, request.TotalProductsPurchase, dailyLedger);
                await repository.Save<DailyLedger>(dailyLedger);
            }

            await repository.Commit();

            return 0;

        }

        /// <summary>
        /// Incrementa los atributos del libro diario mayor
        /// </summary>
        /// <param name="totalPurchasePrice"></param>
        /// <param name="totalProductsPurchase"></param>
        /// <param name="dailyLedger"></param>
        private static void IncrementAttributesDailyLedger(double totalPurchasePrice, int totalProductsPurchase, DailyLedger dailyLedger)
        {
            dailyLedger.IncreaseTotalSales(totalPurchasePrice);
            dailyLedger.IncreaseTotalProductsSold(totalProductsPurchase);
        }

    }
}
