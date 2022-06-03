using Heliconia.Domain.CategoryEntities;

namespace Heliconia.WebApp.Controllers.Categories
{
    public class GetAllProductsRequest
    {
        public EstadoEnum FilterState { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public double StartPrice { get; set; }

        public double EndPrice { get; set; }
    }
}
