# SOLID

Small, focused interfaces (ISP), subtypes that honor contracts (LSP), and designs that stay substitutable without forcing unused members.

```bash
dotnet run --project samples/SOLID
```

## Why these examples

- **Printers** — clear ISP: a print-only device should not be forced to implement Scan/Fax.
- **Shapes / animals** — LSP is checkable (areas add up; every animal can `Moves()` through `Habitat`).

## Explaining the key types

| Type | Role |
| --- | --- |
| `IMultiFunctionDevice` / `OldPrinter` | Don’t — fat interface forces unused Scan/Fax |
| `IPrinter` / `PrintService` | Do — depend only on what you need |
| `Shape` / `AreaCalculator` | LSP — any shape returns a sensible area |
| `Habitat` | LSP — works for `Animal`, `Bird`, `Fish` |

**Takeaway:** small interfaces; subtypes that keep the base promise.

## What’s here

- **Interface Segregation** — fat `IMultiFunctionDevice` (Don’t) vs `IPrinter` / `IScanner` + `PrintService` (Do)
- **Liskov Substitution** — `Shape` area calculator + `Animal` / `Bird` / `Fish` through `Habitat`

## Guide

See [docs/06-solid-and-patterns.md](../../docs/06-solid-and-patterns.md).

## How the code works

Step-by-step explanation of this sample: [docs/06-solid-and-patterns.md](../../docs/06-solid-and-patterns.md).
