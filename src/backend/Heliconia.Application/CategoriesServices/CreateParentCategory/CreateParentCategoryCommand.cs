using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.CreateParentCategory
{
    public class CreateParentCategoryCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public string Name { get; set; }

        public string StoreId { get; set; }
    }
}
