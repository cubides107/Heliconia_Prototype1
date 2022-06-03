using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.RegisterProduct
{
    public class RegisterProductCommand : IRequest<string>
    {
        public List<Claim> Claims { get; set; }

        public string CategoryId { get; set; }

        public ProductData Product { get; set; }

        public class ProductData
        {
            public double Price { get; set; }
        }
    }
}
