using Microsoft.AspNetCore.RateLimiting;
using PruebaEjemploAPI_Backend.Transversal.Settings;
using System.Text;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.RateLimiter
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfigurationSection confSection)
        {
            var permitLimit = int.Parse(confSection.Get<AppRateLimitingSettings>().PermitLimit);
            var window = int.Parse(confSection.Get<AppRateLimitingSettings>().Window);
            var queueLimit = int.Parse(confSection.Get<AppRateLimitingSettings>().QueueLimit);

            var fixedWindowPolicy = "fixedWindow";
            services.AddRateLimiter(configureOptions =>
            {
                configureOptions.AddFixedWindowLimiter(fixedWindowPolicy, conf =>
                {
                    conf.PermitLimit = permitLimit;
                    conf.Window = TimeSpan.FromSeconds(window);
                    conf.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    conf.QueueLimit = queueLimit;
                });
                // Más tipos de limitaciones de solicitudes
                //configureOptions.AddConcurrencyLimiter();
                //configureOptions.AddSlidingWindowLimiter();
                //configureOptions.AddTokenBucketLimiter();
                configureOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;

        }
    }
}
