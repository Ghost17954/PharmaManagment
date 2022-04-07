using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PharmaManagment.Models;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PharmaManagment.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public CustomExceptionHandler(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("MyMiddleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                await HandleExceptionAsync(httpContext);

            }

        }

        public Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(new ErrorDetails
            {
                ErrorStatusCode = context.Response.StatusCode,
                Message = "Internal Server Error"
            }.ToString());
        }
    }

    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UserCustomExceptionHandler(this IApplicationBuilder builder, ILoggerFactory loggerFactory)
        {
            return builder.UseMiddleware<CustomExceptionHandler>(loggerFactory);
        }
    }
}
