using IdentityService.Domain.Entities;

namespace IdentityService.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByNameAsync(string name);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> AddAsync(User user);
    Task<User?> UpdateAsync(User user);
    Task<User?> DeleteAsync(User user);
}