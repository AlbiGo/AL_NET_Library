# Custom LINQ

Build your own operators with `yield return` for **deferred** execution (work runs when enumerated).

## Why this example

`WherePositive` on a tiny int list is the smallest deferred-operator demo — no EF required.

## Explaining `LinqExt.WherePositive`

```csharp
foreach (int element in source)
{
    if (element > 0)
        yield return element;  // pause; hand value to caller
}
```

Nothing runs until `foreach` / `ToList`. Same laziness idea as LINQ’s `Where`.

**Takeaway:** custom LINQ = normal methods + `yield`; deferred by default.

## Run

Included in `dotnet run --project samples/AdvancedFeatures` (Custom LINQ section).

## How the code works

[docs/08](../../../docs/08-generics-expressions-compiled-queries.md#custom-linq)
