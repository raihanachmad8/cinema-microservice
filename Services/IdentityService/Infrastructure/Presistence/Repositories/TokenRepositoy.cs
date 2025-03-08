using System.Text.Json;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Application.Services;
using IdentityService.Domain.Enums;
using StackExchange.Redis;

namespace IdentityService.Infrastructure.Persistence.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly IDatabase _database;
    private readonly ISerilog<TokenRepository> _logger;

    public TokenRepository(IConnectionMultiplexer redis, ISerilog<TokenRepository> logger)
    {
        _database = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<bool> AddAsync(string id, TokenType type, string token, TimeSpan expiresIn)
    {
        var key = $"{id}:{type}:{token}";
        var data = new TokenData
        {
            Id = id,
            Token = token,
            ExpiryDate = DateTime.Now.Add(expiresIn),
            IsRevoked = false
        };

        try
        {
            var jsonData = JsonSerializer.Serialize(data);
            _logger.LogInformation(
                $"Adding token to database: {id} with token type: {type}. Expiration: {DateTime.Now.Add(expiresIn)}.");
            var result = await _database.StringSetAsync(key, jsonData, expiresIn);


            if (result)
                _logger.LogInformation($"Token added successfully: {id} with token type: {type}.");
            else
                _logger.LogWarning($"Failed to add token: {id} with token type: {type}.");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while adding token to the database.");
            return false;
        }
    }

    public async Task<TokenData?> GetAsync(string id, TokenType type, string token)
    {
        try
        {
            var key = $"{id}:{type}:{token}";
            _logger.LogInformation($"Getting token from database: {id} with token type: {type}.");
            {
                var jsonData = await _database.StringGetAsync(key);
                if (jsonData.IsNull)
                {
                    _logger.LogWarning($"Token not found: {id} with token type: {type}.");
                    return null;
                }

                var tokenData = JsonSerializer.Deserialize<TokenData>(jsonData!);
                if (tokenData?.ExpiryDate < DateTime.Now)
                {
                    _logger.LogWarning($"Token expired: {id} with token type: {type}.");
                    return null;
                }

                _logger.LogInformation($"Token found and retrieved: {id} with token type: {type}.");
                return tokenData;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while retrieving token from the database.");
            return null;
        }
    }

    public async Task<bool> RemoveAsync(string id, TokenType type, string token)
    {
        try
        {
            var tokenData = await GetAsync(id, type, token);
            if (tokenData == null)
            {
                _logger.LogWarning($"Token to remove not found: {id} with token type: {type}.");
                return false;
            }

            var key = $"{id}:{type}:{token}";
            tokenData.IsRevoked = true;
            var jsonData = JsonSerializer.Serialize(tokenData);
            var result = await _database.StringSetAsync(key, jsonData, TimeSpan.FromDays(1));
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while removing token from the database.");
            return false;
        }
    }
}