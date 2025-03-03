using IdentityService.Application.Services;
using IdentityService.Domain.Enums;
namespace IdentityService.Application.Interfaces.Repositories;
public interface ITokenRepository
{
    Task<bool> AddAsync(string id, TokenType type, string token, TimeSpan expiresIn);
    Task<TokenData?> GetAsync(string id, TokenType type, string token);
    Task<bool> RemoveAsync(string id, TokenType type, string token);
}

