namespace IdentityService.Application.Interfaces.Security
{
    public interface ICryptographyService
    {
        string Hash(string input);
        bool Verify(string hashedInput, string rawInput);

        Task<string> RandomHashAsync(string input);
    }
}
