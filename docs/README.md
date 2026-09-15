# C# best practices guide

Each page explains **the concept** and **how the sample code works**.  
Samples live under [`samples/`](../samples/).

## Fundamentals

| Guide | Sample |
| --- | --- |
| [Naming and style](01-naming-and-style.md) | `.editorconfig` / `Directory.Build.props` |
| [Exceptions](05-exceptions-and-logging.md#exceptions) | `samples/Exceptions/` |
| [Logging](05-exceptions-and-logging.md#logging) | `samples/Logging/` |

## Architecture

| Guide | Sample |
| --- | --- |
| [Dependency injection](02-dependency-injection.md) | `samples/Dependency/` |
| [Async and concurrency](03-async-and-concurrency.md) | `samples/Threads/` |

## Data

| Guide | Sample |
| --- | --- |
| [LINQ](04-linq-and-ef.md#linq) | `samples/LINQ/` |
| [Repositories & soft-delete](04-linq-and-ef.md#repositories-and-soft-delete) | `samples/DataManagement/` |
| [Parameterized SQL](04-linq-and-ef.md#parameterized-sql) | `samples/DataManagement/` |
| [EF detach](04-linq-and-ef.md#ef-detach) | `samples/EntityFramework/` |
| [Audit trail](04-linq-and-ef.md#audit-trail) | `samples/AuditEntry/` |

## Design

| Guide | Sample |
| --- | --- |
| [SOLID (ISP & LSP)](06-solid-and-patterns.md#solid) | `samples/SOLID/` |
| [Singleton](06-solid-and-patterns.md#singleton) | `samples/DesignPatterns/` |
| [Factory Method](06-solid-and-patterns.md#factory-method) | `samples/DesignPatterns/` |
| [Abstract Factory](06-solid-and-patterns.md#abstract-factory) | `samples/DesignPatterns/` |
| [Builder](06-solid-and-patterns.md#builder) | `samples/DesignPatterns/` |
| [Strategy](06-solid-and-patterns.md#strategy) | `samples/DesignPatterns/` |

## Advanced language features

| Guide | Sample |
| --- | --- |
| [Delegates](07-delegates-and-events.md#delegates) | `samples/AdvancedFeatures/Delegates/` |
| [Events](07-delegates-and-events.md#events) | `samples/AdvancedFeatures/Events/` |
| [Expression trees](08-generics-expressions-compiled-queries.md#expression-trees) | `samples/AdvancedFeatures/Expressions/` |
| [Generics](08-generics-expressions-compiled-queries.md#generics) | `samples/AdvancedFeatures/Generics/` |
| [Custom LINQ / yield](08-generics-expressions-compiled-queries.md#custom-linq) | `samples/AdvancedFeatures/Linq/` |
| [Compiled queries](08-generics-expressions-compiled-queries.md#compiled-queries) | `samples/AdvancedFeatures/CompiledQueries/` |
| [Reflection](09-reflection.md) | `samples/AdvancedFeatures/Reflection/` |

## How to use

1. Read **What it is**, **Why this example**, and **Explaining …** (key types).
2. Run the sample from the repo root.
3. Compare Prefer / Avoid with the code you just ran.

```bash
dotnet run --project samples/DesignPatterns
```
