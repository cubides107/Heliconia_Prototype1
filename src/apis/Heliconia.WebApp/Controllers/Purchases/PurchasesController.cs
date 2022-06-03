using Heliconia.Application.PurchasesServices.GeneratePurchaseReceipt;
using Heliconia.Application.PurchasesServices.GetCustomer;
using Heliconia.Application.PurchasesServices.RegisterCustomer;
using Heliconia.Application.PurchasesServices.RegisterPurchase;
using Heliconia.WebApp.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Heliconia.WebApp.Controllers.Purchases
{
    [Route("api/[Controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IMediator mediator;

        public PurchasesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Api para crear un compra
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut]
        [Route("RegisterPurchase")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Manager, Worker")]
        public async Task<IActionResult> RegistePurchase(PurchaseRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new RegisterPurchaseCommand
            {
                Purchase = new RegisterPurchaseCommand.PurchaseData()
                {
                    DatePurchase = request.DatePurchase,
                    CustomerId = request.CustomerId,
                    ProductsId = request.ProductsId,
                },
                Claims = User.Claims.ToList(),
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para registrar un Comprador
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut]
        [Route("RegisterCustomer")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> RegisterCustomer(RegisterCustomerRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new RegisterCustomerCommand
            {
                Customer = new RegisterCustomerCommand.CustomerData()
                {
                    Name = request.Name,
                    LastName = request.LastName,
                    IdentificationDocument = request.IdentificationDocument,
                },
                Claims = User.Claims.ToList(),
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para obtener un comprador
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetCustomer")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> GetCustomer(string customerDocument)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var query = new GetCustomerQuery
            {
                CustomerDocument = customerDocument,
                Claims = User.Claims.ToList(),
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        /// <summary>
        /// Api para genera un recibo de compra
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GeneratePurchaseReceipt")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> GeneratePurchaseReceipt(string customerId)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var query = new GeneratePurchaseReceiptQuery
            {
                CustomerId = customerId,
                Claims = User.Claims.ToList(),
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }

    }
}
