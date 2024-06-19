using PruebaExperticket_Backend.Services;
using Microsoft.EntityFrameworkCore;
using PruebaExperticket_Backend.Context;
using PruebaExperticket_Backend.Repository;

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
        Title = "Prueba Experticket",
        Description = "Parte backend de la prueba de Experticket"
    });
});

builder.Services.AddDbContext<ContextDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionAzure")));

builder.Services.AddTransient<IContextDB, ContextDB>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IClienteService, ClienteService>(); // Servicio del cliente inyectado

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
