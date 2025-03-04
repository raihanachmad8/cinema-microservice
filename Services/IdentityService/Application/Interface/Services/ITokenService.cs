using System.Security.Claims;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;

namespace IdentityService.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<TokenResponse?> GenerateToken(User user);
        Task<TokenResponse?> RefreshToken(string refreshToken);
        
        Task<ClaimsPrincipal?> GetClaimsPrincipal(TokenType type, string token);
        Task<bool> RevokeTokenAsync(string accessToken);

    }
}