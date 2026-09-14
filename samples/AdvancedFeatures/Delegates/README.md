# Delegates

A **delegate** is a type-safe callback: a variable that holds a reference to a method (or several methods) with a matching signature.

## Why this example

A car garage pipeline is an ordered list of void steps from different classes — ideal for showing multicast `+=` and `Invoke` without needing events or DI.

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
| `CarServicesLocal.cs` | Methods that match the delegate signature |
| `Car.cs` | Model delivered after services run |

## Run

```bash
dotnet run --project samples/AdvancedFeatures
```

Look for the **Delegates** section in console output.

## How the code works

Full walkthrough: [docs](../../../docs/07-delegates-and-events.md#delegates).
