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

var builder = WebApplication.CreateBuilder(args);

// Añade servicios.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Añade Swagger para la documentación de la api
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Prueba Ejemplo API",
        Description = "Parte backend de la prueba de Ejemplo API"
    });

    c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
    {
        Description = "Authorization by API key",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Scheme = "Authorization"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authorization"
                },
                Scheme = "oauth2",
                Name = "Authorization",
                In = ParameterLocation.Header,

            },
            new List<string>()
        }
    });
});
var appSettingsSection = builder.Configuration.GetSection("ConfigToken");
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddDbContext<ContextDB>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionAzure")));

builder.Services.AddTransient<IContextDB, ContextDB>();
builder.Services.AddTransient<IClienteRepository, ClienteRepository>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteAppService, ClienteAppService>(); // Servicio del cliente inyectado
builder.Services.AddScoped<IClienteDomService, ClienteDomService>(); // Servicio del cliente inyectado
builder.Services.AddScoped<IUsuarioAppService, UsuarioAppService>(); // Servicio del cliente inyectado
builder.Services.AddScoped<IUsuarioDomService, UsuarioDomService>(); // Servicio del cliente inyectado

var mapperConf = new MapperConfiguration(conf =>
{
    var profile = new ClienteEntityMapperProfile();
    conf.AllowNullCollections = true;
    conf.AddGlobalIgnore("Item");
    conf.AddProfile(profile);
});

var mapper = mapperConf.CreateMapper();
builder.Services.AddSingleton(mapper);

var key = Encoding.ASCII.GetBytes(appSettingsSection.Get<AppSettings>().Secret);
var issuer = appSettingsSection.Get<AppSettings>().Issuer;
var audience = appSettingsSection.Get<AppSettings>().Audience;

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.Events = new JwtBearerEvents
        {
            OnTokenValidated = context =>
            {
                var userId = int.Parse(context.Principal.Identity.Name);
                return Task.CompletedTask;
            },

            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType().Equals(typeof(SecurityTokenExpiredException)))
                {
                    context.Response.Headers.Append("Token-Expired", "true");
                }
                return Task.CompletedTask;
            }
        };
        x.RequireHttpsMetadata = false;
        x.SaveToken = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });

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
