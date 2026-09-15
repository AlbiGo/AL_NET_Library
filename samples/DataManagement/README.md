# Data management

CRUD and soft-delete with clear timestamps and navigation handling; load SQL from files and bind parameters separately — never concatenate values into SQL.

```bash
dotnet run --project samples/DataManagement
```

## Why this example

Multi-entity soft-delete forces cascading via EF navigations (the hard part). File SQL + `@params` proves values never enter the SQL string.

## Explaining soft-delete and `QueryBuilder`

**Soft-delete:** mark `Deleted`/`Updated` (UTC) instead of removing rows; cascade with EF navigation metadata (`IsCollection`), not type-name heuristics.

**`QueryBuilder`:** returns SQL text + parameter objects. Bind values separately — never `Replace` user input into the SQL string.

**Takeaway:** soft-delete via metadata; parameterized SQL always.

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
