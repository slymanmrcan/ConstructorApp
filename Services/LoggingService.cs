

namespace ConstructorApp.Services
{
    public class LoggingService(ILogger<LoggingService> logger) : ILoggingService
    {
        public void LogError(string message, Exception ex = null)
        {
            logger.LogInformation(message);
        }

        public void LogInfo(string message)
        {
            logger.LogWarning(message);
        }

        public void LogWarning(string message, Exception ex = null)
        {
            if (ex != null)
                logger.LogError(ex, message);
            else
                logger.LogError(message);
        }

        
    }
}