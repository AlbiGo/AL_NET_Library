# LINQ

Keep filters on `IQueryable` so EF can translate to SQL, paginate with `(page - 1) * size`, and project joins into DTOs from both sides.

```bash
dotnet run --project samples/LINQ
```

## Why this example

Filter, pagination, and join are the three LINQ mistakes teams hit most — shown in memory here, with EF versions in `LamdaMethods`.

## Explaining pagination and joins

- **Pagination:** humans use 1-based pages → `Skip((page - 1) * size).Take(size)`. Never `Skip(page)` for a page number.
- **Joins:** project both sides (or a DTO). Discarding `entity2` in the select makes related data unreachable.
- **Filters:** keep predicates on `IQueryable`; null/empty-safe name checks.

**Takeaway:** correct skip math + keep both join sides + stay on `IQueryable` for EF.

## Do

- 1-based pagination: `Skip((page - 1) * size).Take(size)`
- Null/empty-safe name filters
- Joins that project **both** sides into a DTO

## Don’t

- `Skip(page)` when `page` is a page number
- Using a navigation after the join result discarded the related entity

## Guide

See [docs/04-linq-and-ef.md](../../docs/04-linq-and-ef.md).

## How the code works

Step-by-step explanation of this sample: [docs/04-linq-and-ef.md](../../docs/04-linq-and-ef.md).
