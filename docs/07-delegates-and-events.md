# Delegates and events

## Delegates

### What it is

A **delegate** is a type-safe reference to a method (or several methods) with a matching signature. Callers pass “what to do”; callees invoke without knowing the concrete methods.

In modern C# you often use `Action` / `Func<>`; a named delegate is clearer for teaching.

### How the sample code works

**Sample:** [`samples/AdvancedFeatures/Delegates/`](../samples/AdvancedFeatures/Delegates/)

1. `ServiceGarage` defines `CarServiceDelegate` = `void ()`
2. `CarServicesLocal` methods (`EngineService`, `TireChange`, …) match that signature
3. `Program` builds a **multicast** pipeline with `+=`, then `DoService` invokes the chain and delivers the car:

```csharp
ServiceGarage.CarServiceDelegate pipeline = services.EngineService;
pipeline += services.TireChange;
pipeline += services.OilChange;
pipeline += extension.TransmissionService;
garage.DoService(pipeline);  // Invoke all, then Deliver()
```

`DoService` uses `carServiceDelegate?.Invoke()` so a null pipeline is safe.

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

### Prefer / Avoid

- Prefer: `EventHandler<TEventArgs>` when subscribers need data; raise through protected `OnX` methods
- Avoid: empty `EventArgs` when a title/id would help; long blocking sleeps in publishers; exposing the raw delegate for public invoke
