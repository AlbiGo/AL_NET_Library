# Advanced features

Advanced C# techniques used in real apps: delegates, events, expression trees, generics, and compiled queries.

```bash
dotnet run --project samples/AdvancedFeatures
```

## Topics

| Topic | Description |
| --- | --- |
| **Delegates** | Type-safe callbacks / multicast pipelines — [`Delegates/`](Delegates/) |
| **Events** | Pub-sub on top of delegates — [`Events/`](Events/) |
| **Expression trees** | Keep filters as `Expression` on `IQueryable` — [`Expressions/`](Expressions/) |
| **Generics** | Behavior on the type (`Calculate`), not `switch` on `T` — [`Generics/`](Generics/) |
| **Custom LINQ** | `yield return` operators — [`Linq/`](Linq/) |
| **Compiled queries** | `EF.CompileQuery` for hot paths — [`CompiledQueries/`](CompiledQueries/) |

## How the code works

Step-by-step explanation of this sample: [docs/07-delegates-and-events.md](../../docs/07-delegates-and-events.md).

Also: [docs/08-generics-expressions-compiled-queries.md](../../docs/08-generics-expressions-compiled-queries.md).
