using Microsoft.Identity.Client;
using PruebaEjemploAPI_Backend.Dominio.Services;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using PruebaEjemploAPI_Backend.Transversal.Common;
using PruebaEjemploAPI_Backend.Transversal.Logging;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.MappingServices
{
    public static class MappingServicesExtensions
    {

        public static IServiceCollection AddMappingServices(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<IClienteAppService, ClienteAppService>(); // Servicio del cliente inyectado
            services.AddScoped<IClienteDomService, ClienteDomService>(); // Servicio del cliente inyectado
            services.AddScoped<IUsuarioAppService, UsuarioAppService>(); // Servicio del cliente inyectado
            services.AddScoped<IUsuarioDomService, UsuarioDomService>(); // Servicio del cliente inyectado

            return services;
        }
    }
}
