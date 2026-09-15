# Events

Events are restricted multicast delegates: subscribers attach with `+=`, the publisher raises with `?.Invoke`.

## Why this example

Task lifecycle (created / completed) with App + Email subscribers matches real “one raise, many side effects.” `TaskEventArgs` shows why handlers need a payload, not empty `EventArgs`.

## Explaining `TaskService`

`TaskService` is the **publisher**. It declares `TaskCreated` / `TaskCompleted` as `event EventHandler<TaskEventArgs>?`.

Outsiders may only subscribe (`+=`) or unsubscribe (`-=`). They cannot assign `null` or call `Invoke` — unlike a public delegate field. That restriction is the whole point of `event`.

Flow:

1. `PrepareTask` / `CompleteTask` do the business work
2. They call protected `OnTaskCreated` / `OnTaskCompleted`
3. Those raise with `TaskEventArgs { Title = ... }`
4. `AppService` and `EmailService` each handle the same raise differently

```text
TaskService.PrepareTask
        │
        ▼
  OnTaskCreated(e)  ──►  AppService.OnTaskCreated(e)     (UI / state)
                    └──►  EmailService.OnTaskCreated(e)   (mail)
```

**Takeaway:** one raise, many listeners; put useful data in `EventArgs`.

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
