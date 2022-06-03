using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CategoriesServices.RemoveCategory
{
    public class RemoveCategoryCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }

        public string Id { get; set; }
    }
}
