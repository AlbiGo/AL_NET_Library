# Reflection

## What it is

**Reflection** lets code inspect assemblies, types, and members at runtime (`typeof`, `GetType()`, `PropertyInfo`, `Activator`, custom attributes). Compilers and frameworks use it heavily; application code should use it sparingly and deliberately.

## Why this example

Two demos side by side:

1. **Plugin discovery** — a Prefer use: types opt in with `[Plugin]` + `IPlugin`; the scanner finds them without Main listing concrete classes.
2. **Property access** — Prefer direct members (or cached `PropertyInfo`) vs Avoid uncached magic-string lookups.

## How the sample code works

**Sample:** [`samples/AdvancedFeatures/Reflection/`](../samples/AdvancedFeatures/Reflection/)

### Prefer — discover plugins

```csharp
var plugins = PluginScanner.Discover(typeof(PluginScanner).Assembly);
foreach (var plugin in plugins)
    Console.WriteLine(plugin.Describe());
```

`PluginScanner` keeps types that:

- are concrete
- implement `IPlugin`
- carry `[Plugin("…")]`

then `Activator.CreateInstance` and returns `IPlugin` instances.

### Prefer / Avoid — reading a property

```csharp
PreferDirect(person);              // person.Name
PreferCachedReflection(person);  // cached PropertyInfo + nameof
AvoidUncachedMagicString(person);  // GetProperty("Name") every call
```

### Explaining `PluginScanner`

Like `CarServices` for delegates: this is the **work** reflection is good at — runtime discovery when the set of types is not fixed at compile time. Main never writes `new HelloPlugin()`.

### Explaining `PropertyAccessDemo`

When you already know `Person`, use `person.Name`. If a framework must reflect, cache `PropertyInfo` once and prefer `nameof(Person.Name)` over `"Name"`.

```bash
dotnet run --project samples/AdvancedFeatures
```

Look for the **Reflection** section in the console output.

## Prefer

- Attribute- or interface-based discovery (plugins, serializers, DI conventions)
- `nameof` / `typeof`; cache metadata used repeatedly
- Call through a known interface after `Activator` when possible

## Avoid

- Reflection for ordinary domain get/set
- Uncached `GetProperty` / `GetMethod` in hot loops
- Magic strings that break under rename
- Bypassing access modifiers without a framework-level reason
