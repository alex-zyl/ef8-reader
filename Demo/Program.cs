using System;
using Demo;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Logging;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) => configuration
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {SourceContext} [{Level}] {Message}{NewLine}{Exception}")
        .MinimumLevel.Verbose()
        .MinimumLevel.Override("Microsoft.EntityFrameworkCore.ChangeTracking", LogEventLevel.Warning)
        .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Warning));

    builder.Services.AddDbContextPool<DatabaseContext>(optionsBuilder =>
    {
        optionsBuilder
            .UseNpgsql(builder.Configuration["Database"])
            .UseLoggerFactory(new SerilogLoggerFactory(Log.Logger))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    });

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.MapGet("/", async (DatabaseContext db) =>
    {
        await db.Features.ToListAsync();

        await db.Substations.Include(x => x.VoltageMeasurement).ToListAsync();
    });

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during host run");
}
finally
{
    Log.CloseAndFlush();
}