using Heliconia.Domain;
using Microsoft.Extensions.Configuration; //importante instalar la libreria Microsoft.Extensions.Configuration.Binder para el .get<list<string>>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Infrastructure.Utilities
{
    public class Utility : IUtility
    {
        private readonly IConfiguration configuration;

        public Utility(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool CheckValueInlistJson(string field, string value)
        {
            var heliconiasMails = configuration.GetSection(key: "heliconiasMailsJson").Get<List<string>>();

            return heliconiasMails.Exists(x => x == value);
        }

        public Guid CreateId()
        {
            return Guid.NewGuid();
        }

        public Guid CreateId(string id)
        {
            return Guid.Parse(id);
        }

        public string GetEnvironmentVariable(string name)
        {
            return this.configuration[name];
        }
    }
}
