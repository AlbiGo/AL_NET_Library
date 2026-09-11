# Threads / async

Compares sequential work, overlapping threads, and real async/await.

```bash
dotnet run --project Threads
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

See [docs/03-async-and-concurrency.md](../docs/03-async-and-concurrency.md).
