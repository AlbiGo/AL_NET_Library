# Design Patterns

Practical creational and behavioral patterns: thread-safe Singleton, Factory Method, Abstract Factory, Builder, and Strategy — when they help, and when DI or simple code is enough.

```bash
dotnet run --project samples/DesignPatterns
```

Comment out any `Run*Demo()` call in `Program.cs` to focus on one pattern.

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
