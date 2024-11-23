using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaEjemploAPI.Persistence.Context;

namespace PruebaEjemploAPI.Persistence.Extensions.DB
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
