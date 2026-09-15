# Samples

Runnable demos for each best-practice topic. Read Prefer/Avoid, **Why this example**, and **Explaining …** in [`docs/`](../../docs/) and each sample README, then run the matching project.

```bash
dotnet run --project samples/DesignPatterns
dotnet run --project samples/Dependency
dotnet run --project samples/Threads
```

| Folder | Topic | Key type to study |
| --- | --- | --- |
| `DesignPatterns` | Singleton, Factory, Builder, Strategy | `TaxCalculateContext`, `Shop`, `AnimalWorld` |
| `SOLID` | ISP / LSP | `PrintService`, `AreaCalculator` |
| `Dependency` | Composition root & ctor injection | `EconomicsController` |
| `Threads` | Async / concurrency | `Kitchen` / `KitchenAsync` |
| `LINQ` | Filters, pagination, joins | `LamdaMethods` |
| `DataManagement` | Soft-delete, parameterized SQL | `BaseRepository`, `QueryBuilder` |
| `EntityFramework` | Detach / change tracking | Detach repository |
| `AuditEntry` | Change auditing | `AuditDbContext` |
| `Logging` | Boundary logging | `LogService` |
| `Exceptions` | Catch / rethrow | `throw;` demo |
| `AdvancedFeatures` | Delegates, events, expressions, generics, reflection | `CarServices`, `TaskService`, `PluginScanner`, … |

Each sample README has **Why this example** and **Explaining …** for the main types.

Solution folders under **samples** group these the same way in Visual Studio.
