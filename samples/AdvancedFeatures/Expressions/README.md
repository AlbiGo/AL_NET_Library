# Expression trees

An **expression tree** describes code as data (`Expression<Func<…>>`). Providers like EF Core can translate it to SQL. `.Compile()` turns it into an in-memory delegate.

## Why this example

Student age/email filters are a realistic dynamic `Where`. `.Compile()` “works” on lists and silently breaks EF translation — that contrast is the lesson.

## Explaining `ExpressionTrees`

`CreateExpressionTreeFromFilter` returns an `Expression<Func<Student, bool>>` — a tree of nodes, not a running method.

```csharp
students.AsQueryable().InlineFilter(filter);
// → Where(expression)     Prefer — stays translatable
// → Where(expression.Compile())  Avoid on IQueryable — client-side
```

Locals from the filter are captured into the tree so providers can parameterize them. Prefer keep the `Expression` on `IQueryable`.

**Takeaway:** expression = data for query providers; compiled delegate = in-memory only.

## Run

Included in `dotnet run --project samples/AdvancedFeatures` (Expression trees section).

## How the code works

[docs/08](../../../docs/08-generics-expressions-compiled-queries.md#expression-trees)
