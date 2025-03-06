using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text.Json;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route(".well-known")]
    public class OpenIdJwksController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OpenIdJwksController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// OpenID Connect Configuration Metadata
        /// </summary>
        [HttpGet("openid-configuration")]
        public IActionResult GetOpenIdConfiguration()
        {
            var issuer = _configuration["JwtSettings:Issuer"];

            var openIdConfig = new
            {
                issuer,
                jwks_uri = $"{issuer}/.well-known/jwks.json",
                id_token_signing_alg_values_supported = new[] { "RS256" },
                token_endpoint = $"{issuer}/connect/token",
                userinfo_endpoint = $"{issuer}/connect/userinfo",
                authorization_endpoint = $"{issuer}/connect/authorize",
                response_types_supported = new[] { "code", "id_token", "token id_token" },
                subject_types_supported = new[] { "public" },
                grant_types_supported = new[] { "authorization_code", "client_credentials", "refresh_token" }
            };

            return Ok(openIdConfig);
        }

        /// <summary>
        /// JSON Web Key Set (JWKS) untuk validasi JWT
        /// </summary>
        [HttpGet("jwks.json")]
        public IActionResult GetJwks()
        {
            var publicKeyPath = _configuration["JwtSettings:PublicKeyPath"];

            if (string.IsNullOrEmpty(publicKeyPath) || !System.IO.File.Exists(publicKeyPath))
            {
                return BadRequest(new { error = "Public key not found." });
            }

            var rsa = LoadRsaPublicKey(publicKeyPath);
            var rsaParameters = rsa.ExportParameters(false); // Hanya mengambil kunci publik

            var jwk = new
            {
                keys = new[]
                {
                    new
                    {
                        kty = "RSA",
                        use = "sig",
                        alg = "RS256",
                        kid = "default-key",
                        n = Base64UrlEncode(rsaParameters.Modulus),
                        e = Base64UrlEncode(rsaParameters.Exponent)
                    }
                }
            };

            var json = JsonSerializer.Serialize(jwk, new JsonSerializerOptions { WriteIndented = true });
            return Content(json, "application/json");
        }

        /// <summary>
        /// Memuat RSA Public Key dari file
        /// </summary>
        private static RSA LoadRsaPublicKey(string publicKeyPath)
        {
            var keyContent = System.IO.File.ReadAllText(publicKeyPath);
            var rsa = RSA.Create();
            rsa.ImportFromPem(keyContent);
            return rsa;
        }

        /// <summary>
        /// Mengonversi byte array ke Base64 URL Safe string
        /// </summary>
        private static string Base64UrlEncode(byte[] input)
        {
            return Convert.ToBase64String(input)
                .TrimEnd('=')
                .Replace('+', '-')
                .Replace('/', '_');
        }
    }
}
