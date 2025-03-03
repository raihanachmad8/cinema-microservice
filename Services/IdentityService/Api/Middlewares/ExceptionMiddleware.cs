using System.Net;
using IdentityService.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IWebHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            await HandleResponseStatusCodeAsync(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleResponseStatusCodeAsync(HttpContext context)
    {
        switch (context.Response.StatusCode)
        {
            case (int)HttpStatusCode.NotFound:
                await HandleResponseAsync(context, HttpStatusCode.NotFound, "Resource Not Found", "The requested resource was not found.");
                break;
            case (int)HttpStatusCode.Forbidden:
                await HandleResponseAsync(context, HttpStatusCode.Forbidden, "Forbidden", "You do not have permission to access this resource.");
                break;
            case (int)HttpStatusCode.MethodNotAllowed:
                await HandleResponseAsync(context, HttpStatusCode.MethodNotAllowed, "Method Not Allowed", "This HTTP method is not allowed for the requested resource.");
                break;
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = ex switch
        {
            ArgumentException => HttpStatusCode.BadRequest,
            InvalidOperationException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            ConflictException => HttpStatusCode.Conflict,
            ForbiddenException => HttpStatusCode.Forbidden,
            BadHttpRequestException => HttpStatusCode.BadRequest,
            KeyNotFoundException => HttpStatusCode.NotFound,
            FormatException => HttpStatusCode.BadRequest,
            NullReferenceException => HttpStatusCode.InternalServerError,
            NotImplementedException => HttpStatusCode.NotImplemented,
            _ => HttpStatusCode.InternalServerError
        };

        var title = statusCode switch
        {
            HttpStatusCode.BadRequest => "Invalid argument or format.",
            HttpStatusCode.Unauthorized => "Unauthorized access.",
            HttpStatusCode.Conflict => "Conflict.",
            HttpStatusCode.Forbidden => "Forbidden.",
            HttpStatusCode.NotFound => "Resource not found.",
            HttpStatusCode.InternalServerError => "Internal server error.",
            HttpStatusCode.NotImplemented => "Not implemented.",
            _ => "An error occurred while processing your request."
        };

        return HandleResponseAsync(context, statusCode, title, ex.Message);
    }

    private Task HandleResponseAsync(HttpContext context, HttpStatusCode statusCode, string title, string detail)
    {
        _logger.LogWarning($"{title}: {context.Request.Path}");

        var problemDetails = new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
            Title = title,
            Status = (int)statusCode,
            Detail = detail,
            Instance = context.Request.Path,
            Extensions = { ["traceId"] = context.TraceIdentifier }
        };

        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsJsonAsync(problemDetails);
    }
}