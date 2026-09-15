# Logging

Log at boundaries with clear levels and context; prefer structured messages over silent failures or swallowing exceptions.

```bash
dotnet run --project samples/Logging
```

## Why this example

Info → simulated Error → read-back shows inject → write at boundaries → verify, without a real logging SaaS.

## Explaining `LogService`

Ctor-injected `IExceptionLogRepository`. Log Info at start, catch a simulated failure as Error, then `GetLogs()` to prove persistence. Prefer boundary logging; avoid secrets in messages.

**Takeaway:** inject the log store; log at boundaries; verify you can read it back.

## What’s here

- `ILogService` / `LogService` and exception log repository
- Wiring via the teaching DI helper (hardening of EF API mix continues)

## Guide

See [docs/05-exceptions-and-logging.md](../../docs/05-exceptions-and-logging.md).

## How the code works

Step-by-step explanation of this sample: [docs/05-exceptions-and-logging.md#logging](../../docs/05-exceptions-and-logging.md#logging).
