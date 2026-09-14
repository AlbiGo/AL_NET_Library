# Samples

Runnable demos for each best-practice topic. Read Prefer/Avoid (and **Why this example**) in [`docs/`](../../docs/), then run the matching project.

```bash
dotnet run --project samples/DesignPatterns
dotnet run --project samples/Dependency
dotnet run --project samples/Threads
```

| Folder | Topic |
| --- | --- |
| `DesignPatterns` | Singleton, Factory, Builder, Strategy |
| `SOLID` | ISP / LSP |
| `Dependency` | Composition root & constructor injection |
| `Threads` | Async / concurrency |
| `LINQ` | Filters, pagination, joins |
| `DataManagement` | Repositories, soft-delete, parameterized SQL |
| `EntityFramework` | Detach / change tracking |
| `AuditEntry` | Change auditing |
| `Logging` | Boundary logging |
| `Exceptions` | Catch / rethrow |
| `AdvancedFeatures` | Delegates, events, expressions, generics |

Each sample README has a short **Why this example** section explaining the scenario choice.

Solution folders under **samples** group these the same way in Visual Studio.
