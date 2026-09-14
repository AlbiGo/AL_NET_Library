# SOLID and design patterns

## SOLID

### What it is

SOLID is a set of design guidelines. This repo demos two of them clearly:

| Principle | Idea |
| --- | --- |
| **ISP** — Interface Segregation | Many small interfaces beat one fat interface |
| **LSP** — Liskov Substitution | Subtypes must be usable wherever the base type is expected |

### How the sample code works

**Sample:** [`samples/SOLID/`](../samples/SOLID/)

**ISP Don’t** — `OldPrinter` implements a fat `IMultiFunctionDevice` and must throw for Scan/Fax.

**ISP Do** — `PrintService` depends only on `IPrinter`. Both `SimplePrinter` and `Photocopier` work:

```csharp
new PrintService(new SimplePrinter()).Run("Invoice");
new PrintService(new Photocopier()).Run("Invoice");
```

**LSP** — `AreaCalculator.TotalArea` accepts any `Shape` (`Rectangle`, `Square`). `Habitat.MakeAnimalMove(Animal)` works for `Bird` / `Fish` the same way.

```bash
dotnet run --project samples/SOLID
```

---

## Singleton

### What it is

Ensure **one shared instance** and a global access point. Use sparingly; prefer DI lifetimes when you already have a container.

### How the sample code works

**File:** `samples/DesignPatterns/Creational/Singleton/Singleton.cs`

- Private constructor blocks `new Singleton()`
- Double-checked locking: null-check → `lock` → null-check again → create
- `Value` is set **only on first create**; later `GetInstance("BAR")` still returns `"FOO"`

`LoadBalancer` shows an **eager** singleton (`static readonly` instance) — no lock needed because type initialization is thread-safe.

---

## Factory Method

### What it is

A creator defines an abstract factory method; subclasses decide which concrete products to build.

### How the sample code works

`Document` constructor calls `CreatePages()`. `Resume` and `Report` override it to add different `Page` types. Client code works with `Document` without knowing page lists.

---

## Abstract Factory

### What it is

Create **families** of related products (e.g. herbivore vs carnivore factories) without binding to concrete classes.

### How the sample code works

`AnimalWorld` depends on abstract `Factory` only. `HerbivoreFactory` / `CarnivoreFactory` supply `Bison` / `Wolf`. Swap factories without changing `AnimalWorld`.

---

## Builder

### What it is

Build a complex object step by step. The director (`Shop`) calls fixed steps; concrete builders (`CarBuilder`, `MotorcycleBuilder`) accumulate different parts.

### How the sample code works

```csharp
shop.Construct(new CarBuilder()).PrintParts();
shop.Construct(new MotorcycleBuilder()).PrintParts();  // same steps, different product
```

---

## Strategy

### What it is

Swap algorithms behind a common interface at runtime.

### How the sample code works

`TaxCalculateContext` holds `ICalculateTax`. The demo calls `SetStrategy` to swap PERC → FLAT → PROG at runtime without editing the context class.

```bash
dotnet run --project samples/DesignPatterns
```

---

## Prefer

- Small interfaces; substitutable subtypes; patterns when variation is real

## Avoid

- Fat interfaces; unlocked lazy singletons in multithreaded code; patterns for their own sake
