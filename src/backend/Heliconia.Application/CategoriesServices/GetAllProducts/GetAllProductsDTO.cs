using Heliconia.Domain.CategoryEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.GetAllProducts
{
    public class GetAllProductsDTO
    {
        public string Id { get; set; }

        public double Price { get; set; }

        public string CodigoBarras { get; set; }

        public string NameLastState { get; set; }   

        /// <summary>
        /// El producto puede o no tener una compra
        /// </summary>
        public string PurchaseId { get; set; }
    }
}
