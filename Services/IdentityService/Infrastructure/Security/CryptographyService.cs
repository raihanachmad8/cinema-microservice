using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using IdentityService.Application.Interfaces.Security;

namespace IdentityService.Infrastructure.Security;

public class CryptographyService : ICryptographyService
{
    public CryptographyService()
    {
    }

    public string Hash(string password)
    {
        return Argon2.Hash(password);
    }

    public bool Verify(string hashedPassword, string password)
    {
        return Argon2.Verify(hashedPassword, password);
    }

    public Task<string> RandomHashAsync(string input)
    {
        var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        foreach (var b in bytes) builder.Append(b.ToString("x2"));
        return Task.FromResult(builder.ToString());
    }
}