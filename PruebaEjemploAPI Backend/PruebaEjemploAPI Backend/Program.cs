using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI.Transversal.Extensions.MappingServices;
using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using WatchDog;
using PruebaEjemploAPI.Application.Extensions.Versioning;
using PruebaEjemploAPI.Application.Extensions.Swagger;
using PruebaEjemploAPI.Persistence.Extensions.DB;
using PruebaEjemploAPI.Application.Extensions.Mapper;
using PruebaEjemploAPI.Service.Extensions.Authentication;
using PruebaEjemploAPI.Application.Extensions.Validator;
using PruebaEjemploAPI.Persistence.Extensions.HealthCheck;
using PruebaEjemploAPI.Persistence.Extensions.WatchDogX;
using PruebaEjemploAPI.Persistence.Extensions.Redis;
using PruebaEjemploAPI.Application.Extensions.RateLimiter;
using PruebaEjemploAPI.Transversal.Common.Settings;

var builder = WebApplication.CreateBuilder(args);

// Añade servicios.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersioning();
builder.Services.AddSwagger();

builder.Services.AddDatabaseConf(builder.Configuration.GetConnectionString("DefaultConnectionAzure"));
builder.Services.AddMappingServices();
builder.Services.AddMapper();

var appSettingsTokenSection = builder.Configuration.GetSection("ConfigToken");
builder.Services.Configure<AppTokenSettings>(appSettingsTokenSection);
builder.Services.AddAuthenticationServices(appSettingsTokenSection);

builder.Services.AddValidator();
builder.Services.AddHealthCheck(builder.Configuration.GetConnectionString("DefaultConnectionAzure"), builder.Configuration.GetConnectionString("RedisConnectionAzure"));
builder.Services.AddWatchDog(builder.Configuration.GetConnectionString("DefaultWatchDogConnectionAzure"));
builder.Services.AddRedisCache(builder.Configuration.GetConnectionString("RedisConnectionAzure"));

var appSettingsRateLimiting = builder.Configuration.GetSection("RateLimiting");
builder.Services.Configure<AppRateLimitingSettings>(appSettingsRateLimiting);
builder.Services.AddRateLimiting(appSettingsRateLimiting);

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba Ejemplo API");
    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
    }
});

app.UseWatchDogExceptionLogger();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseRateLimiter();
app.UseEndpoints(_ => { });

app.MapControllers();

app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
var appSettingsWatchDogSection = builder.Configuration.GetSection("WatchDog");

app.UseWatchDog(c =>
{
    c.WatchPageUsername = appSettingsWatchDogSection.GetValue<string>("WatchPageUsername");
    c.WatchPagePassword = appSettingsWatchDogSection.GetValue<string>("WatchPagePassword");
});

app.Run();
public partial class Program { } // esta clase se crea parcial para que se haga pública y pueda ser accesible desde la clase de test
