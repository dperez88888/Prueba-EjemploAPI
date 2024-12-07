using Microsoft.Extensions.DependencyInjection;
using PruebaEjemploAPI.Application.Interface.Persistence;
using PruebaEjemploAPI.Application.Interface.UseCases;
using PruebaEjemploAPI.Persistence.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEjemploAPI.Persistence.Extensions.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddPersistenceInjection(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
