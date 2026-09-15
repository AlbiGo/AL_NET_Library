# SOLID and design patterns

## SOLID

### What it is

SOLID is a set of design guidelines. This repo demos two of them clearly:

| Principle | Idea |
| --- | --- |
| **ISP** — Interface Segregation | Many small interfaces beat one fat interface |
| **LSP** — Liskov Substitution | Subtypes must be usable wherever the base type is expected |

### Why this example

- **Printers** — everyone understands “print-only vs scan/fax.” A fat `IMultiFunctionDevice` forces `OldPrinter` to throw; `PrintService(IPrinter)` shows the fix without inventing domain jargon.
- **Shapes (+ animals)** — area is a contract you can check with a number (`TotalArea == 37`). Animals keep a second, simpler LSP story for movement.

### How the sample code works

**Sample:** [`samples/SOLID/`](../samples/SOLID/)

**ISP Don’t** — `OldPrinter` implements a fat `IMultiFunctionDevice` and must throw for Scan/Fax.

**ISP Do** — `PrintService` depends only on `IPrinter`. Both `SimplePrinter` and `Photocopier` work:

```csharp
new PrintService(new SimplePrinter()).Run("Invoice");
new PrintService(new Photocopier()).Run("Invoice");
```

**LSP** — `AreaCalculator.TotalArea` accepts any `Shape` (`Rectangle`, `Square`). `Habitat.MakeAnimalMove(Animal)` works for `Bird` / `Fish` the same way.

### Explaining `PrintService` and `AreaCalculator`

**ISP — `PrintService(IPrinter)`:** callers that only print never see Scan/Fax. `SimplePrinter` implements just `IPrinter`; `Photocopier` can implement both without forcing print-only clients to care.

**LSP — `AreaCalculator`:** any `Shape` must return a sensible area. `Rectangle` and `Square` are interchangeable here — that is the substitution rule in one number (`TotalArea == 37`).

**Takeaway:** depend on small surfaces; subtypes must honor the base contract.

```bash
dotnet run --project samples/SOLID
```

---

## Singleton

### What it is

Ensure **one shared instance** and a global access point. Use sparingly; prefer DI lifetimes when you already have a container.

### Why this example

`GetInstance("FOO")` then `GetInstance("BAR")` proves the second call does not overwrite — the classic singleton bug. Threads + eager `LoadBalancer` cover lazy locking vs type-init without needing a real shared cache.

### How the sample code works

**File:** `samples/DesignPatterns/Creational/Singleton/Singleton.cs`

- Private constructor blocks `new Singleton()`
- Double-checked locking: null-check → `lock` → null-check again → create
- `Value` is set **only on first create**; later `GetInstance("BAR")` still returns `"FOO"`

`LoadBalancer` shows an **eager** singleton (`static readonly` instance) — no lock needed because type initialization is thread-safe.

### Explaining `Singleton`

Lazy + lock protects concurrent first access. The second argument to `GetInstance` is ignored after creation — that is intentional teaching of “init once.” Prefer DI lifetimes when you already have a container; use Singleton sparingly for true process-wide state.

---

## Factory Method

### What it is

A creator defines an abstract factory method; subclasses decide which concrete products to build.

### Why this example

**Resume vs Report** documents have different page sets but the same “create document → fill pages” flow. Clients stay on `Document`; subclasses own the product list — the textbook Factory Method shape.

### How the sample code works

`Document` constructor calls `CreatePages()`. `Resume` and `Report` override it to add different `Page` types. Client code works with `Document` without knowing page lists.

### Explaining `Document`

The base type defines *when* pages are created; subclasses define *which* pages. Clients loop `document.pages` without branching on Resume vs Report.

---

## Abstract Factory

### What it is

Create **families** of related products (e.g. herbivore vs carnivore factories) without binding to concrete classes.

### Why this example

Prey/predator factories make “families of related products” obvious in one line of output (`Wolf eats Bison`). `AnimalWorld` depending on abstract `Factory` shows the client never mentions `new Wolf()`.

### How the sample code works

`AnimalWorld` depends on abstract `Factory` only. `HerbivoreFactory` / `CarnivoreFactory` supply `Bison` / `Wolf`. Swap factories without changing `AnimalWorld`.

### Explaining `AnimalWorld`

Constructor takes two `Factory` abstractions. `RunFoodChain` only calls `CreateAnimal()`. Swap concrete factories to change the ecosystem without editing the world class.

---

## Builder

### What it is

Build a complex object step by step. The director (`Shop`) calls fixed steps; concrete builders (`CarBuilder`, `MotorcycleBuilder`) accumulate different parts.

### Why this example

Vehicles need the same construction order (frame → engine → wheels → doors) but different parts. Running **car and motorcycle** through one `Shop` shows the director stays fixed while the builder swaps.

### How the sample code works

```csharp
shop.Construct(new CarBuilder()).PrintParts();
shop.Construct(new MotorcycleBuilder()).PrintParts();  // same steps, different product
```

### Explaining `Shop`

`Shop` is the director — fixed step order. Builders accumulate parts. Same construction recipe, different products.

---

## Strategy

### What it is

Swap algorithms behind a common interface at runtime.

### Why this example

**Tax rules** (percentage / flat / progressive) are interchangeable algorithms with the same “calculate” call site. Swapping via `SetStrategy` mirrors how real billing/pricing code changes policy without editing the context.

### How the sample code works

`TaxCalculateContext` holds `ICalculateTax`. The demo calls `SetStrategy` to swap PERC → FLAT → PROG at runtime without editing the context class.

### Explaining `TaxCalculateContext`

The context depends on `ICalculateTax`, not on PERC/FLAT/PROG classes. Change behavior by injecting or `SetStrategy` — never by editing the context’s `Calculate` method.

```bash
dotnet run --project samples/DesignPatterns
```

---

## Prefer

- Small interfaces; substitutable subtypes; patterns when variation is real

## Avoid

- Fat interfaces; unlocked lazy singletons in multithreaded code; patterns for their own sake
