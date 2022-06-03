using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CategoriesServices.ModifyCategory
{
    public class ModifyCategoryCommand: IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public CategoryData CategoryDataRequest { get; set; }

        public class CategoryData
        {
            public string Id { get; set; }

            public string Name { get; set; }
        }

    }
}
