using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Context;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.DB
{
    public static class DbExtensions
    {
        public static IServiceCollection AddDatabaseConf(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ContextDB>(options => options.UseSqlServer(connectionString));
            services.AddTransient<IContextDB, ContextDB>();

            return services;
        }
    }
}
