# Delegates

A **delegate** is a type-safe callback: a variable that holds a reference to a method (or several methods) with a matching signature.

## Why this example

A car garage pipeline is an ordered list of void steps from different classes — ideal for showing multicast `+=` and `Invoke` without needing events or DI.

## Explaining `CarServices`

`CarServices` is not the “smart” part of the demo — it is the **work** the delegate points at.

### What it is

A small class that knows one `Car` and exposes three service steps:

- `EngineService`
- `TireChange`
- `OilChange`

Each method is `void` with no parameters — the same signature as `ServiceGarage.CarServiceDelegate` (same as `Action`).

### Why it looks so simple

That is intentional. Delegates care about **shape**, not class hierarchy:

- same signature → can be stored in a delegate variable
- can be combined with `+=` into a multicast pipeline
- can come from **this** class or from another (`CarServiceExtension.TransmissionService`)

So `CarServices` is the concrete “things we might do to a car,” while `ServiceGarage` only knows “run whatever pipeline you give me, then deliver.”

### How it fits together

```text
CarServices.EngineService  ─┐
CarServices.TireChange     ─┼─►  CarServiceDelegate pipeline  ─►  DoService()  ─►  Car.Deliver()
CarServices.OilChange      ─┘
```

In `Program`:

```csharp
ServiceGarage.CarServiceDelegate pipeline = services.EngineService;
pipeline += services.TireChange;
pipeline += services.OilChange;
garage.DoService(pipeline);
```

`DoService` does not call `OilChange` by name. It calls `pipeline.Invoke()`, which runs every attached method in order on that same `Car`.

**One-line takeaway:** any matching methods can become a type-safe, reorderable checklist — the garage stays open to new steps without changing `DoService`.

## Prefer

- Use a named delegate (or `Action` / `Func<>`) when a component should accept “what to do” without knowing the concrete methods
- Multicast with `+=` when several handlers should run in order
- Invoke safely (`delegate?.Invoke()`)

## Avoid

- Confusing delegates with events — events are a restricted wrapper around delegates for pub/sub
- Huge multicast lists with unclear order/side effects

## Files

| File | Role |
| --- | --- |
| `ServiceGarage.cs` | Defines `CarServiceDelegate` and invokes the pipeline |
| `CarServices.cs` | Concrete service steps (the work the delegate points at) |
| `Car.cs` | Model delivered after services run |

## Run

```bash
dotnet run --project samples/AdvancedFeatures
```

Look for the **Delegates** section in console output.

## How the code works

Full walkthrough: [docs](../../../docs/07-delegates-and-events.md#delegates).
