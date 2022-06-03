using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.AddChildCategory
{
    public class AddChildCategoryCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public string Name { get; set; }

        public string ParentCategoryId { get; set; }
    }
}
