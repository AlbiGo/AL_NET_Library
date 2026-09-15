# Async and concurrency

## What it is

**Concurrency** overlaps work (threads or tasks). **Async/await** frees threads while waiting on I/O or timers, instead of blocking with `Thread.Sleep`.

## Why it matters

Fake `async` (no `await`) and `async void` look modern but teach the wrong mental model. Real samples show *when* work can overlap and *when* order must be respected.

## Why this example

A **kitchen** has obvious independent steps (boil water vs chop ingredients) and real dependencies (pasta waits for water). That makes `Join` / `await` ordering intuitive without needing sockets or a database.

## How the sample code works

**Sample:** [`samples/Threads/`](../samples/Threads/)

### Sequential — `Kitchen`

Each step runs one after another (`BoilWater` → `PrepareIngredients` → …). This is the baseline timing.

### Threads — `KitchenThread`

Independent steps start on separate threads; `Join` waits where there is a real dependency (e.g. water must boil before pasta goes in):

```csharp
boilWater.Start();
prepareIngredients.Start();
prepareIngredients.Join();
makeSauce.Start();
boilWater.Join();
pourPasta.Start();
```

### Async — `KitchenAsync`

Same dependency graph using `await Task.Delay(...)` (true async). Methods return `Task` and are named `*Async`.

### Explaining the kitchen demos

| Type | Role |
| --- | --- |
| `Kitchen` | Sequential baseline — every step waits for the previous |
| `KitchenThread` | Overlap independent work; `Join` only where order matters |
| `KitchenAsync` | Same graph with `await Task.Delay` — frees the thread while waiting |

Boil water and chop ingredients can overlap; pouring pasta must wait for boiled water. That dependency is why `Join` / `await` exist in the sample.

**Takeaway:** overlap what is independent; express real ordering explicitly.

```bash
dotnet run --project samples/Threads
```

Compare elapsed ms: sequential is slowest; threaded/async overlap independent work.

## Prefer

- `async`/`await` with real waits; `Task`/`Task<T>` (not `async void` except event handlers)
- Express ordering constraints explicitly

## Avoid

- `async` with no `await`; `Thread.Sleep` inside async methods; assuming more threads always means faster
