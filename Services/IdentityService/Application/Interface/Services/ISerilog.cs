using System;

namespace IdentityService.Application.Interfaces.Services
{
    public interface ISerilog<T>
    {
        void LogInformation(string message, params object[] args);
        void LogWarning(string message, params object[] args);
        void LogError(Exception exception, string message);
        void LogDebug(string message, params object[] args);
        void LogTrace(string message);
    }
}