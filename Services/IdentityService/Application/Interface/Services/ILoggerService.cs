namespace IdentityService.Application.Interfaces.Services
{
    public interface ILoggerService<T>
    {
        void LogInformasi(string message);
        void LogWarning(string message);
        void LogError(string message, Exception ex);
    }
}
