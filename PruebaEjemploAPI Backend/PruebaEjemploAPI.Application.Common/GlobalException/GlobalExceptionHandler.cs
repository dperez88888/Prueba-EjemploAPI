using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PruebaEjemploAPI.Transversal.Common;
using System.Net;
using System.Text.Json;

namespace PruebaEjemploAPI.Application.Common.GlobalException
{
    public class GlobalExceptionHandler : IMiddleware
    {

        private ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError(ex.Message);

                var response = new Response<Object>()
                {
                    Message = ex.Message,
                    IsSuccess = false
                };

                await JsonSerializer.SerializeAsync(context.Response.Body, response);

            }
        }
    }
}
