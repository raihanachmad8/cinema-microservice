namespace IdentityService.Application.Interfaces.Services;

public interface ILoggerService<T>
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(Exception ex, string message);
}