# Events

Events are restricted multicast delegates: subscribers attach with `+=`, the publisher raises with `?.Invoke`.

## Why this example

Task lifecycle (created / completed) with App + Email subscribers matches real “one raise, many side effects.” `TaskEventArgs` shows why handlers need a payload, not empty `EventArgs`.

## Prefer

- `EventHandler<TaskEventArgs>` so handlers receive the task title
- Raise via a protected `OnX` method
- Keep handlers focused (one side effect each)

## Avoid

- Long `Thread.Sleep` inside publishers (blocks the thread)
- Exposing the event delegate publicly for arbitrary invoke/reset

## Run

Included in `dotnet run --project samples/AdvancedFeatures` (Events section).

## How the code works

Full walkthrough: [docs](../../../docs/07-delegates-and-events.md#events).
