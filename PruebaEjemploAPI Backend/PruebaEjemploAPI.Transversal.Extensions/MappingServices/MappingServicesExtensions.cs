using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Interface.UseCases;
using PruebaEjemploAPI.Application.UseCases;
using PruebaEjemploAPI.Persistence.Repository;
using PruebaEjemploAPI.Transversal.Common;
using PruebaEjemploAPI.Transversal.Common.Logging;

namespace PruebaEjemploAPI.Transversal.Extensions.MappingServices
{
    public static class MappingServicesExtensions
    {

        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
