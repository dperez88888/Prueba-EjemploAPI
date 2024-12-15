using Microsoft.Extensions.DependencyInjection;
using PruebaEjemploAPI.Application.Common.GlobalException;
using PruebaEjemploAPI.Application.Interface.UseCases;
using PruebaEjemploAPI.Application.UseCases;
using System.Reflection;

namespace PruebaEjemploAPI.Application.Extensions.Injection
{
    public static class InjectionExtensions
    {

        public static IServiceCollection AddApplicationInjection(this IServiceCollection services) 
        {
            //services.AddMediatR(conf =>
            //{
            //    conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            //});
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            }
            services.AddScoped<IClienteAppService, ClienteAppService>(); // Servicio del cliente inyectado            
            services.AddScoped<IUsuarioAppService, UsuarioAppService>(); // Servicio del cliente inyectado
            services.AddTransient<GlobalExceptionHandler>();

            return services;
        }
    }
}
