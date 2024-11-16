using Microsoft.EntityFrameworkCore.Storage;
using PruebaEjemploAPI_Backend.Transversal.Extensions.Redis;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, string connectionStringBBDD, string connectionStringCache)
        {
            services.AddHealthChecks()
                .AddSqlServer(connectionStringBBDD, tags: new[] {"database"})
                .AddRedis(connectionStringCache, tags: new[] { "cache" });

            services.AddHealthChecksUI().AddInMemoryStorage();
            
            

            return services;
        }
    }
}
