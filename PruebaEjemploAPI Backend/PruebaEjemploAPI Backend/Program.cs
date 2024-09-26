using Microsoft.EntityFrameworkCore;
using PruebaEjemploAPI_Backend.Infraestructura.Context;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using AutoMapper;
using PruebaEjemploAPI_Backend.Transversal.Mapper;
using PruebaEjemploAPI_Backend.Dominio.Services;

var builder = WebApplication.CreateBuilder(args);

// Añade servicios.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Añade Swagger para la documentación de la api
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Prueba Ejemplo API",
        Description = "Parte backend de la prueba de Ejemplo API"
    });
});

builder.Services.AddDbContext<ContextDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionAzure")));

builder.Services.AddTransient<IContextDB, ContextDB>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IClienteAppService, ClienteAppService>(); // Servicio del cliente inyectado
builder.Services.AddScoped<IClienteDomService, ClienteDomService>(); // Servicio del cliente inyectado

var mapperConf = new MapperConfiguration(conf =>
{
    var profile = new ClienteEntityMapperProfile();
    conf.AllowNullCollections = true;
    conf.AddGlobalIgnore("Item");
    conf.AddProfile(profile);
});

var mapper = mapperConf.CreateMapper();
builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { } // esta clase se crea parcial para que se haga pública y pueda ser accesible desde la clase de test
