using DataManagement.DbContext;
using DataManagement.Entities;
using Dependency.Concept;
using Logging.Repositories;
using Logging.Services;
using Microsoft.EntityFrameworkCore;

// Log at boundaries. Catch, record, then rethrow with throw; when you cannot recover.

Console.WriteLine("=== Logging ===");

DependencyInjectionProvider.Register<DatabaseContext>();
DependencyInjectionProvider.Register<IExceptionLogRepository, ExceptionLogRepository>();
DependencyInjectionProvider.Register<ILogService, LogService>();

var logService = DependencyInjectionProvider.Resolve<ILogService>();

try
{
    await logService.Log(new Log
    {
        LogMessage = "Process started",
        Type = LogType.Info,
        Name = "Startup"
    });

    // Simulated failure at the app boundary.
    throw new InvalidOperationException("An error happened in the app");
}
catch (Exception ex)
{
    await logService.Log(new Log
    {
        LogMessage = ex.Message,
        Type = LogType.Error,
        Name = "Boundary"
    });

    Console.WriteLine($"Logged error: {ex.Message}");
}

var logs = await logService.GetLogs();
Console.WriteLine($"Stored log count: {logs.Count}");
foreach (var entry in logs)
{
    Console.WriteLine($"[{entry.Type}] {entry.LogMessage}");
}
