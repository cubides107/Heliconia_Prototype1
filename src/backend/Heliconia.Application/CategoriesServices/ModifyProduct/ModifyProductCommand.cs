using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CategoriesServices.ModifyProduct
{
    public class ModifyProductCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public ProductData ProductDataRequest { get; set; }

        public class ProductData
        {
            public string Id { get; set; }
            public double Price { get; set; }
        }
    }
}
