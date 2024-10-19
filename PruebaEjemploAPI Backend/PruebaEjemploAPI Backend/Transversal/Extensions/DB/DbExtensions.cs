using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Context;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.DB
{
    public static class DbExtensions
    {
        public static IServiceCollection AddDatabaseConf(this IServiceCollection services, IConfigurationSection conf)
        {
            services.AddDbContext<ContextDB>(options => options.UseSqlServer(conf.GetConnectionString("DefaultConnectionAzure")));
            services.AddTransient<IContextDB, ContextDB>();

            return services;
        }
    }
}
