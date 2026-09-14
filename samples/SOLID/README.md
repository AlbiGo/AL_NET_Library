# SOLID

Small, focused interfaces (ISP), subtypes that honor contracts (LSP), and designs that stay substitutable without forcing unused members.

```bash
dotnet run --project samples/SOLID
```

## What’s here

- **Interface Segregation** — fat `IMultiFunctionDevice` (Don’t) vs `IPrinter` / `IScanner` + `PrintService` (Do)
- **Liskov Substitution** — `Shape` area calculator + `Animal` / `Bird` / `Fish` through `Habitat`

## Guide

See [docs/06-solid-and-patterns.md](../../docs/06-solid-and-patterns.md).

## How the code works

Step-by-step explanation of this sample: [docs/06-solid-and-patterns.md](../../docs/06-solid-and-patterns.md).
