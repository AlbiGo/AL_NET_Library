# Exceptions and logging

## Exceptions

### What it is

Exceptions signal unexpected failures. Catch only what you can handle; otherwise let them bubble. When rethrowing, use `throw;` so the stack trace stays intact.

### How the sample code works

**Sample:** [`samples/Exceptions/`](../samples/Exceptions/)

1. **Handle and continue** — loop calls `ProcessIndex`; index `6` throws `InvalidOperationException`; catch logs and continues.
2. **Rethrow correctly** — `RethrowDemo` catches, then `throw;` (not `throw ex;`). The outer catch checks that the stack still mentions `ThrowInner`.

```bash
dotnet run --project samples/Exceptions
```

### Prefer / Avoid

- Prefer: specific exception types; `throw;`
- Avoid: empty catches; `throw ex;`; exceptions for normal control flow

---

## Logging

### What it is

Record meaningful events at **boundaries** (process start, handled failures) with a level/type and message. Business code should not swallow errors silently.

### How the sample code works

**Sample:** [`samples/Logging/`](../samples/Logging/)

1. Register `DatabaseContext`, `IExceptionLogRepository`, `ILogService` via the teaching DI helper
2. `LogService` takes `IExceptionLogRepository` in its **constructor** (injection)
3. Log an Info entry, throw a simulated app error, catch and log Error, then print stored logs via `GetLogs()`

```bash
dotnet run --project samples/Logging
```

### Prefer / Avoid

- Prefer: log at boundaries; constructor-injected log services
- Avoid: logging secrets; `throw ex;` after logging when you meant to preserve the stack
