# Entity Framework

Entity Framework Core practices for tracking, detach, and repository-style data access — keep work on `IQueryable` where possible and be explicit about entity state.

```bash
dotnet run --project samples/EntityFramework
```

## Why this example

Edit → detach → save makes “Change Tracker ignored this” visible in one run.

## Explaining detach

A tracked entity’s property edits are saved by `SaveChanges`. After `Detach`, the same edits are local only — the tracker no longer owns the instance.

**Takeaway:** be explicit about entity state when you do not want a write-back.

## What’s here

- Detach / repository helpers under `Detach/Repository`
- Complements soft-delete and query patterns in `DataManagement`

## Guide

See [docs/04-linq-and-ef.md](../../docs/04-linq-and-ef.md).

## How the code works

Step-by-step explanation of this sample: [docs/04-linq-and-ef.md#ef-detach](../../docs/04-linq-and-ef.md#ef-detach).
