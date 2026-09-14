# Generics, expression trees, and compiled queries

## Generics

### What it is

**Generics** let you write code once for many types (`List<T>`, `GenericServices<T>`). Constraints (`where T : IMainData`) describe what `T` can do.

### How the sample code works

**Sample:** [`samples/AdvancedFeatures/Generics/`](../samples/AdvancedFeatures/Generics/)

Anti-pattern (removed): `switch (data)` + cast to `Data1`/`Data2`.  
**Do instead:** each type implements `Calculate()` on `IMainData` / `MainData`.

```csharp
GenericServices<Data1>.Calculate(data1);  // calls data1.Calculate()
GenericServices<Data2>.Calculate(data2);  // calls data2.Calculate()
```

The generic helper stays tiny; polymorphism holds the type-specific math.

---

## Expression trees

### What it is

An **expression tree** (`Expression<Func<…>>`) is a data structure describing code. EF Core can translate it to SQL. Compiling it to a delegate forces **in-memory** execution.

### How the sample code works

**Sample:** [`samples/AdvancedFeatures/Expressions/`](../samples/AdvancedFeatures/Expressions/)

```csharp
students.AsQueryable().InlineFilter(filter);
// → query.Where(CreateExpressionTreeFromFilter(filter))  // keeps Expression
// NOT: query.Where(filterExp.Compile())                   // client-side only
```

`FilterBy` shows the alternative style: add optional `Where` clauses one at a time (often clearer for EF).

---

## Custom LINQ

### What it is

You can write your own operators with `yield return` for **deferred execution** (work runs when enumerated).

### How the sample code works

`LinqExt.WherePositive` walks the source and yields only values `> 0`. No extra enumerator tricks — just a clean iterator.

---

## Compiled queries

### What it is

`EF.CompileQuery` caches a query **shape** so repeated executions with different parameters avoid rebuilding the expression tree every time (useful on hot paths).

### How the sample code works

**File:** `samples/AdvancedFeatures/CompiledQueries/CompiledQueryEx.cs`

```csharp
private static readonly Func<DatabaseContext, string, DateTime, IEnumerable<Entity1>> FilterQuery =
    EF.CompileQuery((DatabaseContext context, string term, DateTime created) =>
        context.Entity1s.Where(p => p.Name.Contains(term) && p.Created > created));
```

Call `CompiledQueryEx.Filter(context, filter)` with an open `DatabaseContext`.

---

## Prefer

- Polymorphic methods under generic constraints; keep `Expression` on `IQueryable`; compile EF queries only for hot paths

## Avoid

- `switch`/`as` on `T` inside generic helpers; `.Compile()` for EF filters; opening a new DbContext inside an extension that ignores its input sequence

```bash
dotnet run --project samples/AdvancedFeatures
```
