using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace PruebaEjemploAPI.Persistence.Extensions.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, string connectionStringBBDD, string connectionStringCache)
        {
            services.AddHealthChecks()
                .AddSqlServer(connectionStringBBDD, tags: new[] { "database" })
                .AddRedis(connectionStringCache, tags: new[] { "cache" });

            services.AddHealthChecksUI().AddInMemoryStorage();



            return services;
        }
    }
}
