using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain
{
    public interface IUtility
    {
        public const string CLAVE_SECRETA = "CLAVE_SECRETA";

        public const string DOMINIO_APP = "DOMINIO_APP";

        public const string DIAS_EXPIRACION = "DIAS_EXPIRACION";

        /// <summary>
        /// verifica so el valor esta en el campo de arreglos del json
        /// </summary>
        /// <returns></returns>
        public bool CheckValueInlistJson(string field, string value);

        public string GetEnvironmentVariable(string name);

        public Guid CreateId();

        public Guid CreateId(string id);
    }
}
