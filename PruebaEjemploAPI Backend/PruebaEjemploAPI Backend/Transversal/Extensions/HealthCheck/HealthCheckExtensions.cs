using Microsoft.EntityFrameworkCore.Storage;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, string connectionString)
        {
            services.AddHealthChecks().AddSqlServer(connectionString, tags: new[] {"database"});
            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }
    }
}
