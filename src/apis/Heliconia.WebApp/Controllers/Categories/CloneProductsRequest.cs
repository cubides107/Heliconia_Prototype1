using Heliconia.Application.CategoriesServices.CloneProducts;
using System;
using System.Collections.Generic;

namespace Heliconia.WebApp.Controllers.Categories
{
    public class CloneProductsRequest
    {
        public string BarCode { get; set; }

        public List<ProductsData> ProductsDataRequest { get; set; }

        public class ProductsData
        {
            public double? Price { get; set; }

            public Guid? CategoryElementId { get; set; }
        }
    }
}
