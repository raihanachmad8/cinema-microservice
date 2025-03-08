using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using IdentityService.Application.Interfaces.Security;
using IdentityService.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.Infrastructure.Security;

public sealed class JwtService : IJwtService
{
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expiryMinutes;
    private readonly int _expirationRefreshMinutes;
    private readonly Lazy<Task<RSA>> _privateKey;
    private readonly Lazy<Task<RSA>> _publicKey;

    public JwtService(IConfiguration configuration)
    {
        _issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException(nameof(_issuer));
        _audience = configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException(nameof(_audience));
        _expiryMinutes = Convert.ToInt16(configuration["JwtSettings:ExpiryMinutes"] ?? "60");
        _expirationRefreshMinutes = Convert.ToInt16(configuration["JwtSettings:ExpiryDay"] ?? "1") * 1440;


        var privateKeyPath = configuration["JwtSettings:PrivateKeyPath"] ??
                             throw new ArgumentNullException("JwtSettings:PrivateKeyPath");
        var publicKeyPath = configuration["JwtSettings:PublicKeyPath"] ??
                            throw new ArgumentNullException("JwtSettings:PublicKeyPath");

        _privateKey = new Lazy<Task<RSA>>(() => LoadRsaKeyAsync(privateKeyPath));
        _publicKey = new Lazy<Task<RSA>>(() => LoadRsaKeyAsync(publicKeyPath));
    }

    public async Task<string> GenerateTokenAsync(User user)
    {
        var privateKey = await _privateKey.Value;
        var credentials = new SigningCredentials(new RsaSecurityKey(privateKey), SecurityAlgorithms.RsaSha256);
        var claims = ExtractClaims(user);

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_expiryMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> GenerateRefreshTokenAsync(User user)
    {
        var privateKey = await _privateKey.Value;
        var credentials = new SigningCredentials(new RsaSecurityKey(privateKey), SecurityAlgorithms.RsaSha256);
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var refreshToken = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationRefreshMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(refreshToken);
    }

    public async Task<ClaimsPrincipal?> ValidateTokenAsync(string token)
    {
        return await ValidateTokenInternalAsync(token, _publicKey.Value);
    }

    public async Task<ClaimsPrincipal?> ValidateRefreshTokenAsync(string refreshToken)
    {
        return await ValidateTokenInternalAsync(refreshToken, _publicKey.Value);
    }

    private async Task<ClaimsPrincipal?> ValidateTokenInternalAsync(string token, Task<RSA> rsaKeyTask)
    {
        try
        {
            var publicKey = await rsaKeyTask;
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new RsaSecurityKey(publicKey),
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidateAudience = true,
                ValidAudience = _audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }

    private static List<Claim> ExtractClaims(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        return claims;
    }

    private static async Task<RSA> LoadRsaKeyAsync(string? keyPath)
    {
        if (string.IsNullOrWhiteSpace(keyPath))
            throw new ArgumentNullException(nameof(keyPath), "RSA key path cannot be null or empty.");

        if (!File.Exists(keyPath)) throw new FileNotFoundException($"RSA key file not found at: {keyPath}");

        var keyContent = await File.ReadAllTextAsync(keyPath);
        var rsa = RSA.Create();
        rsa.ImportFromPem(keyContent);
        return rsa;
    }
}