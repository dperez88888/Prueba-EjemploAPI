using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Context;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using AutoMapper;
using PruebaEjemploAPI_Backend.Transversal.Mapper;
using PruebaEjemploAPI_Backend.Dominio.Services;
using PruebaEjemploAPI_Backend.Transversal.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using PruebaEjemploAPI_Backend.Transversal.Extensions.Swagger;
using PruebaEjemploAPI_Backend.Transversal.Extensions.DB;
using PruebaEjemploAPI_Backend.Transversal.Extensions.MappingServices;
using PruebaEjemploAPI_Backend.Transversal.Extensions.Mapper;
using PruebaEjemploAPI_Backend.Transversal.Extensions.Authentication;
using PruebaEjemploAPI_Backend.Transversal.Extensions.Validator;
using PruebaEjemploAPI_Backend.Transversal.Extensions.Versioning;
using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using PruebaEjemploAPI_Backend.Transversal.Extensions.HealthCheck;
using PruebaEjemploAPI_Backend.Transversal.Extensions.WatchDogX;
using WatchDog;
using PruebaEjemploAPI_Backend.Transversal.Extensions.Redis;
using PruebaEjemploAPI_Backend.Transversal.Extensions.RateLimiter;

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
