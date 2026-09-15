# Options pattern

## What it is

The **Options pattern** binds configuration into a strongly typed class (`PricingOptions`) and injects it with `IOptions<T>` / `IOptionsSnapshot<T>` / `IOptionsMonitor<T>`. The composition root owns binding and validation; business code only reads the typed object.

## Why it matters

- One place to bind and validate settings
- Testable — inject fake `IOptions<T>` without static globals
- Same DI lifetimes story as the rest of the app

## Why this example

**Pricing tax rate + currency** lives in [`appsettings.json`](../samples/Dependency/appsettings.json). Prefer binds that JSON section into `PricingOptions` and injects `IOptions<PricingOptions>`. Avoid uses a mutable `StaticPricingConfig` — no file, no validation, easy to mutate from anywhere.

## How the sample code works

**Sample:** [`samples/Dependency/`](../samples/Dependency/)

### 1. Save settings in JSON

[`appsettings.json`](../samples/Dependency/appsettings.json):

```json
{
  "Pricing": {
    "Rate": 0.20,
    "Currency": "EUR"
  }
}
```

Copied to the output folder so `AppContext.BaseDirectory` can find it at runtime.

### 2. Prefer — load, bind, validate, inject

In `AppServices.Configure`:

```csharp
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

services.AddOptions<PricingOptions>()
    .Bind(configuration.GetSection("Pricing"))
    .Validate(o => o.Rate is > 0 and <= 1.0, "…")
    .ValidateOnStart();

services.AddTransient<PricingService>();
```

`PricingService` takes `IOptions<PricingOptions>` and uses `options.Value` — it never opens the JSON file itself.

### Avoid — static config

`StaticPricingConfig.Rate` is global and mutable. `PricingServiceAvoid` reads it directly. Demo mutates the static after use to show the hazard.

### Explaining `IOptions<T>`

| Type | Typical use |
| --- | --- |
| `IOptions<T>` | Singleton snapshot from startup — fine for most services |
| `IOptionsSnapshot<T>` | Scoped — re-reads per scope/request when using config reload |
| `IOptionsMonitor<T>` | Singleton + change notifications |

This sample uses `IOptions<T>` — enough to teach Prefer vs static.

```bash
dotnet run --project samples/Dependency
```

Look for the **Options pattern** sections. Change `Rate` in `appsettings.json` and re-run to see the new value.

## Prefer

- Settings in JSON (or env); strongly typed options classes
- Bind at the composition root; `IOptions<T>` in services
- `.Validate(...)` / fail fast at startup

## Avoid

- Mutable static config bags
- Scattering `Configuration["key"]` string lookups through business code
- Skipping validation until a request fails in production
