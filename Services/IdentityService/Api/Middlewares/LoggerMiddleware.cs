using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityService.Application.Interfaces.Services;

namespace IdentityService.Api.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Resolve the ILoggerService from the service provider
            var loggerService = context.RequestServices.GetRequiredService<ILoggerService<LoggerMiddleware>>();
            
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            // Log request details
            loggerService.LogInformasi($"Incoming Request: {request.Method} {request.Path}");

            try
            {
                // Call the next middleware in the pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log any exceptions that happen during request processing
                loggerService.LogError("An error occurred during request processing.", ex);
                throw;
            }
            finally
            {
                // Log response details after the request has been processed
                var response = context.Response;
                stopwatch.Stop();

                loggerService.LogInformasi($"Outgoing Response: {response.StatusCode} (Request took {stopwatch.ElapsedMilliseconds} ms)");
            }
        }
    }
}