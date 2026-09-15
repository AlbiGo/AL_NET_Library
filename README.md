# C# / .NET best practices

Curated guides and runnable samples for modern C# development.  
**Docs define the rules; samples prove them.**

```text
docs/        Prefer / Avoid guides (curriculum)
samples/     Runnable projects (one topic each)
_archive/    Historical / unfinished (not in the solution)
```

```bash
git clone https://github.com/AlbiGo/AL_NET_Library.git
dotnet build AL_NET_Library.sln
dotnet run --project samples/DesignPatterns
```

Requires **.NET SDK 8.0+** (CI builds with 8.0).

## Start here

1. Read the [best practices guide](docs/README.md) — each page has **What**, **Why this example**, **Explaining …** (key types), and Prefer/Avoid
2. Open `AL_NET_Library.sln` (projects under **samples**)
3. Run a sample, e.g. `dotnet run --project samples/Dependency`

## Samples (in solution)

| Solution folder | Projects |
| --- | --- |
| **Patterns** | `DesignPatterns`, `SOLID` |
| **Data** | `DataManagement`, `EntityFrameworkTraining`, `AuditEntry`, `LINQ`, `Logging` |
| **Concurrency** | `Threads` |
| **Architecture** | `DependencyInjection`, `AdvancedFeatures` |
| **Fundamentals** | `Exceptions` |

See also [samples/README.md](samples/README.md).

## Tooling

- [`.editorconfig`](.editorconfig) — naming and code style
- [`Directory.Build.props`](Directory.Build.props) — nullable, analyzers, shared defaults

## Archive

Unfinished historical code lives in [`_archive/`](_archive/) and is **not** part of the solution.

## Contributing

1. Branch from `dev` and open a PR
2. Use descriptive branch names: `username/(feature|bug|fix|new)/description-id`
3. Prefer adding Prefer/Avoid notes in `docs/` when you change sample guidance
4. Keep sample projects buildable under .NET 8

## License

MIT — see the LICENSE file for details.
