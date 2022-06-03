using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.CategoryEntities
{
    public class Estado : Entity
    {
        public DateTime Fecha { get; private set; }

        public EstadoEnum EstadoEnum { get; private set; }

        public Guid ProductId { get; private set; }

        private Estado():base()
        {

        }

        private Estado(DateTime fecha, EstadoEnum estadoEnum, Guid productId)
        {
            Fecha = fecha;
            EstadoEnum = estadoEnum;
            ProductId = productId;
        }

        public static Estado Build(DateTime fecha, EstadoEnum estadoEnum, Guid productId)
        {
            return new Estado(fecha, estadoEnum, productId);
        }
    }
}
