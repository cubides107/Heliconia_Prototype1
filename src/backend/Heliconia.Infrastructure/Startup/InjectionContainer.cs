using Heliconia.Domain;
using Heliconia.Domain.CompaniesEntities;
using Heliconia.Infrastructure.Mappings;
using Heliconia.Infrastructure.Repositories;
using Heliconia.Infrastructure.Securities;
using Heliconia.Infrastructure.Utilities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Infrastructure.Startup
{
    public class InjectionContainer
    {
        public static void Inyection(IServiceCollection services)
        {
            //repositorios
            services.AddScoped<IRepository, HeliconiaRepositorySQL>();
            services.AddScoped<IMapObject, MapObject>();
            services.AddScoped<ISecurity, Security>();
            services.AddScoped<IUtility, Utility>();
            services.AddScoped<IMediator, Mediator>();

        }
    }
}
