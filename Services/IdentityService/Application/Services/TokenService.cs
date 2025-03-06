using System.Security.Claims;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Domain.Enums;
using StackExchange.Redis;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Security;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Services;

public class TokenService : ITokenService
{
    private readonly ITokenRepository _tokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IJwtService _jwtService;
    private readonly ISerilog<TokenService> _logger;

    public TokenService(
        IConfiguration configuration,
        IJwtService jwtService,
        IUserRepository userRepository,
        ITokenRepository tokenRepository,
        ISerilog<TokenService> logger)
    {
        _tokenRepository = tokenRepository;
        _configuration = configuration;
        _jwtService = jwtService;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<TokenResponse?> GenerateToken(User user)
    {
        _logger.LogInformation($"Generating token for user: {user.Id}");

        var access = await _jwtService.GenerateTokenAsync(user);
        var refresh = await _jwtService.GenerateRefreshTokenAsync(user);

        var accessTokenExpiryIn = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes");
        var refreshExpiryIn = _configuration.GetValue<int>("JwtSettings:ExpiryDay");

        _logger.LogInformation(
            $"Saving access token for user {user.Id} to Redis with expiry of {accessTokenExpiryIn} minutes.");
        await _tokenRepository.AddAsync(user.Id.ToString(), TokenType.Access, access,
            TimeSpan.FromMinutes(accessTokenExpiryIn));

        _logger.LogInformation(
            $"Saving refresh token for user {user.Id} to Redis with expiry of {refreshExpiryIn} days.");
        await _tokenRepository.AddAsync(user.Id.ToString(), TokenType.Refresh, refresh,
            TimeSpan.FromDays(refreshExpiryIn));

        return new TokenResponse
        {
            AccessToken = access,
            RefreshToken = refresh,
            ExpiresIn = refreshExpiryIn
        };
    }

    public async Task<TokenResponse?> RefreshToken(string refreshToken)
    {
        _logger.LogInformation($"Refreshing token with provided refresh token: {refreshToken}");

        var principal = await _jwtService.ValidateRefreshTokenAsync(refreshToken);

        if (principal == null) return null;

        var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id == null)
        {
            _logger.LogWarning("Invalid refresh token payload.");
            throw new UnauthorizedAccessException("Invalid refresh token payload.");
        }

        _logger.LogInformation($"Found user ID {id} for refresh token.");

        var tokenData = await _tokenRepository.GetAsync(id, TokenType.Refresh, refreshToken);

        ValidateToken(tokenData!);

        var user = await _userRepository.GetByIdAsync(int.Parse(id));
        if (user == null)
        {
            _logger.LogWarning("Invalid refresh token payload.");
            throw new UnauthorizedAccessException("Invalid refresh token payload.");
        }

        var access = await _jwtService.GenerateTokenAsync(user);
        var refresh = await _jwtService.GenerateRefreshTokenAsync(user);

        var accessTokenExpiryIn = _configuration.GetValue<int>("JwtSettings:ExpiryMinutes");
        var refreshExpiryIn = _configuration.GetValue<int>("JwtSettings:ExpiryDay");

        _logger.LogInformation($"Saving new access token for user {user.Id} to Redis.");
        await _tokenRepository.AddAsync(user.Id.ToString(), TokenType.Access, access,
            TimeSpan.FromMinutes(accessTokenExpiryIn));

        _logger.LogInformation($"Saving new refresh token for user {user.Id} to Redis.");
        await _tokenRepository.AddAsync(user.Id.ToString(), TokenType.Refresh, refresh,
            TimeSpan.FromMinutes(refreshExpiryIn));

        return new TokenResponse
        {
            AccessToken = access,
            RefreshToken = refresh,
            ExpiresIn = refreshExpiryIn
        };
    }

    public async Task<ClaimsPrincipal?> GetClaimsPrincipal(TokenType type, string token)
    {
        _logger.LogInformation($"Getting claims principal for token type {type} and token: {token}");

        return type == TokenType.Access
            ? await _jwtService.ValidateTokenAsync(token)
            : await _jwtService.ValidateRefreshTokenAsync(token);
    }

    private void ValidateToken(TokenData tokenData)
    {
        if (tokenData == null)
        {
            _logger.LogWarning("Token is invalid.");
            throw new BadHttpRequestException("Token is invalid.");
        }

        if (tokenData.ExpiryDate < DateTime.UtcNow)
        {
            _logger.LogWarning("Token has expired.");
            throw new BadHttpRequestException("Token has expired.");
        }

        if (tokenData.IsRevoked)
        {
            _logger.LogWarning("Token is revoked.");
            throw new BadHttpRequestException("Token is revoked.");
        }
    }

    public async Task<bool> RevokeTokenAsync(TokenType type, string token)
    {
        _logger.LogInformation($"Processing logout request with token: {token}");

        var principal = type == TokenType.Access
            ? await _jwtService.ValidateTokenAsync(token)
            : await _jwtService.ValidateRefreshTokenAsync(token);
        if (principal == null)
        {
            _logger.LogWarning("Logout failed: Invalid access token.");
            throw new UnauthorizedAccessException("Invalid access token.");
        }

        var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (id == null)
        {
            _logger.LogWarning("Logout failed: Invalid access token payload.");
            throw new UnauthorizedAccessException("Invalid access token payload.");
        }


        var tokenData = await _tokenRepository.GetAsync(id, type, token);
        if (tokenData == null)
        {
            _logger.LogWarning($"Token to remove not found: {id} with token type: {TokenType.Access}.");
            return false;
        }

        return await _tokenRepository.RemoveAsync(id, TokenType.Access, token);
    }
}