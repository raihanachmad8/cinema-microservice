using System.Security.Claims;
using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces.Security;

public interface IJwtService
{
    Task<string> GenerateTokenAsync(User user);
    Task<string> GenerateRefreshTokenAsync(User user);
    Task<ClaimsPrincipal?> ValidateTokenAsync(string token);
    Task<ClaimsPrincipal?> ValidateRefreshTokenAsync(string refreshToken);
}