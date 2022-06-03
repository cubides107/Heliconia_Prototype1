using MediatR;
using System.Collections.Generic;
using System.Security.Claims;

namespace Heliconia.Application.CategoriesServices.RemoveProduct
{
    public class RemoveProductCommand : IRequest<int>
    {
        public List<Claim> Claims { get; set; }
        public string Id { get; set; }
    }
}
