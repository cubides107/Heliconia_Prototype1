using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.GetAllProducts
{
    public class GetAllProductsQuery: IRequest<List<GetAllProductsDTO>>
    {
        public List<Claim> Claims { get; set; }

        public string FilterState { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public double StartPrice { get; set; }

        public double EndPrice { get; set; }


    }
}
