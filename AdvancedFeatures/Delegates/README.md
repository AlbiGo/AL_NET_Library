# Delegates

A **delegate** is a type-safe callback: a variable that holds a reference to a method (or several methods) with a matching signature.

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
dotnet run --project AdvancedFeatures
```

Look for the **Delegates** section in console output.
