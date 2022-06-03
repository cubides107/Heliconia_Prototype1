using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain.CategoryEntities
{
    public enum EstadoEnum
    {
        registrado = 0,
        vendido = 1,
        devueltoPorComprador = 2,
        devueltoPorCompania = 3,
        malEstado = 4
    }
}
