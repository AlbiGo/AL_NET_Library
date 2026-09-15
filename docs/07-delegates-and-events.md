# Delegates and events

## Delegates

### What it is

A **delegate** is a type-safe reference to a method (or several methods) with a matching signature. Callers pass “what to do”; callees invoke without knowing the concrete methods.

In modern C# you often use `Action` / `Func<>`; a named delegate is clearer for teaching.

### Why this example

A **garage service pipeline** (engine → tires → oil → transmission) is an ordered list of void steps — perfect for multicast `+=`. You see methods from different classes (`CarServices`, `CarServiceExtension`) plugged into one `DoService` call without inheritance.

### How the sample code works

**Sample:** [`samples/AdvancedFeatures/Delegates/`](../samples/AdvancedFeatures/Delegates/)

1. `ServiceGarage` defines `CarServiceDelegate` = `void ()`
2. `CarServices` methods (`EngineService`, `TireChange`, …) match that signature
3. `Program` builds a **multicast** pipeline with `+=`, then `DoService` invokes the chain and delivers the car:

```csharp
ServiceGarage.CarServiceDelegate pipeline = services.EngineService;
pipeline += services.TireChange;
pipeline += services.OilChange;
pipeline += extension.TransmissionService;
garage.DoService(pipeline);  // Invoke all, then Deliver()
```

`DoService` uses `carServiceDelegate?.Invoke()` so a null pipeline is safe.

### Explaining `CarServices`

`CarServices` is not the “smart” part of the demo — it is the **work** the delegate points at.

It holds one `Car` and exposes simple steps (`EngineService`, `TireChange`, `OilChange`). Each is `void` with no args — the same shape as `CarServiceDelegate` / `Action`.

That simplicity is the point. Delegates care about **signature**, not inheritance:

- matching methods can be stored in a delegate variable
- combined with `+=` into a multicast pipeline
- mixed across classes (e.g. `CarServiceExtension.TransmissionService`)

```text
CarServices.EngineService  ─┐
CarServices.TireChange     ─┼─►  CarServiceDelegate pipeline  ─►  DoService()  ─►  Car.Deliver()
CarServices.OilChange      ─┘
```

`DoService` never calls `OilChange` by name — it invokes the pipeline. New steps can be added without changing the garage.

**Takeaway:** matching methods become a type-safe, reorderable checklist.

More detail: [`samples/AdvancedFeatures/Delegates/README.md`](../samples/AdvancedFeatures/Delegates/README.md).

```bash
dotnet run --project samples/AdvancedFeatures
```

Look for the **Delegates** section in the console output.

### Prefer / Avoid

- Prefer: named delegates or `Action`/`Func`; multicast when order is intentional
- Avoid: confusing delegates with events; huge unordered multicast lists

---

## Events

### What it is

An **event** is a restricted multicast delegate. Outside code can only subscribe (`+=`) / unsubscribe (`-=`). Only the declaring type can raise it — that protects the publisher.

### Why this example

**Task created / completed** is a familiar domain event: one publisher, many side effects (UI + email). `TaskEventArgs.Title` shows why `EventHandler<T>` beats empty `EventArgs` when handlers need payload.

### How the sample code works

**Sample:** [`samples/AdvancedFeatures/Events/`](../samples/AdvancedFeatures/Events/)

- `TaskService` is the **publisher** (`EventHandler<TaskEventArgs>` — payload carries the title)
- `AppService` and `EmailService` are **subscribers** with different side effects
- `PrepareTask` / `CompleteTask` raise via `OnTaskCreated` / `OnTaskCompleted`

```csharp
taskService.TaskCreated += app.OnTaskCreated;
taskService.TaskCreated += email.OnTaskCreated;
taskService.PrepareTask(work);  // both handlers run; each reads e.Title
```

The model is named `TaskItem` so it does not clash with `System.Threading.Tasks.Task`.

### Explaining `TaskService`

`TaskService` owns the events. Subscribers may only `+=` / `-=`; they cannot `Invoke` or clear the list. That is the difference from a public delegate field.

`PrepareTask` does real work, then calls `OnTaskCreated` with `TaskEventArgs` so handlers learn *which* task changed. `AppService` and `EmailService` are parallel listeners — same raise, different side effects.

**Takeaway:** publisher raises once; many handlers react; payload travels in `EventArgs`.

### Prefer / Avoid

- Prefer: `EventHandler<TEventArgs>` when subscribers need data; raise through protected `OnX` methods
- Avoid: empty `EventArgs` when a title/id would help; long blocking sleeps in publishers; exposing the raw delegate for public invoke
