# Compiled queries

`EF.CompileQuery` translates a LINQ query **once** and reuses that plan with new parameters.

## Why this example

Name + created-date filtering is a repeated hot-path shape — enough to show `CompileQuery` without implying every query should be compiled.

## Explaining `CompiledQueryEx`

`FilterQuery` is a `static readonly` `Func<DatabaseContext, string, DateTime, IEnumerable<Entity1>>` built by `EF.CompileQuery` when the type loads.

- **Without compile** — EF rebuilds/translates the same expression tree on every call
- **With compile** — translation happens once; later calls only bind fresh parameter values

`Filter(context, filter)` just invokes that cached delegate. Pass an **open** `DatabaseContext` — do not open a new context inside the helper and ignore the caller.

**Takeaway:** compile hot-path shapes; pass live context + parameters.

## How this file works

| Piece | Role |
| --- | --- |
| `Filter` | Holds `FilterTerm` + `Created` for one call |
| `FilterQuery` | `static readonly` compiled delegate (built at type load) |
| `EF.CompileQuery(...)` | Turns the expression into that reusable `Func<…>` |
| `Filter(context, filter)` | Invokes the compiled delegate with live values |

See comments inside [`CompiledQueryEx.cs`](CompiledQueryEx.cs) and [docs/08](../../../docs/08-generics-expressions-compiled-queries.md#compiled-queries).

## Prefer

- Compile queries you run often with different parameters
- Pass in an existing `DbContext`

## Avoid

- Compiling one-off queries (extra complexity, little gain)
- Opening a new context inside the helper and ignoring caller state
