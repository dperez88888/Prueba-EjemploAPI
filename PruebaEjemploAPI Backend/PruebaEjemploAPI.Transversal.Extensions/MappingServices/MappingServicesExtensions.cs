using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Interface.UseCases;
using PruebaEjemploAPI.Application.UseCases;
using PruebaEjemploAPI.Persistence.Repository;
using PruebaEjemploAPI.Transversal.Common;
using PruebaEjemploAPI.Transversal.Logging;

namespace PruebaEjemploAPI.Transversal.Extensions.MappingServices
{
    public static class MappingServicesExtensions
    {

        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IClienteAppService, ClienteAppService>(); // Servicio del cliente inyectado            
            services.AddScoped<IUsuarioAppService, UsuarioAppService>(); // Servicio del cliente inyectado
            services.AddScoped<IUnitOfWork, UnitOfWork>(); // Servicio del cliente inyectado

            return services;
        }
    }
}
