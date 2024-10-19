using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services) 
        {
            // Añade Swagger para la documentación de la api
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Prueba Ejemplo API",
                    Description = "Parte backend de la prueba de Ejemplo API"
                });

                var secScheme = new OpenApiSecurityScheme
                {
                    Description = "Authorization by API key",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Name = "Authorization",
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition(secScheme.Reference.Id, secScheme);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    { secScheme, new List<string>()}
                });
            });

            return services;
        }
    }
}
