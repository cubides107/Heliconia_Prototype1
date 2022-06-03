using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CategoriesServices.GetChildCategories
{
    public class GetChildCategoriesQuery : IRequest<GetChildCategoriesDTO>
    {

        public List<Claim> Claims { get; set; }

        public string ParentCategoryId { get; set; }

    }
}
