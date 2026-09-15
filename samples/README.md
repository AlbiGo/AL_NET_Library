# Samples

Runnable demos for each best-practice topic. Read Prefer/Avoid, **Why this example**, and **Explaining …** in [`docs/`](../../docs/), then run the matching project.

```bash
dotnet run --project samples/Dependency
dotnet run --project samples/Threads
dotnet run --project samples/DesignPatterns
```

Smoke everything: `pwsh scripts/run-all.ps1`

| Folder | Topic | Key type to study |
| --- | --- | --- |
| `DesignPatterns` | Singleton, Factory, Builder, Strategy | `TaxCalculateContext`, `Shop`, `AnimalWorld` |
| `SOLID` | ISP / LSP | `PrintService`, `AreaCalculator` |
| `Dependency` | DI + Options (`appsettings.json`) | `EconomicsController`, `PricingService` |
| `Threads` | Async / concurrency + `lock` | `Kitchen`, `SharedCounterDemo` |
| `LINQ` | Filters, pagination, joins | `LamdaMethods` |
| `DataManagement` | Soft-delete, parameterized SQL | `BaseRepository`, `QueryBuilder` |
| `EntityFramework` | Detach / change tracking | Detach repository |
| `AuditEntry` | Change auditing | `AuditDbContext` |
| `Logging` | Boundary logging | `LogService` |
| `Exceptions` | Catch / rethrow | `throw;` demo |
| `AdvancedFeatures` | Delegates → reflection | `CarServices`, `TaskService`, `PluginScanner` |

Each sample README has **Why this example** and **Explaining …** for the main types.

Solution folders under **samples** group these the same way in Visual Studio.
