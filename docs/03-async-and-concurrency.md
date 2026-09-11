# Async and concurrency

## Prefer

- `async`/`await` with real asynchronous waits (`Task.Delay`, I/O APIs)
- `Task` / `Task<T>` return types (not `async void`, except event handlers)
- Overlapping independent work with `Task.WhenAll` or carefully joined threads
- Expressing real ordering constraints (e.g. boil water before pouring pasta)
- Cancellation tokens on cancellable async APIs

## Avoid

- Marking methods `async` without awaiting anything (fake async)
- `async void` for library/business methods
- `Thread.Sleep` inside async methods (blocks a thread)
- Fire-and-forget without observing errors
- Assuming more threads always means faster (measure)

## See sample

- [`Threads/`](../Threads/) — sequential vs threaded vs async kitchen timings

```bash
dotnet run --project Threads
```
