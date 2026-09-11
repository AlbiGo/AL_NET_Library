# C# best practices guide

Written rules for this repository. Sample projects under the solution demonstrate the same ideas in runnable code.

| Guide | Sample projects |
| --- | --- |
| [Naming and style](01-naming-and-style.md) | Enforced via `.editorconfig` / analyzers |
| [Dependency injection](02-dependency-injection.md) | `Dependency/` |
| [Async and concurrency](03-async-and-concurrency.md) | `Threads/` |
| [LINQ and EF](04-linq-and-ef.md) | `LINQ/`, `DataManagement/` |
| [Exceptions and logging](05-exceptions-and-logging.md) | `Exceptions/`, `Logging/` |
| [SOLID and patterns](06-solid-and-patterns.md) | `SOLID/`, `DesignPatterns/` |

## How to use this repo

1. Read the Prefer / Avoid bullets for a topic.
2. Open the linked sample and run it (`dotnet run --project <path>`).
3. Treat `_archive/` as historical only — not recommended practice.

## Planned topics

- Algorithms
- Unit testing (xUnit)
- Additional structural design patterns
