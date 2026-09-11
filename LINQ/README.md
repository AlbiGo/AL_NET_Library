# LINQ

Queryable filters, pagination, and joins against EF-style models.

```bash
dotnet run --project LINQ
```

## Do

- 1-based pagination: `Skip((page - 1) * size).Take(size)`
- Null/empty-safe name filters
- Joins that project **both** sides into a DTO

## Don’t

- `Skip(page)` when `page` is a page number
- Using a navigation after the join result discarded the related entity

## Guide

See [docs/04-linq-and-ef.md](../docs/04-linq-and-ef.md).
