using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

namespace PruebaEjemploAPI_Backend.Transversal.Extensions.Versioning
{
    public static class VersioningExtensions
    {

        public static IServiceCollection AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(v =>
            {
                v.DefaultApiVersion = new Asp.Versioning.ApiVersion(1, 0);
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.ReportApiVersions = true;
                v.ApiVersionReader = new HeaderApiVersionReader("x-api-version"); // Versionado por el encabezado
            })
            .AddApiExplorer(v =>
            {
                v.GroupNameFormat = "'v'VVV"; // version major + version minor + patch
            });

            return services;

        }
    }
}
