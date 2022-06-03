using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.CategoryEntities
{
    public class Product : CategoryElement, ICloneable
    {
        public double Price { get; private set; }

        public List<Estado> Estados { get; private set; }

        public string CodigoBarras { get; private set; }

        public Guid CategoryElementId { get; private set; }

        public string NameLastState { get; private set; }

        /// <summary>
        /// El producto puede o no tener una compra
        /// </summary>
        public Guid? PurchaseId { get; private set; }

        /// <summary>
        /// for ef
        /// </summary>
        private Product() : base()
        {

        }

        private Product(double price, Guid categoryElementId) : base()
        {
            Price = price;
            CategoryElementId = categoryElementId;
            CodigoBarras = Convert.ToBase64String(this.Id.ToByteArray());

            this.Estados = new();
            this.AddEstado(DateTime.Now, EstadoEnum.registrado);
        }

        public static Product Build(double price, Guid categoryElementId)
        {
            return new Product(price, categoryElementId);
        }

        public void ChangeMainAttributes(double price)
        {
            Price = price;
        }

        public void changeCategoryElementId(Guid categoryElementId)
        {
            CategoryElementId = categoryElementId;
        }

        public void AddEstado(DateTime fecha, EstadoEnum estadoEnum)
        {
            var estado = Estado.Build(fecha, estadoEnum, this.Id);
            this.Estados.Add(estado);
        }

        public override void AddElement(CategoryElement element)
        {
            throw new NotImplementedException("No se puede añadir un producto a un producto");
        }

        public override void AddElements(IEnumerable<CategoryElement> Elements)
        {
            throw new NotImplementedException("No se puede añadir un producto a un producto");
        }

        public void ChangeLastNameState(string lastNameState)
        {
            NameLastState = lastNameState;
        }

        public object Clone()
        {
            Product product;

            try
            {
                product = (Product)this.MemberwiseClone();
            }
            catch (Exception ex)
            {
                throw new Exception($"Entidad no se pudo clonar {ex}");
            }

            return product;
        }

        public void ChangeAttributesClone(double? price = null, Guid? categoryElementId = null)
        {
            //genera el id de la clase
            this.GenerateNewId();

            //si el valor del precio es diferente a null asignar el valor del parametro si no asignar el que ya tiene
            Price = price != null ? price.Value: this.Price;

            //generar el codigo del barras
            CodigoBarras = Convert.ToBase64String(this.Id.ToByteArray());

            //si el valor de la categoria asociacion es diferente a null asignar el valor del parametro si no dejar la que ya tiene
            CategoryElementId = categoryElementId != null ? categoryElementId.Value : this.CategoryElementId;

            //un producto clonado no tiene compra
            PurchaseId = null;

            //Se agrega el estado de registrado al producto 
            this.Estados = new();
            this.AddEstado(DateTime.Now, EstadoEnum.registrado);
        }

    }
}
