# Exceptions and logging

## Prefer

- Catch only what you can handle; otherwise let exceptions bubble
- Re-throw with `throw;` to preserve the stack trace (not `throw ex;`)
- Log with structured context (message, level, correlation) at boundaries
- Use specific exception types when callers can react differently
- Fail fast on programmer errors (`ArgumentNullException`, etc.)

## Avoid

- Empty `catch` blocks that swallow failures
- `throw ex;` (resets the stack trace)
- Using exceptions for normal control flow
- Logging secrets or huge payloads
- Catching `Exception` everywhere “just in case” without rethrow or recovery

## See samples

- [`Exceptions/`](../Exceptions/) — catch / log patterns (hardening continues in Phase 2)
- [`Logging/`](../Logging/) — log service + repository wiring

```bash
dotnet run --project Exceptions
dotnet run --project Logging
```
