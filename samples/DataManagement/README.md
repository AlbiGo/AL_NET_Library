# Data management

CRUD and soft-delete with clear timestamps and navigation handling; load SQL from files and bind parameters separately — never concatenate values into SQL.

```bash
dotnet run --project samples/DataManagement
```

## Why this example

Multi-entity soft-delete forces cascading via EF navigations (the hard part). File SQL + `@params` proves values never enter the SQL string.

## Do

- Soft-delete with UTC timestamps and EF navigation metadata (`IsCollection` / references)
- **`QueryBuilder`** — load SQL files and bind parameters (no string replacement of values)
- Keep query text and parameter values separate for ADO.NET / Dapper

## Don’t

- Concatenate or `Replace` user values into SQL strings
- Detect collections by type name heuristics (`Contains("List")`)

## Guide

See [docs/04-linq-and-ef.md](../../docs/04-linq-and-ef.md).

## How the code works

Step-by-step explanation of this sample: [docs/04-linq-and-ef.md](../../docs/04-linq-and-ef.md).
