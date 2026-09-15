# Reflection

**Reflection** inspects and uses types/members at runtime (`Type`, `PropertyInfo`, `Activator`, attributes).

## Why this example

- **Plugins** — attribute + interface discovery is a real Prefer use (types opt in; Main never hard-codes them).
- **Property access** — shows Prefer direct/`nameof`+cache vs Avoid uncached magic strings.

## Explaining `PluginScanner`

Not everyday business logic — the Prefer “work” for reflection: scan an assembly, keep types that implement `IPlugin` and carry `[Plugin]`, `Activator.CreateInstance`, call through the interface.

`HelloPlugin` / `EchoPlugin` are the concrete work (like `CarServices` steps). The scanner never lists them by name.

```text
[Plugin] HelloPlugin  ─┐
[Plugin] EchoPlugin   ─┼─►  PluginScanner.Discover(assembly)  ─►  IPlugin.Describe()
```

## Explaining `PropertyAccessDemo`

| Method | Verdict |
| --- | --- |
| `PreferDirect` | Use when the type is known |
| `PreferCachedReflection` | OK when you must reflect — cache `PropertyInfo` |
| `AvoidUncachedMagicString` | Look up `"Name"` every call — slow and brittle |

## Prefer

- Attribute / interface discovery for plugins and frameworks
- `nameof` / `typeof` instead of raw magic strings
- Cache `Type` / `PropertyInfo` / `MethodInfo` on hot paths

## Avoid

- Reflection for normal property get/set when compile-time access works
- Uncached `GetProperty("X")` in loops
- Using reflection to bypass encapsulation without a strong reason

## Run

```bash
dotnet run --project samples/AdvancedFeatures
```

Look for the **Reflection** section in console output.

## How the code works

Full walkthrough: [docs/09-reflection.md](../../../docs/09-reflection.md).
