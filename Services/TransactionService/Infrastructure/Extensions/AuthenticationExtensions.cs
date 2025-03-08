using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TransactionService.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationExtensions(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var issuer = configuration["JwtSettings:Issuer"];
        var audience = configuration["JwtSettings:Audience"];
        var jwksUri = $"{issuer}/.well-known/jwks.json";

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.MetadataAddress = $"{issuer}/.well-known/openid-configuration";
                options.Audience = audience;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();
                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";
                        var problemDetails = new ProblemDetails
                        {
                            Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                            Status = 401,
                            Title = "Unauthorized",
                            Detail = "Token is missing or invalid.",
                            Instance = context.Request.Path,
                            Extensions = { ["traceId"] = context.HttpContext.TraceIdentifier }
                        };
                        var jsonResponse = JsonSerializer.Serialize(problemDetails);
                        await context.Response.WriteAsync(jsonResponse);
                    }
                };
            });

        return services;
    }
}