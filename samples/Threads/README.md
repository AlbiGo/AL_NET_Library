# Threads / async and concurrency

Do real async (`await` I/O / `Task.Delay`), avoid fake async and `async void`, and overlap independent work with threads or tasks while respecting real ordering.

```bash
dotnet run --project samples/Threads
```

## Do

- **`Kitchen`** — clear sequential baseline
- **`KitchenThread`** — overlap independent steps; `Join` enforces real dependencies
- **`KitchenAsync`** — `await Task.Delay` (true async), same dependency story as threads

## Don’t

- `async` methods that never `await` (fake async)
- `async void` for workflow methods
- Blocking `Thread.Sleep` inside async code paths

## Guide

See [docs/03-async-and-concurrency.md](../../docs/03-async-and-concurrency.md).

## How the code works

Step-by-step explanation of this sample: [docs/03-async-and-concurrency.md](../../docs/03-async-and-concurrency.md).
