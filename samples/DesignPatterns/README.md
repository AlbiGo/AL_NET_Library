# Design Patterns

Practical creational and behavioral patterns: thread-safe Singleton, Factory Method, Abstract Factory, Builder, and Strategy — when they help, and when DI or simple code is enough.

```bash
dotnet run --project samples/DesignPatterns
```

Comment out any `Run*Demo()` call in `Program.cs` to focus on one pattern.

## Why these examples

| Pattern | Why we chose it |
| --- | --- |
| Singleton | FOO then BAR proves the second create does not overwrite; threads stress locking |
| Factory Method | Resume/Report page lists make “subclass picks products” obvious |
| Abstract Factory | Wolf/Bison food chain shows families without `new` in the client |
| Builder | Car vs motorcycle — same Shop steps, different parts |
| Strategy | Tax PERC/FLAT/PROG — swap algorithms at the call site |

## Explaining the key types

| Pattern | Key type | What to remember |
| --- | --- | --- |
| Singleton | `Singleton` / `LoadBalancer` | Init once; second `GetInstance` does not overwrite; prefer DI when you have a container |
| Factory Method | `Document` | Base decides *when*; subclass decides *which* pages |
| Abstract Factory | `AnimalWorld` | Depends on abstract `Factory` — never `new Wolf()` in the client |
| Builder | `Shop` | Fixed construction order; swap `CarBuilder` / `MotorcycleBuilder` |
| Strategy | `TaxCalculateContext` | Holds `ICalculateTax`; `SetStrategy` changes algorithm |

## Do

| Pattern | Practice highlighted |
| --- | --- |
| Singleton | Double-checked locking; value set only on first create; eager singleton via `LoadBalancer` |
| Factory Method | Subclasses decide which products to create (`Resume` / `Report`) |
| Abstract Factory | `AnimalWorld` depends on abstract `Factory` — not concrete animals |
| Builder | Same `Shop` steps with `CarBuilder` and `MotorcycleBuilder` |
| Strategy | `TaxCalculateContext` + `SetStrategy` swaps PERC / FLAT / PROG |

## Don’t

- Use Singleton for everything that “feels global” — prefer DI lifetimes when you have a container
- Copy unlocked lazy singletons into multithreaded production code

## Guide

See [docs/06-solid-and-patterns.md](../../docs/06-solid-and-patterns.md).

## How the code works

Step-by-step explanation of this sample: [docs/06-solid-and-patterns.md](../../docs/06-solid-and-patterns.md).
