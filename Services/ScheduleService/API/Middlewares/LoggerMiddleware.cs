using System.Diagnostics;
using ScheduleService.Application.Interfaces.Services;

namespace ScheduleService.API.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;

    public LoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Resolve the ISerilog from the service provider
        var loggerService = context.RequestServices.GetRequiredService<ISerilog<LoggerMiddleware>>();

        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;

        // Log request details
        loggerService.LogInformation($"Incoming Request: {request.Method} {request.Path}");

        try
        {
            // Call the next middleware in the pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log any exceptions that happen during request processing
            loggerService.LogError(ex, "An error occurred during request processing.");
            throw;
        }
        finally
        {
            // Log response details after the request has been processed
            var response = context.Response;
            stopwatch.Stop();

            loggerService.LogInformation(
                $"Outgoing Response: {response.StatusCode} (Request took {stopwatch.ElapsedMilliseconds} ms)");
        }
    }
}