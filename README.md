# C# / .NET Library

A collection of C# / .NET examples covering software engineering concepts, patterns, and techniques.

The project is still growing. Contributions are welcome once the library is in good shape for sharing more widely.

## Contents (in solution)

Projects under [AL_NET_Library.sln](AL_NET_Library.sln), grouped by topic:

| Solution folder | Projects |
| --- | --- |
| **Patterns** | `DesignPatterns`, `SOLID` |
| **Data** | `DataManagement`, `EntityFrameworkTraining`, `AuditEntry`, `LINQ`, `Logging` |
| **Concurrency** | `Threads`, `Memory_Span` (placeholder), `Parallell` (placeholder) |
| **Architecture** | `DependencyInjection`, `AdvancedFeatures` |
| **Fundamentals** | `Exceptions` |

`Memory_Span` and `Parallell` are stubs kept in the solution as placeholders for future examples.

## On disk / not in solution (WIP)

These folders exist in the repo but are **not** added to the solution yet:

- `Training`
- `CarServices`
- `GarbageCollector`
- `AsynchronousProgramming`
- `MultithreadingAndConcurrency`
- `DependencyInjectionOld`
- `Solid Principles`

## Planned (not started)

- Algorithms
- Unit testing examples (NUnit / MSTest)
- Additional design patterns (e.g. Observer, structural patterns)

## Contributing

1. Always use branching and PRs.
2. All development must start from the `dev` branch.
3. Use descriptive branch naming: `username/(feature|bug|fix|new)/branch-name-unique-code`
4. Be professional on PR comments.

## Getting started

```bash
git clone https://github.com/AlbiGo/AL_NET_Library.git
```

Open `AL_NET_Library.sln` in Visual Studio or another C# IDE, then explore the project folders and comments in the code.

### Requirements

- .NET SDK 6.0 or later (CI builds with .NET 8.0)
- Visual Studio or any IDE that supports C# development

## License

This project is licensed under the MIT License — see the LICENSE file for details.

## Acknowledgments

- [C# documentation](https://learn.microsoft.com/dotnet/csharp/)
- Stack Overflow and community resources for .NET development challenges
