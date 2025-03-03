using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text.Json;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Infrastructure.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthenticationExtensions(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var privateKeyPath = configuration["JwtSettings:PrivateKeyPath"];
            var publicKeyPath = configuration["JwtSettings:PublicKeyPath"];
            var jwtIssuer = configuration["JwtSettings:Issuer"];
            var jwtAudience = configuration["JwtSettings:Audience"];
            var expiryMinutes = configuration.GetValue<int>("JwtSettings:ExpiryMinutes");
            var refreshTokenExpiryMinutes = configuration.GetValue<int>("JwtSettings:RefreshTokenExpiryMinutes");

            if (string.IsNullOrEmpty(publicKeyPath))

            {
                throw new ArgumentNullException(nameof(publicKeyPath), "Public key path must be provided in the configuration.");
            }
            var publicKey = LoadRsaPublicKey(publicKeyPath);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new RsaSecurityKey(publicKey),
                        ValidateIssuer = true,
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = jwtAudience,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            context.Response.ContentType = "application/json";
                            var problemDetails = new ProblemDetails
                            {
                                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1", 
                                Status = (int)HttpStatusCode.Unauthorized,
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

        private static RSA LoadRsaPublicKey(string publicKeyPath)
        {
            var keyContent = File.ReadAllText(publicKeyPath);
            var rsa = RSA.Create();
            rsa.ImportFromPem(keyContent);
            return rsa;
        }
    }
}