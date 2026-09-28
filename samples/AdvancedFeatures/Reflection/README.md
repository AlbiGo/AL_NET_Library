# Reflection

**Reflection** inspects and uses types/members at runtime (`Type`, `PropertyInfo`, `Activator`, attributes).

## Why this example

- **Plugins** — attribute + interface discovery is a real Prefer use (types opt in; Main never hard-codes them).
- **Property access** — Prefer direct/`nameof`+cache vs Avoid uncached magic strings.
- **Dynamic JSON** — payload shape changes; you only know the property *name* → Prefer `JsonNode` / `JsonElement`, not `Type.GetProperty`.

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

## Explaining `DynamicJsonLookup`

When JSON is always different (order vs user vs sensor), you cannot bind a fixed C# class.

Test files (copied to output on build):

| File | Sample key |
| --- | --- |
| [`sample-order.json`](sample-order.json) | single object → `orderId` |
| [`sample-user.json`](sample-user.json) | single object → `email` |
| [`sample-sensor.json`](sample-sensor.json) | single object → `celsius` |
| [`sample-items.json`](sample-items.json) | **array of objects** → `type` / `email` per item |

| Method | Role |
| --- | --- |
| `GetByPropertyName` | Get by key |
| `GetByPropertyNameFromEach` | Get by key on each array item |
| `GetObjectByPropertyValue` | Property + value → matching object |
| `GetWithJsonElement` | Same get-by-name via `JsonDocument` |
| `ClrGetPropertyOnJson` | Shows why `GetProperty` on JSON `object` fails |

```csharp
var json = File.ReadAllText(Path.Combine(dir, "sample-user.json"));
DynamicJsonLookup.GetByPropertyName(json, "email"); // → ada@example.com
```

Edit a sample JSON file and re-run to confirm lookup by name still works.

**Takeaway:** reflection finds .NET members; JSON APIs find JSON properties.

## Prefer

- Attribute / interface discovery for plugins and frameworks
- `nameof` / `typeof` instead of raw magic strings
- Cache `Type` / `PropertyInfo` / `MethodInfo` on hot paths
- `JsonNode` / `JsonElement` when reading a property by name from varying JSON

## Avoid

- Reflection for normal property get/set when compile-time access works
- Uncached `GetProperty("X")` in loops
- `Type.GetProperty` on deserialized JSON `object` to read JSON keys
- Using reflection to bypass encapsulation without a strong reason

## Run

```bash
dotnet run --project samples/AdvancedFeatures
```

Look for the **Reflection** section (including Dynamic JSON) in console output.

## How the code works

Full walkthrough: [docs/09-reflection.md](../../../docs/09-reflection.md).
