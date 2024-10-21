using PruebaEjemploAPI_Backend.Aplicacion.Validators;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.Validator
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
