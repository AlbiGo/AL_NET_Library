# Exceptions

Catch only what you handle, rethrow with `throw;` (not `throw ex;`), and don’t use exceptions for normal control flow.

```bash
dotnet run --project samples/Exceptions
```

## Why this example

A deliberate throw and nested rethrow make Prefer (`throw;`) vs Avoid (`throw ex;`) visible in the printed stack.

## What’s here

- Catch / log / continue patterns in `Program.cs`
- Analyzer feedback (e.g. CA2200) flags bad rethrows — prefer `throw;`

## Guide

See [docs/05-exceptions-and-logging.md](../../docs/05-exceptions-and-logging.md).

## How the code works

Step-by-step explanation of this sample: [docs/05-exceptions-and-logging.md#exceptions](../../docs/05-exceptions-and-logging.md#exceptions).
