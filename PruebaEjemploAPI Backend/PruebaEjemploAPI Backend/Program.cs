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

var builder = WebApplication.CreateBuilder(args);

// Añade servicios.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

var appSettingsSection = builder.Configuration.GetSection("ConfigToken");
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddDatabaseConf(appSettingsSection);
builder.Services.AddMappingServices();
builder.Services.AddMapper();
builder.Services.AddAuthenticationServices(appSettingsSection);
builder.Services.AddValidator();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
public partial class Program { } // esta clase se crea parcial para que se haga pública y pueda ser accesible desde la clase de test
