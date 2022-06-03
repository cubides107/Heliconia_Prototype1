using Heliconia.Application.UsersServices.GetTypeUser;
using Heliconia.Application.UsersServices.GetUsersName;
using Heliconia.Application.UsersServices.LoginHeliconiaUser;
using Heliconia.Application.UsersServices.LoginManager;
using Heliconia.Application.UsersServices.LoginWorker;
using Heliconia.Application.UsersServices.LogoutUser;
using Heliconia.Application.UsersServices.ModifyHeliconiaUser;
using Heliconia.Application.UsersServices.ModifyManager;
using Heliconia.Application.UsersServices.ModifyWorker;
using Heliconia.Application.UsersServices.RegisterHeliconiaUser;
using Heliconia.Application.UsersServices.RegisterManager;
using Heliconia.Application.UsersServices.RegisterWorker;
using Heliconia.Application.UsersServices.RemoveUser;
using Heliconia.WebApp.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Heliconia.WebApp.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// api para el registro del usuario Heliconia
        /// </summary>
        /// <param name="internalRegisterExternalCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("RegisterHeliconia")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        public async Task<IActionResult> Register(RegisterHeliconiaRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new RegisterHeliconiaUserCommand
            {
                Mail = request.Mail,
                Password = request.Password,
                IdentificationDocument = request.IdentificationDocument,
                CellPhoneNumber = request.CellPhoneNumber,
                Lasname = request.Lasname,
                Name = request.Name
            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// api para el login del usuario Heliconia
        /// </summary>
        /// <param name="internalRegisterExternalCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginHeliconia")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new LoginHeliconiaUserCommand
            {
                Mail = request.Mail,
                Password = request.Password,
            };

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// api para el logout del usuario Heliconia
        /// </summary>
        /// <param name="internalRegisterExternalCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Logout")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Worker, Manager")]
        public async Task<IActionResult> Logout()
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new LogoutUserCommand();
            command.Claims = User.Claims.ToList();

            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para el registro de un usuario manager
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut]
        [Route("RegisterManager")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> RegisterManagerUser(RegisterManagerRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var command = new RegisterManagerCommand
            {
                CompanyId = request.CompanyId,
                Mail = request.Mail,
                Password = request.Password,
                IdentificationDocument = request.IdentificationDocument,
                CellPhoneNumber = request.CellPhoneNumber,
                LastName = request.LastName,
                Name = request.Name
            };
            command.UserClaims = User.Claims.ToList();
            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para el login de un usuario Manager
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("LoginManager")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        public async Task<IActionResult> LoginManager(LoginRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var command = new LoginManagerCommand
            {
                Mail = request.Mail,
                Password = request.Password
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para el registro de un usuario Woker
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut]
        [Route("RegisterWorker")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> RegisterWorkerUser(RegisterWorkerRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var command = new RegisterWorkerCommand
            {
                StoreId = request.StoreId,
                Mail = request.Mail,
                Password = request.Password,
                IdentificationDocument = request.IdentificationDocument,
                CellPhoneNumber = request.CellPhoneNumber,
                LastName = request.LastName,
                Name = request.Name
            };
            command.UserClaims = User.Claims.ToList();
            var dto = await mediator.Send(command);

            return Ok(dto);
        }

        /// <summary>
        /// Api para login de un usuario worker
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("LoginWorker")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        public async Task<IActionResult> LoginWorker(LoginRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var command = new LoginWorkerCommand
            {
                Mail = request.Mail,
                Password = request.Password
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para modificar un usuario heliconia
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("ModifyHeliconia")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser")]
        public async Task<IActionResult> ModifyHeliconia(ModifyUserRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var command = new ModifyHeliconiaUserCommand
            {
                claims = User.Claims.ToList(),
                
                dataUser = new()
                {
                    id = request.id,
                    Name = request.Name,
                    LastName = request.LastName,
                    IdentificationDocument = request.IdentificationDocument,  
                    Phone = request.Phone
                }
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para modificar un usuario manager
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("ModifyManager")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> ModifyManager(ModifyUserRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var command = new ModifyManagerCommand
            {
                claims = User.Claims.ToList(),
             
                dataUser = new()
                {
                    id = request.id,
                    Name = request.Name,
                    LastName = request.LastName,
                    IdentificationDocument = request.IdentificationDocument,
                    Phone = request.Phone
                }
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para modificar un usuario Worker
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("ModifyWorker")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> ModifyWorker(ModifyUserRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var command = new ModifyWorkerCommand
            {
                claims = User.Claims.ToList(),
    
                dataUser = new()
                {
                    id = request.id,
                    Name = request.Name,
                    LastName = request.LastName,
                    IdentificationDocument = request.IdentificationDocument,
                    Phone = request.Phone
                }
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }
        
        /// <summary>
        /// Api para obtener el listado de usuarios por nombre
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetUsersByName")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> GetUsersByName(string filterName, int page, int pageSize)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var query = new GetUsersNameQuery
            {
                FilterName = filterName,
                Page = page,
                PageSize = pageSize,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        /// <summary>
        /// Api para eliminar un usuario de la bd, es decir cambiar el estado del mismo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("RemoveUser")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager")]
        public async Task<IActionResult> RemoveUser(RemoveUserRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var query = new RemoveUserCommand
            {
                Id = request.Id,
                Claims = User.Claims.ToList(),
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        /// <summary>
        /// Api para obtener el tipo de usuario
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetTypeUser")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        public async Task<IActionResult> GetTypeUser(string mail)
        {
            if (!ModelState.IsValid)
                throw new Exception("Modelo Invalido");

            var query = new GetTypeUserQuery
            {
                Mail = mail
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }
    }
}
