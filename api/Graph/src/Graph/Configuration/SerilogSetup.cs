using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.SystemConsole.Themes;

namespace Spend.Graph.Configuration;

public static class SerilogSetup
{
    public static void SetupBootstrapLogger()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();
    }
    
    public static void ConfigureSerilog(HostBuilderContext context, IServiceProvider services,
        LoggerConfiguration loggerConfiguration)
    {
        loggerConfiguration
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Override("MongoDB.Connection", LogEventLevel.Information)
            .MinimumLevel.Override("MongoDB.SDAM", LogEventLevel.Information)
            .MinimumLevel.Override("MongoDB.ServerSelection", LogEventLevel.Information)
            ;

        if (!context.HostingEnvironment.IsProduction())
            loggerConfiguration
                .MinimumLevel.Verbose()
                .WriteTo.Console(outputTemplate:
                    "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz}] [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}",
#if DEBUG
                    theme: SystemConsoleTheme.Colored
#else
                    theme: SystemConsoleTheme.None
#endif
                )
                ;
    }
}