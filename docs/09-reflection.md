# Reflection

## What it is

**Reflection** lets code inspect assemblies, types, and members at runtime (`typeof`, `GetType()`, `PropertyInfo`, `Activator`, custom attributes). Compilers and frameworks use it heavily; application code should use it sparingly and deliberately.

## Why this example

Three demos:

1. **Plugin discovery** — Prefer: types opt in with `[Plugin]` + `IPlugin`.
2. **Property access** — Prefer direct members (or cached `PropertyInfo`) vs Avoid uncached magic strings.
3. **Dynamic JSON** — Prefer `JsonNode` / `JsonElement` by property name when the JSON shape always changes; Avoid `Type.GetProperty` on a deserialized `object`.

## How the sample code works

**Sample:** [`samples/AdvancedFeatures/Reflection/`](../samples/AdvancedFeatures/Reflection/)

### Prefer — discover plugins

```csharp
var plugins = PluginScanner.Discover(typeof(PluginScanner).Assembly);
foreach (var plugin in plugins)
    Console.WriteLine(plugin.Describe());
```

### Prefer / Avoid — reading a CLR property

```csharp
PreferDirect(person);              // person.Name
PreferCachedReflection(person);  // cached PropertyInfo + nameof
AvoidUncachedMagicString(person);  // GetProperty("Name") every call
```

### Prefer / Avoid — value by name from varying JSON

Payloads are loaded from files under `Reflection/`:

- [`sample-order.json`](../samples/AdvancedFeatures/Reflection/sample-order.json) → `orderId`
- [`sample-user.json`](../samples/AdvancedFeatures/Reflection/sample-user.json) → `email`
- [`sample-sensor.json`](../samples/AdvancedFeatures/Reflection/sample-sensor.json) → `celsius`

```csharp
var orderJson = File.ReadAllText(Path.Combine(reflectionDir, "sample-order.json"));
DynamicJsonLookup.GetByPropertyName(orderJson, "orderId");
```

Why CLR `GetProperty` fails on JSON (step-by-step):

```csharp
DynamicJsonLookup.ClrGetPropertyOnJson(userJson, "email");
// Deserialize<object> → usually JsonElement
// GetProperty("email") looks for a C# property on JsonElement → null
// GetByPropertyName(userJson, "email") → ada@example.com
```

Search by property + value (returns the matching object):

```csharp
DynamicJsonLookup.GetObjectByPropertyValue(itemsJson, "email", "ada@example.com");
// → {"type":"user","email":"ada@example.com","role":"admin"}
```

### Explaining `PluginScanner`

Runtime discovery when the set of types is not fixed at compile time. Main never writes `new HelloPlugin()`.

### Explaining `PropertyAccessDemo`

When you already know `Person`, use `person.Name`. If a framework must reflect, cache `PropertyInfo` and prefer `nameof`.

### Explaining `DynamicJsonLookup`

Reflection answers “what members does this .NET type have?” JSON APIs answer “what keys does this document have?” For always-different JSON, Prefer `JsonNode` / `JsonElement`.

```bash
dotnet run --project samples/AdvancedFeatures
```

Look for **Reflection** and **Dynamic JSON** in the console output.

## Prefer

- Attribute- or interface-based discovery (plugins, serializers, DI conventions)
- `nameof` / `typeof`; cache metadata used repeatedly
- Call through a known interface after `Activator` when possible
- `JsonNode` / `JsonElement` to read a property by name from dynamic JSON

## Avoid

- Reflection for ordinary domain get/set
- Uncached `GetProperty` / `GetMethod` in hot loops
- Magic strings that break under rename
- Using reflection to read JSON keys after `Deserialize<object>`
- Bypassing access modifiers without a framework-level reason
