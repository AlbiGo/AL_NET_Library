# SOLID

Small, focused interfaces (ISP), subtypes that honor contracts (LSP), and designs that stay substitutable without forcing unused members.

```bash
dotnet run --project SOLID
```

## What’s here

- **Interface Segregation** — split roles (`IBaseClassA` / `IBaseClassB`) instead of one fat interface
- **Liskov Substitution** — `Animal` / `Bird` / `Fish` used through a shared `Habitat` API

## Guide

See [docs/06-solid-and-patterns.md](../docs/06-solid-and-patterns.md).
