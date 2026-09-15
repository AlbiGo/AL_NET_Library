# Generics

Write once for many types. Constraints (`where T : IMainData`) say what `T` can do; subtypes supply the behavior.

## Why this example

`Data1` / `Data2` with `Calculate()` show Prefer. The anti-pattern is `switch` / cast on `T` inside the “generic” helper.

## Explaining `GenericServices<T>`

```csharp
public static class GenericServices<T> where T : IMainData
{
    public static void Calculate(T data) => data.Calculate();
}
```

The helper never mentions `Data1` or `Data2`. `where T : IMainData` guarantees `Calculate()` exists; the runtime type does the math.

If you `switch (data)` or `as Data1` inside here, every new type forces edits — you no longer have a real generic.

**Takeaway:** constrain `T`, call members on the constraint, let polymorphism do the rest.

## Run

Included in `dotnet run --project samples/AdvancedFeatures` (Generics section).

## How the code works

[docs/08](../../../docs/08-generics-expressions-compiled-queries.md#generics)
