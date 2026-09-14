# Audit entry

Capturing change history for entities — who changed what, when, and which properties moved — as a foundation for auditable data access.

```bash
dotnet run --project samples/AuditEntry
```

## Why this example

Overriding `SaveChangesAsync` is the usual production hook for auditing — property old/new values show why Change Tracker is the right place.

## What’s here

- `AuditEntry` / `AuditEntryProperty` models
- EF context setup for audit records

## Guide

See [docs/04-linq-and-ef.md](../../docs/04-linq-and-ef.md).

## How the code works

Step-by-step explanation of this sample: [docs/04-linq-and-ef.md#audit-trail](../../docs/04-linq-and-ef.md#audit-trail).
