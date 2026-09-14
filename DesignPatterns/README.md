# Design Patterns

Practical creational and behavioral patterns: thread-safe Singleton, Factory Method, Abstract Factory, Builder, and Strategy — when they help, and when DI or simple code is enough.

```bash
dotnet run --project DesignPatterns
```

Comment out any `Run*Demo()` call in `Program.cs` to focus on one pattern.

## Do

| Pattern | Practice highlighted |
| --- | --- |
| Singleton | Double-checked locking; value set only on first create; eager singleton via `LoadBalancer` |
| Factory Method | Subclasses decide which products to create (`Resume` / `Report`) |
| Abstract Factory | Families of related products (`HerbivoreFactory` / `CarnivoreFactory`) |
| Builder | Stepwise construction via `Shop` + `CarBuilder` |
| Strategy | Swap tax algorithms through `TaxCalculateContext` |

## Don’t

- Use Singleton for everything that “feels global” — prefer DI lifetimes when you have a container
- Copy unlocked lazy singletons into multithreaded production code

## Guide

See [docs/06-solid-and-patterns.md](../docs/06-solid-and-patterns.md).
