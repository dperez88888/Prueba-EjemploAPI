using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestEjemploAPI.Helper
{
    public static class TestHelper
    {

        private const string _jsonMediaType = "application/json";
        private const int _expectedMaxElapsedMilliseconds = 1000;

        public static HttpClient GetClient()
        {
            var application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    //builder.ConfigureTestServices(services =>
                    //{

                    //});

                });

            return application.CreateClient();
        }
        
        public static async Task AssertResponseWithContentAsync<T>(Stopwatch stopwatch,
        HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode,
        T expectedContent)
        {
            AssertCommonResponseParts(stopwatch, response, expectedStatusCode);
            Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
            Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
                await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }));
        }

        private static void AssertCommonResponseParts(Stopwatch stopwatch,
            HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
        {
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
        }

        public static StringContent GetJsonStringContent<T>(T model)
            => new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);
    }
}
