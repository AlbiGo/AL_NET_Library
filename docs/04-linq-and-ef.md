# LINQ, EF, and data access

## LINQ

### What it is

**LINQ** queries collections and databases with a consistent syntax. Against EF, keep work as `IQueryable` so filters translate to SQL.

### Why this example

**Filter + pagination + join** are the three LINQ mistakes teams hit most (wrong `Skip`, null filters, discarding a join side). An in-memory demo proves the math; `LamdaMethods` shows the same ideas against EF.

### How the sample code works

**Sample:** [`samples/LINQ/`](../samples/LINQ/)

`Program.cs` shows the rules in memory:

```csharp
.Where(e => e.Name.Contains(filter.Name))
.Skip((filter.Page - 1) * filter.Size)  // page is 1-based
.Take(filter.Size);
```

EF-backed versions of filter/join/pagination live in `Lamda/LamdaMethods.cs`:

- `GetWhere` / `GetAllPagination` — correct skip math
- `GetJoin` — projects **both** join sides into a DTO (does not discard `entity2`)

### Explaining pagination and joins

**Pagination:** page numbers are 1-based for humans. Offset is `(page - 1) * size`. `Skip(page)` when `page` is a page number skips the wrong number of rows.

**Joins:** if the projection keeps only `entity1`, you cannot use `entity2` afterward. Project both sides (or a DTO) when both matter.

```bash
dotnet run --project samples/LINQ
```

---

## Repositories and soft-delete

### What it is

A **repository** hides DbContext details. **Soft-delete** sets `Deleted`/`Updated` instead of removing the row, often cascading to related navigations.

### Why this example

Related entities (`Entity1`…`Entity4`) force the hard part of soft-delete: cascading marks through navigations using EF metadata, not string heuristics. A single-table delete would hide that.

### How the sample code works

**Sample:** [`samples/DataManagement/`](../samples/DataManagement/)

1. Seed `Entity1` with related `Entity2` / `Entity3` / `Entity4`
2. Call `SoftRemoveRelated(entity, new[] { "Entity2", "Entity3s", "Entity4s" })`
3. Print `Deleted` timestamps — related rows are marked, not hard-deleted

`BaseRepository` uses EF metadata (`navigation.IsCollection`) rather than fragile type-name checks, and stamps times in **UTC**.

### Explaining soft-delete

Hard-delete removes rows. Soft-delete marks `Deleted` (and usually `Updated`) so history stays queryable. Cascading through navigations with EF metadata is the hard part this sample exists to show.

```bash
dotnet run --project samples/DataManagement
```

---

## Parameterized SQL

### What it is

Load SQL from files and bind values as **parameters**. Never concatenate or `Replace` user input into the SQL string.

### Why this example

A file-based query with `@nameParam` mirrors how production ADO.NET/Dapper apps keep SQL out of C# strings while still proving values never enter the SQL text.

### How the sample code works

`QueryBuilder.BuildQuery` returns `BuiltQuery` with `Sql` + `Parameters`. The sample SQL uses `@nameParam`; the value is bound separately via `CreateCommand`.

### Explaining `QueryBuilder`

SQL text stays in a file (or constant). Values never enter the string — they travel as parameters. That blocks injection and keeps plans reusable.

---

## EF detach

### What it is

**Detach** sets an entity’s state to `Detached`. Change Tracker no longer tracks it, so `SaveChanges` ignores local edits.

### Why this example

Editing email then detaching makes the “change vanished” outcome visible in one run — clearer than explaining Change Tracker theory alone.

### How the sample code works

**Sample:** [`samples/EntityFramework/`](../samples/EntityFramework/)

```csharp
user.Email = "changed@example.com";
repo.Detach(user);
await repo.SaveChanges();  // change does NOT persist
```

One shared `AuditDbContext("detach-demo")` is passed into the repository so all operations hit the same in-memory store.

### Explaining detach

Tracked entities are saved. Detached entities are ignored by `SaveChanges`. Use detach when you want a local copy that must not write back.

```bash
dotnet run --project samples/EntityFramework
```

---

## Audit trail

### What it is

On save, record **who/what changed**: entity name, state, and per-property old/new values.

### Why this example

Overriding `SaveChangesAsync` is how many apps add auditing without scattering log calls through every service. Property-level old/new values show why Change Tracker is the right hook.

### How the sample code works

**Sample:** [`samples/AuditEntry/`](../samples/AuditEntry/)

`AuditDbContext.SaveChangesAsync` overrides the base method, walks `ChangeTracker` entries, and inserts `AuditEntry` + `AuditEntryProperty` rows before calling `base.SaveChangesAsync`.

### Explaining `AuditDbContext`

Centralizing audit in `SaveChangesAsync` means services do not sprinkle log calls after every update. Old/new property values come from the Change Tracker — the right hook for “what changed.”

```bash
dotnet run --project samples/AuditEntry
```

---

## Prefer

- Keep filters on `IQueryable`; 1-based pagination; parameterized SQL; UTC soft-delete; shared context in demos

## Avoid

- `.Compile()` on EF `IQueryable`; `Skip(page)` for page numbers; string-replaced SQL; mixing EF6 and EF Core APIs
