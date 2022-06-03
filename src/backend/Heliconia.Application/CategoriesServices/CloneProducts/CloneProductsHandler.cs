using Ardalis.GuardClauses;
using Heliconia.Application.Shared;
using Heliconia.Domain;
using Heliconia.Domain.CategoryEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Heliconia.Application.CategoriesServices.CloneProducts
{
    public class CloneProductsHandler : IRequestHandler<CloneProductsCommand, List<string>>
    {

        private readonly IRepository repository;

        private readonly ISecurity security;

        private readonly IUtility utility;

        public CloneProductsHandler(IRepository repository, ISecurity security, IUtility utility)
        {
            this.repository = repository;
            this.security = security;
            this.utility = utility;
        }

        public async Task<List<string>> Handle(CloneProductsCommand request, CancellationToken cancellationToken)
        {
            Product product;
            Product cloneProduct;
            Category category;
            List<string> barcodes = new(); 

            //verificar request
            Guard.Against.Null(request, nameof(request));

            //verificar acceso de cualquir usuario
            await Access.CheckAccessToAll(request.Claims, this.repository, this.security, this.utility);

            //Verificar si el producto existe en la bd, si existe obtenerlo
            if (repository.Exists<Product>(x => x.CodigoBarras == request.BarCodeProductClone) is false)
                throw new Exception("El producto no existe");

            product = await repository.Get<Product>(x => x.CodigoBarras == request.BarCodeProductClone);

            //Recorrer lista de informacion de productos del request, clonar y modificar valores del producto clonado
            foreach (var item in request.ProductsDataRequest)
            {
                //clonar y cambiar atributos si se requiere
                cloneProduct = (Product)product.Clone();
                cloneProduct.ChangeAttributesClone(price: item.Price, categoryElementId: item.CategoryElementId);

                //Verificar si la categoria es nula, si es modificarla por la del producto a clonar
                if (item.CategoryElementId is null)
                    item.CategoryElementId = product.CategoryElementId;

                //obteniendo la categoria e incrementar los productos
                category = await repository.Get<Category>(x => x.Id == item.CategoryElementId);
                category.IncrementTotalProducts();

                //anadiendo el codigo de barras a la lista para luego retornarlos
                barcodes.Add(cloneProduct.CodigoBarras);

                //actualizar categoria y guardar el producto clonado
                repository.Update(category);
                await repository.Save<Product>(cloneProduct);
            }

            await repository.Commit();
            return barcodes;
        }
    }
}
