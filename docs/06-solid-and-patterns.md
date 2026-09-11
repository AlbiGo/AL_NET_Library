# SOLID and design patterns

## Prefer

- Small interfaces that match how callers use them (ISP)
- Substitutable subtypes that honor base contracts (LSP)
- Strategy / factory / builder when behavior or construction truly varies
- Thread-safe singleton only when a single shared instance is required (prefer DI lifetimes)
- Pattern demos that show **why** the pattern helps, not only the shape

## Avoid

- Fat interfaces that force unused members
- Overusing singleton for everything that “feels global”
- Patterns for their own sake when a simple function would do
- Inheritance hierarchies that break expectations (e.g. throwing in overrides)

## See samples

- [`SOLID/`](../SOLID/) — ISP / LSP sketches
- [`DesignPatterns/`](../DesignPatterns/) — Singleton, Factory Method, Abstract Factory, Builder, Strategy

```bash
dotnet run --project SOLID
dotnet run --project DesignPatterns
```
