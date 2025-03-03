using System.Security.Cryptography;
using System.Text;
using Isopoh.Cryptography.Argon2;
using IdentityService.Application.Interfaces.Security;

namespace IdentityService.Infrastructure.Services
{
    public class CryptographyService : ICryptographyService
    {
        public CryptographyService() { }

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
            SHA256 sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return Task.FromResult(builder.ToString());
        }
    }
}
