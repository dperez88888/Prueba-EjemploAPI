using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaEjemploAPI.Transversal.Common.Settings;

namespace PruebaEjemploAPI.Application.Extensions.RateLimiter
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
