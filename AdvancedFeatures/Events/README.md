# Events

Events are restricted multicast delegates: subscribers attach with `+=`, the publisher raises with `?.Invoke`.

## Prefer

- `EventHandler` / `EventHandler<T>` for standard signatures
- Raise via a protected `OnX` method
- Keep handlers focused (one side effect each)

## Avoid

- Long `Thread.Sleep` inside publishers (blocks the thread)
- Exposing the event delegate publicly for arbitrary invoke/reset

## Run

Included in `dotnet run --project AdvancedFeatures` (Events section).
