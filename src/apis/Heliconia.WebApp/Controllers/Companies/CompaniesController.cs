using Heliconia.Application.CompaniesServices.CreateCompany;
using Heliconia.Application.CompaniesServices.GetCompany;
using Heliconia.Application.CompaniesServices.ModifyBasicAttributes;
using Heliconia.Application.UsersServices.RegisterHeliconiaUser;
using Heliconia.WebApp.Controllers.Users;
using Heliconia.WebApp.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heliconia.WebApp.Controllers.Companies
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CompaniesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// api para el registro del usuario Heliconia
        /// </summary>
        /// <param name="internalRegisterExternalCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Register")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser")]
        public async Task<IActionResult> Register(CreateCompanyRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new CreateCompanyCommand
            {
                Descripcion = request.Descripcion,
                Name = request.Name,
                Claims = User.Claims.ToList()

            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para obtener una compañia
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetCompany")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public async Task<IActionResult> GetCompany()
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var query = new GetCompanyQuery();

            query.Claims = User.Claims.ToList();
            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        /// <summary>
        /// Api para modificar atributos basicos de una compañia
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("ModifyBasicAttributes")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> ModifyBasicAttributes(ModifyBasicAttributesRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var commnad = new ModifyBasicAttributesCommand
            {
                CompanyDataRequest = new()
                {
                    Id = request.Id,
                    Descripcion = request.Descripcion,
                    Name = request.Name,
                },

                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(commnad);
            return Ok(dto);
        }
    }
}
