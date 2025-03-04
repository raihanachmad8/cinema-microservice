using System;
using StudioService.Application.Interfaces.Services;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.SystemConsole;
using Serilog.Sinks.File;
namespace StudioService.Infrastructure.Logging
{
    public class LoggerService<T> : ILoggerService<T>
    {
        public LoggerService()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{SourceContext}] [{Level}] : {Message}{NewLine}{Exception}"
                )
                .WriteTo.File(new JsonFormatter(), "logs/application-log.json", rollingInterval: RollingInterval.Day) // JSON format for the log file
                .CreateLogger();
        }

        public void LogInformation(string message)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
               .Information(message);
        }

        public void LogWarning(string message)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
               .Warning(message);
        }

        public void LogError(string message, Exception ex)
        {
            Log.ForContext("SourceContext", typeof(T).Name)
               .Error(ex, message);
        }
    }
}