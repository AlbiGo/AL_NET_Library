# LINQ, EF, and data access

## LINQ

### What it is

**LINQ** queries collections and databases with a consistent syntax. Against EF, keep work as `IQueryable` so filters translate to SQL.

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

```bash
dotnet run --project samples/LINQ
```

---

## Repositories and soft-delete

### What it is

A **repository** hides DbContext details. **Soft-delete** sets `Deleted`/`Updated` instead of removing the row, often cascading to related navigations.

### How the sample code works

**Sample:** [`samples/DataManagement/`](../samples/DataManagement/)

1. Seed `Entity1` with related `Entity2` / `Entity3` / `Entity4`
2. Call `SoftRemoveRelated(entity, new[] { "Entity2", "Entity3s", "Entity4s" })`
3. Print `Deleted` timestamps — related rows are marked, not hard-deleted

`BaseRepository` uses EF metadata (`navigation.IsCollection`) rather than fragile type-name checks, and stamps times in **UTC**.

```bash
dotnet run --project samples/DataManagement
```

---

## Parameterized SQL

### What it is

Load SQL from files and bind values as **parameters**. Never concatenate or `Replace` user input into the SQL string.

### How the sample code works

`QueryBuilder.BuildQuery` returns `BuiltQuery` with `Sql` + `Parameters`. The sample SQL uses `@nameParam`; the value is bound separately via `CreateCommand`.

---

## EF detach

### What it is

**Detach** sets an entity’s state to `Detached`. Change Tracker no longer tracks it, so `SaveChanges` ignores local edits.

### How the sample code works

**Sample:** [`samples/EntityFramework/`](../samples/EntityFramework/)

```csharp
user.Email = "changed@example.com";
repo.Detach(user);
await repo.SaveChanges();  // change does NOT persist
```

One shared `AuditDbContext("detach-demo")` is passed into the repository so all operations hit the same in-memory store.

```bash
dotnet run --project samples/EntityFramework
```

---

## Audit trail

### What it is

On save, record **who/what changed**: entity name, state, and per-property old/new values.

### How the sample code works

**Sample:** [`samples/AuditEntry/`](../samples/AuditEntry/)

`AuditDbContext.SaveChangesAsync` overrides the base method, walks `ChangeTracker` entries, and inserts `AuditEntry` + `AuditEntryProperty` rows before calling `base.SaveChangesAsync`.

```bash
dotnet run --project samples/AuditEntry
```

---

## Prefer

- Keep filters on `IQueryable`; 1-based pagination; parameterized SQL; UTC soft-delete; shared context in demos

## Avoid

- `.Compile()` on EF `IQueryable`; `Skip(page)` for page numbers; string-replaced SQL; mixing EF6 and EF Core APIs
