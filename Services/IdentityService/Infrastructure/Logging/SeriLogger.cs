using Serilog;
using IdentityService.Application.Interfaces.Services;
using System;

namespace IdentityService.Infrastructure.Logging
{
    public class SerilogLogger<T> : ISerilog<T>
    {
        // Constructor initializes the logger (Serilog)
        public SerilogLogger()
        {
            // Configure the global logger once.
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{SourceContext}] [{Level}] : {Message}{NewLine}{Exception}")
                .WriteTo.File("logs/application-log.json", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void LogInformation(string message, params object[] args)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
                .Information(message, args);
        }

        public void LogWarning(string message, params object[] args)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
                .Warning(message, args);
        }

        public void LogError(Exception exception, string message)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
                .Error(exception, message);
        }

        public void LogDebug(string message, params object[] args)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
                .Debug(message, args);
        }

        public void LogTrace(string message)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
                .Verbose(message); // Serilog does not have a `Trace` level, we use `Verbose` instead
        }
    }
}