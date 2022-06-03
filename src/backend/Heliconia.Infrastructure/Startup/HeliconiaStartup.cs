using Heliconia.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heliconia.Infrastructure.Startup
{
    public class HeliconiaStartup
    {
        public static void SetUp(IServiceCollection services, IConfiguration configuration)
        {
            ConfigureContext(services, configuration);
            ConfigureIOC(services);
            ConfigureMediador(services);
            ConfigureMapper(services);
        }

        private static void ConfigureContext(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HeliconiaContext>(options =>
            options.UseSqlServer(
                    configuration.GetConnectionString("HeliconiaConnectionString")));
        }

        private static void ConfigureIOC(IServiceCollection services)
        {
            InjectionContainer.Inyection(services);
        }

        private static void ConfigureMediador(IServiceCollection services)
        {
            MediatorContainer.Configure(services);
        }

        private static void ConfigureMapper(IServiceCollection services)
        {
            //mapeo de entidades
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
