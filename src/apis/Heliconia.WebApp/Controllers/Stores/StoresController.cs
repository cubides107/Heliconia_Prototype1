using Heliconia.Application.StoresServices.CreateStore;
using Heliconia.Application.StoresServices.GetAllStores;
using Heliconia.Application.StoresServices.GetStore;
using Heliconia.Application.StoresServices.ModifyStore;
using Heliconia.Application.StoresServices.RemoveStore;
using Heliconia.WebApp.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Heliconia.WebApp.Controllers.Stores
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly IMediator mediator;

        public StoresController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// api para el registro de tiendas
        /// </summary>
        /// <param name="internalRegisterExternalCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Register")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> Register(RegisterStoreRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new CreateStoreCommand
            {
                Descripcion = request.Descripcion,
                Name = request.Name,
                CompanyId = request.CompanyId,
                Claims = User.Claims.ToList()

            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para obtener la tienda de un usuario Worker
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetStoreWorker")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Worker")]
        public async Task<IActionResult> GetStoreWorker()
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new GetStoreQuery
            {
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para obtener el listado de tiendas de una compañia
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetAllStores")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager")]
        public async Task<IActionResult> GetAllStore(int page, int pageSize)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new GetAllStoresQuery
            {
                page = page,
                pageSize = pageSize,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para modificar una tienda
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("ModifyStore")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> ModifyStore(ModifyStoreRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new ModifyStoreCommand
            {
                StoreRequest = new()
                {
                    Id = request.Id,
                    Name = request.Name,
                    Description = request.Descripcion
                },
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para eliminar una tienda
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("RemoveStore")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> RemoveStore(RemoveStoreRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new RemoveStoreCommand
            {
                Id = request.Id,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }
    }
}
