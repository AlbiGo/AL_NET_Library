# LINQ and Entity Framework

## Prefer

- Keep filters as `Expression` / `IQueryable` so EF can translate to SQL
- 1-based page numbers with `Skip((page - 1) * size).Take(size)`
- Project joins into DTOs using both join sides (`(left, right) => new Dto { ... }`)
- Parameterized SQL for raw queries (never concatenate user input into SQL)
- Soft-delete with consistent timestamps (prefer UTC) and clear navigation handling

## Avoid

- Calling `.Compile()` on expressions used with `IQueryable` (forces client evaluation)
- `Skip(page)` when `page` is a page number (skips the wrong number of rows)
- Using navigation properties after a join that discarded the related entity
- String-replacing values into SQL templates
- Mixing EF6 (`System.Data.Entity`) and EF Core APIs in the same project

## See samples

- [`LINQ/`](../LINQ/) — pagination, filters, joins
- [`DataManagement/`](../DataManagement/) — repository, soft-delete, `QueryBuilder` parameters
- [`AdvancedFeatures/Expressions`](../AdvancedFeatures/Expressions/) — SQL-safe expression filters

```bash
dotnet run --project LINQ
dotnet run --project DataManagement
```
