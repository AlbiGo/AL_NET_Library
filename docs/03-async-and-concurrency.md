# Async and concurrency

## What it is

**Concurrency** overlaps work (threads or tasks). **Async/await** frees threads while waiting on I/O or timers, instead of blocking with `Thread.Sleep`. When threads share mutable state, you also need **synchronization** (`lock`, `Interlocked`, concurrent collections).

## Why it matters

Fake `async` (no `await`) and `async void` look modern but teach the wrong mental model. Real samples show *when* work can overlap, *when* order must be respected, and *when* shared updates must be synchronized.

## Why this example

### Why a kitchen?

Concurrency is abstract until you can *see* which work is independent and which is ordered. Cooking does that without infrastructure noise:

| Kitchen fact | Concurrency lesson |
| --- | --- |
| Boil water while chopping / setting the table | Independent work can overlap (threads / tasks) |
| Pasta only after water boils; sauce pour after sauce is ready | Real dependencies need `Join` / `await` — not “start everything and hope” |
| Same recipe, three styles | Sequential vs threads vs async — compare elapsed ms |
| No sockets / DB / HTTP | The Prefer/Avoid rule stays visible in the console |

We did **not** pick a kitchen to teach cooking — we picked a domain where everyone already knows the dependency graph, so `Join` and `await` feel obvious.

### Why a shared counter too?

The kitchen overlaps *independent* work. The counter shows the other trap: many threads mutating the *same* variable without a lock lose updates (`count++` is not atomic).

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

### Shared state — `SharedCounterDemo`

Eight threads each increment a counter 50,000 times (expected 400,000):

| Method | What happens |
| --- | --- |
| `RunUnsafe` | Avoid — no lock; printed `count` is usually **below** expected |
| `RunWithLock` | Prefer — `lock (gate) count++`; count matches expected |
| `RunWithInterlocked` | Prefer — `Interlocked.Increment(ref count)` for a single int |

### What `lock` is

`lock` is mutual exclusion for a critical section:

1. Thread A enters `lock (gate)` and owns the lock.
2. Thread B hitting the same `gate` **blocks** until A leaves the block.
3. A releases the lock when the block ends (including via `finally` if an exception is thrown).

So `count++` inside `lock` cannot interleave with another thread’s `count++` on that same gate. Without it, two threads can both read `5`, both write `6`, and one increment is lost.

`gate` should be a **private** object used only for locking. Prefer `Interlocked` for one integer; prefer `lock` when multiple fields must update as one unit.

**Takeaway:** kitchen = overlap independent work; counter = synchronize shared mutation.

```bash
dotnet run --project samples/Threads
```

Compare elapsed ms on kitchens; compare lost increments on the counter demo.

## Prefer

- `async`/`await` with real waits; `Task`/`Task<T>` (not `async void` except event handlers)
- Express ordering constraints explicitly
- `lock` or `Interlocked` (or concurrent collections) for shared mutable state

## Avoid

- `async` with no `await`; `Thread.Sleep` inside async methods; assuming more threads always means faster
- Unsynchronized `++` / field updates from multiple threads
