
using Microsoft.Extensions.Logging;
using WatchDog;

namespace PruebaEjemploAPI.Transversal.Common.Logging
{
    public class LoggerAdapter<T> : IAppLogger<T> where T : class
    {
        public readonly ILogger<T> _logger;

        public LoggerAdapter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<T>();
        }

        public void LogInfo(string message, params object[] args) 
        {
            _logger.LogInformation(message, args);
            WatchLogger.Log(message);
        }

        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
            WatchLogger.LogWarning(message);
        }

        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
            WatchLogger.LogError(message);
        }
    }
}
