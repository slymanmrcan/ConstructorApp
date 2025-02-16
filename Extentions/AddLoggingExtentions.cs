using Serilog;
using Serilog.Core;
using Serilog.Sinks.SystemConsole.Themes;

namespace ConstructorApp.Extentions
{
    public static class AddLoggingExtentions
    {
        public static Logger Serilogger { get; } = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}


