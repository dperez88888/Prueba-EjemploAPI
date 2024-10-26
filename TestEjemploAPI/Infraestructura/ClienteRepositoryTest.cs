using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using PruebaEjemploAPI_Backend.Infraestructura.Repository;
using PruebaEjemploAPI_Backend.Transversal.Extensions.DB;
using PruebaEjemploAPI_Backend.Transversal.Extensions.MappingServices;

namespace TestEjemploAPI.Infraestructura
{
    [TestClass]
    public class ClienteRepositoryTest
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _serviceScopeFactory;

        private static IEnumerable<object[]> GetClientesFalsos()
        {
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "",
                    ClienteId = 1,
                    Nombre = "",
                    CodigoPostal = "13500",
                    Direccion = "",
                    Email = "",
                    FechaNacimiento = new DateOnly(),
                    Pais = "",
                    Sexo = ""
                }
            };

            yield return new object[]
            {
                null
            };
        }

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Development");
                    builder.ConfigureAppConfiguration((app, conf) =>
                    {
                        _configuration = conf.Build();
                    });

                    builder.ConfigureServices(services =>
                    {
                        services = new ServiceCollection();
                        services.AddMappingServices();
                        var appSettingsSection = _configuration.GetSection("Config");
                        services.AddDatabaseConf(appSettingsSection);
                        _serviceScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
                    });

                });

            var client = application.CreateClient();
        }

        [TestMethod]
        [DynamicData(nameof(GetClientesFalsos), DynamicDataSourceType.Method)]
        public void AddCliente_ValorFalso(Cliente cli)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            var expected = false;
            

            // Act
            var result = context.AddCliente(cli);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}