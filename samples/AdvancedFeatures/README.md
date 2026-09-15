# Advanced features

Advanced C# techniques used in real apps: delegates, events, expression trees, generics, compiled queries, and reflection.

```bash
dotnet run --project samples/AdvancedFeatures
```

## Why these examples

| Topic | Why we chose it |
| --- | --- |
| Delegates | Garage pipeline = ordered void steps from many classes |
| Events | Task created/completed = one raise, many side effects + payload |
| Expression trees | Student filter shows `.Compile()` breaking EF translation |
| Generics | Data1/Data2 `Calculate()` vs switch-on-`T` anti-pattern |
| Custom LINQ | Smallest deferred `yield` demo |
| Compiled queries | Hot-path name/date filter shape |
| Reflection | Plugins opt in with attributes; property access Prefer vs Avoid |

## Explaining the key types

| Topic | Key type | What to remember |
| --- | --- | --- |
| Delegates | [`CarServices`](Delegates/CarServices.cs) | The **work** the delegate points at — matching `void()` methods, not the garage |
| Events | [`TaskService`](Events/TaskService.cs) | Publisher owns raise; subscribers only `+=`; payload in `TaskEventArgs` |
| Expression trees | [`ExpressionTrees`](Expressions/ExpressionTrees.cs) | Keep `Expression` for `IQueryable`; `.Compile()` is in-memory only |
| Generics | [`GenericServices<T>`](Generics/Implementation/GenericServices.cs) | Constrain `T`, call `Calculate()` — never `switch` on `T` |
| Custom LINQ | [`LinqExt`](Linq/LinqExt.cs) | `yield return` = deferred until enumerated |
| Compiled queries | [`CompiledQueryEx`](CompiledQueries/CompiledQueryEx.cs) | Compile shape once; reuse with new parameters |
| Reflection | [`PluginScanner`](Reflection/PluginScanner.cs) | Discover `[Plugin]` + `IPlugin`; Prefer direct members when the type is known |

### Reflection — `PluginScanner`

Scans an assembly for concrete `IPlugin` types marked `[Plugin]`, activates them, calls through the interface. Main never hard-codes `HelloPlugin` / `EchoPlugin`.

`PropertyAccessDemo` contrasts direct access, cached `PropertyInfo`, and Avoid uncached magic strings.

Full write-up: [Reflection/README.md](Reflection/README.md).

## Topics

| Topic | Description |
| --- | --- |
| **Delegates** | Type-safe callbacks / multicast pipelines — [`Delegates/`](Delegates/) |
| **Events** | Pub-sub on top of delegates — [`Events/`](Events/) |
| **Expression trees** | Keep filters as `Expression` on `IQueryable` — [`Expressions/`](Expressions/) |
| **Generics** | Behavior on the type (`Calculate`), not `switch` on `T` — [`Generics/`](Generics/) |
| **Custom LINQ** | `yield return` operators — [`Linq/`](Linq/) |
| **Compiled queries** | `EF.CompileQuery` for hot paths — [`CompiledQueries/`](CompiledQueries/) |
| **Reflection** | Runtime type discovery + Prefer/Avoid property access — [`Reflection/`](Reflection/) |

## How the code works

Step-by-step explanation of this sample: [docs/07-delegates-and-events.md](../../docs/07-delegates-and-events.md).

Also: [docs/08-generics-expressions-compiled-queries.md](../../docs/08-generics-expressions-compiled-queries.md), [docs/09-reflection.md](../../docs/09-reflection.md).
