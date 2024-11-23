using Microsoft.Extensions.DependencyInjection;
using PruebaEjemploAPI.Application.Validators;

namespace PruebaEjemploAPI.Application.Extensions.Validator
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<UsuarioValidator>();
            services.AddTransient<ClienteValidator>();

            return services;
        }
    }
}
