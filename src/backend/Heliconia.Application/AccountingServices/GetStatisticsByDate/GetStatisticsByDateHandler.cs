using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
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

namespace Heliconia.Application.AccountingServices.GetStatisticsByDate
{
    public class GetStatisticsByDateHandler : IRequestHandler<GetStatisticsByDateQuery, GetStatisticsByDateDTO>
    {
        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        private const int Days_TwoYears = 730;

        public GetStatisticsByDateHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<GetStatisticsByDateDTO> Handle(GetStatisticsByDateQuery request, CancellationToken cancellationToken)
        {
            GetStatisticsByDateDTO dto = new();

            //Verifiar que la peticion no este nula
            Guard.Against.Null(request, nameof(request));

            if ((request.EndDate - request.StartDate).TotalDays > Days_TwoYears)
                throw new Exception("Solo se puede consultar en un rango de 2 años");

            //Verificar Acceso del usuario Heliconia y Manager
            if (Access.IsUserType<HeliconiaUser>(request.Claims, security))
                await Access.VerifyAccess<HeliconiaUser>(request.Claims, repository, security, utility);
            else if (Access.IsUserType<Manager>(request.Claims, security))
                await Access.VerifyAccess<Manager>(request.Claims, repository, security, utility);

            //Verificar que la compañia exista
            if (repository.Exists<Company>(x => x.Id.ToString() == request.CompanyId) is false)
                throw new Exception("La compañia no existe");

            //Obtener los libros mayores de la compañia en el rango de fechas
            List<DailyLedger> dailyLedgers = await this.repository.GetAll<DailyLedger>(
                x => x.CompanyId.ToString() == request.CompanyId,
                x => x.Date >= request.StartDate && x.Date <= request.EndDate);

            //Obtener la suma total y retornar
            dailyLedgers.ForEach(x =>
            {
                dto.TotalDailyLedgersPrice += x.MoneyTotalSales;
                dto.TotalDailyLedgersProducts += x.TotalProductsSold;
            });

            return dto;
        }

    }
}

