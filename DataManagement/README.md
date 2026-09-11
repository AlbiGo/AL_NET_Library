# Data management

Repository patterns, soft-delete, and safe raw SQL loading.

```bash
dotnet run --project DataManagement
```

## Do

- Soft-delete with UTC timestamps and EF navigation metadata (`IsCollection` / references)
- **`QueryBuilder`** — load SQL files and bind parameters (no string replacement of values)
- Keep query text and parameter values separate for ADO.NET / Dapper

## Don’t

- Concatenate or `Replace` user values into SQL strings
- Detect collections by type name heuristics (`Contains("List")`)

## Guide

See [docs/04-linq-and-ef.md](../docs/04-linq-and-ef.md).
