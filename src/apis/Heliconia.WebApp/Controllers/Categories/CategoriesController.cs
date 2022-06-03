using Heliconia.Application.CategoriesServices.AddChildCategory;
using Heliconia.Application.CategoriesServices.CloneProducts;
using Heliconia.Application.CategoriesServices.CreateParentCategory;
using Heliconia.Application.CategoriesServices.GetAllProducts;
using Heliconia.Application.CategoriesServices.GetChildCategories;
using Heliconia.Application.CategoriesServices.GetParentCategories;
using Heliconia.Application.CategoriesServices.ModifyCategory;
using Heliconia.Application.CategoriesServices.ModifyProduct;
using Heliconia.Application.CategoriesServices.RegisterProduct;
using Heliconia.Application.CategoriesServices.RemoveCategory;
using Heliconia.Application.CategoriesServices.RemoveProduct;
using Heliconia.Domain.CategoryEntities;
using Heliconia.WebApp.Filters;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heliconia.WebApp.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// api para el registro de categoria padre
        /// </summary>
        /// <param name="internalRegisterExternalCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("RegisterParentCategory")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> RegisterParentCategory(CreateParentCategoryRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new CreateParentCategoryCommand
            {
                StoreId = request.StoreId,
                Name = request.Name,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// api para el registro de categorias hijas
        /// </summary>
        /// <param name="internalRegisterExternalCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("RegisterChildCategory")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> RegisterChildCategory(AddChildCategoryRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new AddChildCategoryCommand
            {
                ParentCategoryId = request.ParentCategoryId,
                Name = request.Name,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para registrar un producto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut]
        [Route("RegisterProduct")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> RegisterProduct(RegisterProductRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new RegisterProductCommand
            {
                CategoryId = request.CategoryId,
                Product = new()
                {
                    Price = request.Price
                },

                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para obtener la lista de categorias padre
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetParentCategories")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> GetParentCategories(string filterName, int page, int pageSize)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var query = new GetParentCategoriesQuery
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
        /// Api para obtener las categorias y  productos hijos
        /// </summary>
        /// <param name="filterName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetChildCategories")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> GetChildCategories(string parentCategoryId)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var query = new GetChildCategoriesQuery
            {
                ParentCategoryId = parentCategoryId,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        /// <summary>
        /// Api para modificar una categoria
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("ModifyCategory")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> ModifyCategory(ModifyCategoryRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new ModifyCategoryCommand
            {
                CategoryDataRequest = new()
                {
                    Id = request.Id,
                    Name = request.Name,
                },
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para eliminar una categoria
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("RemoveCategory")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> RemoveCategory(RemoveCategoryRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new RemoveCategoryCommand
            {
                Id = request.Id,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para eliminar un producto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("RemoveProduct")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> RemoveProduct(RemoveProductRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new RemoveProductCommand
            {
                Id = request.Id,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para modificar un producto
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [Route("ModifyProduct")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> ModifyProduct(ModifyProductRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new ModifyProductCommand
            {
                ProductDataRequest = new()
                {
                    Id = request.Id,
                    Price = request.Price,
                },
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

        /// <summary>
        /// Api para obtener todos los productos por filtro de precio y estados
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="filterState"></param>
        /// <param name="startPrice"></param>
        /// <param name="endPrice"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpGet]
        [Route("GetAllProducts")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> GetAllProducts(int page, int pageSize, string filterState, double startPrice, double endPrice)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var query = new GetAllProductsQuery
            {
                Page = page,
                PageSize = pageSize,
                FilterState = filterState,
                StartPrice = startPrice,
                EndPrice = endPrice,
                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(query);
            return Ok(dto);
        }

        /// <summary>
        /// Api para clonar productos
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPut]
        [Route("CloneProducts")]
        [TypeFilter(typeof(ExceptionManagerConfigurationFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "HeliconiaUser, Manager, Worker")]
        public async Task<IActionResult> CloneProducts(CloneProductsRequest request)
        {
            if (!ModelState.IsValid)
                throw new Exception("modelo invalido");

            var command = new CloneProductsCommand
            {
                BarCodeProductClone = request.BarCode,

                ProductsDataRequest = request.ProductsDataRequest.ConvertAll(
                    x => new CloneProductsCommand.ProductsData
                    {
                        CategoryElementId = x.CategoryElementId,
                        Price = x.Price,
                    }),

                Claims = User.Claims.ToList()
            };

            var dto = await mediator.Send(command);
            return Ok(dto);
        }

    }
}
