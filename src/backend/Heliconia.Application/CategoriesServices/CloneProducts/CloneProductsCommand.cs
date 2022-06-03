using MediatR;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CategoriesServices.CloneProducts
{
    public class CloneProductsCommand : IRequest<List<string>>
    {
        public List<Claim> Claims { get; set; }

        public string BarCodeProductClone { get; set; }

        public List<ProductsData> ProductsDataRequest { get; set; }

        public class ProductsData
        {
            public double? Price { get; set; }

            public Guid? CategoryElementId { get; set; }
        }
    }
}
