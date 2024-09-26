using Azure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using PruebaEjemploAPI_Backend.Infraestructura.Model;
using System.Diagnostics;
using System.Net.Http.Json;
using TestEjemploAPI.Helper;

namespace TestEjemploAPI
{
    public class TestEjemploAPI
    {
        [Fact]
        public async void Test1()
        {            
            
            var client = TestHelper.GetClient();

            // Arrange
            var stopwatch = Stopwatch.StartNew();

            var expectedStatusCode = System.Net.HttpStatusCode.OK;
            var expectedContent = new Cliente()
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
            };

            // Act
            var result = await client.GetAsync("/PruebaEjemploAPI/Clientes/1");

            //Assert
            await TestHelper.AssertResponseWithContentAsync(stopwatch, result, expectedStatusCode, expectedContent);
        }
    }
}