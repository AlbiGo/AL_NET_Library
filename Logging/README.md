# Logging

Log at boundaries with clear levels and context; prefer structured messages over silent failures or swallowing exceptions.

```bash
dotnet run --project Logging
```

## What’s here

- `ILogService` / `LogService` and exception log repository
- Wiring via the teaching DI helper (hardening of EF API mix continues)

## Guide

See [docs/05-exceptions-and-logging.md](../docs/05-exceptions-and-logging.md).
