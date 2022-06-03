using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Domain
{
    public interface IMapObject
    {
        /// <summary>
        /// mapeo de objetos
        /// </summary>
        /// <typeparam name="TSourse"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="sourse"></param>
        /// <returns></returns>
        public TDestination Map<TSourse, TDestination>(TSourse sourse);
    }
}
