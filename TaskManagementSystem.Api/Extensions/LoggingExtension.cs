using Serilog;

namespace TaskManagementSystem.Api.Extensions;

public static class LoggingExtensions
{
    public static void AddSerilogLogging(this WebApplicationBuilder builder)  // ✅ WebApplicationBuilder
    {
        EnsureLogDirectoriesExist();

        builder.Host.UseSerilog((context, services, config) =>   // ✅ builder.Host.UseSerilog
            config.ReadFrom.Configuration(context.Configuration)
                  .ReadFrom.Services(services)
                  .Enrich.FromLogContext());
    }

    private static void EnsureLogDirectoriesExist()
    {
        var directories = new[]
        {
            "wwwroot/logs/info",
            "wwwroot/logs/errors",
            "wwwroot/logs/debug"
        };

        foreach (var dir in directories)
        {
            Directory.CreateDirectory(dir);
        }
    }
}