using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IdentityService.Application.Interfaces.Services;

namespace IdentityService.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdentityDBContext _context;
    private readonly ISerilog<UserRepository> _logger;

    public UserRepository(IdentityDBContext context, ISerilog<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }
}