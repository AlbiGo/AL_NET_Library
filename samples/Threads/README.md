# Threads / async and concurrency

Do real async (`await` I/O / `Task.Delay`), avoid fake async and `async void`, overlap independent work while respecting ordering, and synchronize shared mutable state.

```bash
dotnet run --project samples/Threads
```

## Why this example

### Why a kitchen?

Cooking makes concurrency rules obvious without servers or databases:

- **Independent steps** (boil water, chop, set table) → safe to overlap with threads / tasks  
- **Real dependencies** (pasta needs boiling water; pour sauce after sauce is ready) → need `Join` / `await`  
- **Same recipe three ways** → sequential vs threaded vs async; elapsed ms shows the win  
- **Familiar domain** → you already know the ordering, so the API (`Join`, `await`) is the lesson, not the story

We use a kitchen so the dependency graph is common sense — not to teach cooking.

### Why a shared counter too?

Kitchen = overlap independent work. Counter = shared mutation needs `lock` / `Interlocked` (lost updates without it).

## Explaining the kitchen demos

| Type | Role |
| --- | --- |
| `Kitchen` | Sequential baseline |
| `KitchenThread` | Overlap independent steps; `Join` where order matters |
| `KitchenAsync` | Same graph with `await Task.Delay` (true async) |

## Explaining `SharedCounterDemo`

| Method | Verdict |
| --- | --- |
| `RunUnsafe` | Avoid — bare `count++` from many threads → lost updates |
| `RunWithLock` | Prefer — `lock` around the shared update |
| `RunWithInterlocked` | Prefer — atomic increment for a single `int` |

### What `lock` is

`lock (gate) { … }` is a **mutex for a block of code**: only one thread at a time may run that block while locking on the same `gate` object. Other threads **wait** until the lock is released.

- Why it helps: `count++` is read → add 1 → write. Without a lock, two threads can read the same value and both write `N+1` (one increment lost).
- What `gate` is: a private `object` used only as the lock identity (not the data). Prefer a dedicated object; avoid locking on `this` or public types.
- Under the hood: `Monitor.Enter(gate)` / `try { … } finally { Monitor.Exit(gate) }`.
- Keep it small: lock only the shared mutation, not slow I/O.

`Interlocked.Increment` is a lighter Prefer for a **single** `int`. Use `lock` when several fields must stay consistent together.

Kitchen = independent work can overlap. Counter = shared mutation must be synchronized.

**Takeaway:** overlap what is independent; lock (or `Interlocked`) what is shared; name async methods `*Async`.

## Do

- **`Kitchen`** — clear sequential baseline
- **`KitchenThread`** — overlap independent steps; `Join` enforces real dependencies
- **`KitchenAsync`** — `await Task.Delay` (true async), same dependency story as threads
- **`SharedCounterDemo`** — Prefer `lock` / `Interlocked` for shared counters

## Don’t

- `async` methods that never `await` (fake async)
- `async void` for workflow methods
- Blocking `Thread.Sleep` inside async code paths
- Unsynchronized read/modify/write on shared fields

## Guide

See [docs/03-async-and-concurrency.md](../../docs/03-async-and-concurrency.md).

## How the code works

Step-by-step explanation of this sample: [docs/03-async-and-concurrency.md](../../docs/03-async-and-concurrency.md).
