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
using System.Globalization;
using System.Runtime.CompilerServices;

namespace TestEjemploAPI.Infraestructura
{
    [TestClass]
    public class ClienteRepositoryTest
    {
        private static IConfiguration _configuration;
        private static IServiceScopeFactory _serviceScopeFactory;

        private static IEnumerable<object[]> GetAddClientesExcepcion()
        {
            yield return new object[]
            {
                // Vacio
                new Cliente()
                {
                    Apellidos = null,
                    Nombre = null,
                    CodigoPostal = null,
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = null,
                    Sexo = null
                }
            };

            // Sin nombre
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = null,
                    CodigoPostal = "13500",
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = null
                }
            };

            // Sin apellidos
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = null,
                    Nombre = "pepe",
                    CodigoPostal = "13500",
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = null
                }
            };

            // sin sexo
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "dasd",
                    CodigoPostal = "13500",
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = null
                }
            };

            // sin pais
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "dasd",
                    CodigoPostal = "13500",
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = null,
                    Sexo = "M"
                }
            };

            // longitud de nombre errónea
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "dasdaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    CodigoPostal = "13500",
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = "M"
                }
            };

            // longitud de apellido errónea
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepeaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    Nombre = "dasd",
                    CodigoPostal = "13500",
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = "M"
                }
            };

            // longitud de sexo errónea
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "dasd",
                    CodigoPostal = "13500",
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = null,
                    Sexo = "XXXXX"
                }
            };

            // Longitud de Dirección errónea
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "fsdff",
                    CodigoPostal = "13500",
                    Direccion = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = "M"
                }
            };

            // Longitud de Código postal errónea
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "fsdff",
                    CodigoPostal = "13500000",
                    Direccion = "aaaaaaaaaaa",
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = "M"
                }
            };

            // Longitud de email erróneo
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "fsdf",
                    CodigoPostal = "34530",
                    Direccion = "xxxxx",
                    Email = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = "M"
                }
            };            
        }

        private static IEnumerable<object[]> GetAddClientesFalsos()
        {
            // nulo
            yield return new object[]
            {
                null
            };
        }

        private static IEnumerable<object[]> GetAddClientesVerdaderos()
        {
            // Solo Obligatorios rellenos
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "dasdaaaaaaaaaa",
                    CodigoPostal = null,
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = "M"
                }
            };

            // Solo Obligatorios vacíos
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "",
                    Nombre = "",
                    CodigoPostal = null,
                    Direccion = null,
                    Email = null,
                    FechaNacimiento = new DateOnly(),
                    Pais = "",
                    Sexo = ""
                }
            };

            // Con todos los datos
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepe",
                    Nombre = "dasdaaaaaaaaaa",
                    CodigoPostal = "13500",
                    Direccion = "Calle falsa",
                    Email = "pepe@fff.com",
                    FechaNacimiento = new DateOnly(),
                    Pais = "España",
                    Sexo = "M"
                }
            };

            // Con todos los datos y fecha diferente a hoy
            yield return new object[]
            {
                new Cliente()
                {
                    Apellidos = "pepiño",
                    Nombre = "dasddsaaaaaaa",
                    CodigoPostal = "13500",
                    Direccion = "Calle falsaXXX",
                    Email = "pepe@fff.com",
                    FechaNacimiento = new DateOnly(2021, 8, 6),
                    Pais = "España",
                    Sexo = "M"
                }
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
                        services.AddDatabaseConf(_configuration.GetConnectionString("DefaultConnectionAzure"));
                        _serviceScopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
                    });

                });

            var client = application.CreateClient();
        }

        [TestMethod]
        [DynamicData(nameof(GetAddClientesFalsos), DynamicDataSourceType.Method)]
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

        [TestMethod]
        [DynamicData(nameof(GetAddClientesVerdaderos), DynamicDataSourceType.Method)]
        public void AddCliente_ValorVerdadero(Cliente cli)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            var expected = true;

            // Act
            var result = context.AddCliente(cli);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DynamicData(nameof(GetAddClientesExcepcion), DynamicDataSourceType.Method)]        
        public void AddCliente_ValorAggregateExcepcion(Cliente cli)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            // Act            

            //Assert
            var ex = Assert.ThrowsException<AggregateException>(() =>
            {
                var result = context?.AddCliente(cli);
            });

            Assert.AreEqual(ex.InnerException?.Message, "An error occurred while saving the entity changes. See the inner exception for details.");
        }

        [TestMethod]        
        public void GetClientes_ValoresCorrectos()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            var expected = -1;

            // Act
            var result = context.GetClientes().Count;

            //Assert
            Assert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void GetCliente_ValoresCorrectos()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            Cliente expected = context.GetClientes().Last();

            // Act
            var result = context.GetCliente(context.GetClientes().Last().ClienteId);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(-123)]
        [DataRow(0)]
        public void GetCliente_ValoresFalsos(int clienteId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            Cliente expected = null;

            // Act
            var result = context.GetCliente(clienteId);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]        
        public void DeleteCliente_ValoresCorrectos()
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            var expected = true;

            // Act
            var result = context.DeleteCliente(context.GetClientes().Last().ClienteId);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(-123)]
        [DataRow(0)]
        public void DeleteCliente_ValoresFalsos(int clienteId)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IClienteRepository>();

            // Arrange

            var expected = false;

            // Act
            var result = context.DeleteCliente(clienteId);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}