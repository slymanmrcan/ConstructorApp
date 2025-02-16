namespace ConstructorApp.Services
{
    public interface ILoggingService
    {
        void LogInfo(string message);
        void LogWarning(string message, Exception ex = null);
        void LogError(string message, Exception ex = null);
    }
}