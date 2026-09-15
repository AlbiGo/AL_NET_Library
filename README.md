# C# / .NET best practices

[![.NET](https://github.com/AlbiGo/AL_NET_Library/actions/workflows/dotnet.yml/badge.svg)](https://github.com/AlbiGo/AL_NET_Library/actions/workflows/dotnet.yml)

Curated **Prefer / Avoid** guides and runnable samples for modern C#.  
**Docs define the rules; samples prove them.**

```text
docs/        Curriculum (What / Why / Explaining / Prefer-Avoid)
samples/     Runnable projects (.NET 8)
_archive/    Historical stubs — do not learn from these
```

## Quick start

```bash
git clone https://github.com/AlbiGo/AL_NET_Library.git
cd AL_NET_Library
dotnet build AL_NET_Library.sln
dotnet run --project samples/Dependency
```

Requires **.NET SDK 8.0+**.

### Try this first

| Command | What you’ll see |
| --- | --- |
| `dotnet run --project samples/Dependency` | DI Prefer vs manual `new`, Options from `appsettings.json` |
| `dotnet run --project samples/Threads` | Kitchen timing + shared-counter `lock` demo |
| `dotnet run --project samples/DesignPatterns` | Strategy swap, Builder car/bike, Singleton |

## Learning path

Suggested order (≈ 3–4 hours if you run every sample):

1. **Foundations** — [Naming](docs/01-naming-and-style.md) → [Exceptions](docs/05-exceptions-and-logging.md) → [Logging](docs/05-exceptions-and-logging.md#logging)
2. **Architecture** — [DI](docs/02-dependency-injection.md) → [Options](docs/10-options-pattern.md) → [Async / Threads](docs/03-async-and-concurrency.md)
3. **Design** — [SOLID](docs/06-solid-and-patterns.md) → patterns in the same guide
4. **Data** — [LINQ & EF](docs/04-linq-and-ef.md)
5. **Language** — [Delegates & events](docs/07-delegates-and-events.md) → [Generics / expressions](docs/08-generics-expressions-compiled-queries.md) → [Reflection](docs/09-reflection.md)

Full index: [docs/README.md](docs/README.md) · sample map: [samples/README.md](samples/README.md)

## Samples (in solution)

| Solution folder | Projects (paths under `samples/`) |
| --- | --- |
| **Patterns** | `DesignPatterns`, `SOLID` |
| **Data** | `DataManagement`, `EntityFramework`, `AuditEntry`, `LINQ`, `Logging` |
| **Concurrency** | `Threads` |
| **Architecture** | `Dependency`, `AdvancedFeatures` |
| **Fundamentals** | `Exceptions` |

Smoke all console demos: `pwsh scripts/run-all.ps1`

## Tooling

- [`.editorconfig`](.editorconfig) — naming and style
- [`Directory.Build.props`](Directory.Build.props) — nullable, analyzers
- CI — GitHub Actions builds the solution on .NET 8

## Archive

[`_archive/`](_archive/) holds unfinished historical code. **Not** in the solution and **not** best-practice examples.

## Contributing

See [CONTRIBUTING.md](CONTRIBUTING.md).

## License

[MIT](LICENSE)
