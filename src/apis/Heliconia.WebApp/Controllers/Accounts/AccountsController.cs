using Heliconia.Application.AccountingServices.GetStatisticsByDate;
using Heliconia.WebApp.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Heliconia.WebApp.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api para obtener estadisticas de los Daily Ledgers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetStatisticsByDate")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> GetStatisticsByDate(GetStatisticsByDateRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var query = new GetStatisticsByDateQuery
            {
                StartDate = request.StartDate.Date,
                EndDate = request.EndDate.Date,
                CompanyId = request.CompanyId,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }

    }
}
