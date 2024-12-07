using Microsoft.AspNetCore.Builder;
using PruebaEjemploAPI.Application.Common.GlobalException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaEjemploAPI.Application.Extensions.Middleware
{
    public static class MiddlewareExtensions
    {

        public static IApplicationBuilder AddCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandler>();
        }
    }
}
