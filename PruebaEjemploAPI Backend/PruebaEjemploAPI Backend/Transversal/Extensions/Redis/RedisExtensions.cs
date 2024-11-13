namespace PruebaEjemploAPI_Backend.Transversal.Extensions.Redis
{
    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, string connectionString)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = connectionString;
            });
            return services;
        }
    }
}
