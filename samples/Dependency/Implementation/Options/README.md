# Options (in this sample)

Strongly typed settings via `IOptions<T>` — Prefer bind from **`appsettings.json`** + validate at the composition root; Avoid mutable statics.

## Why this example

A tax **rate + currency** in JSON is the real-world Options shape: edit the file, bind a section, inject `IOptions<T>`.

## Files

| File | Role |
| --- | --- |
| [`appsettings.json`](../../appsettings.json) | Source of truth for `Pricing` |
| `PricingOptions.cs` | POCO matching the JSON section |
| `PricingService.cs` | Prefer — `IOptions<PricingOptions>` |
| `StaticPricingConfig` | Avoid — global mutable config |

## Explaining the bind

```csharp
.Bind(configuration.GetSection("Pricing"))
```

Maps JSON properties (`Rate`, `Currency`) onto `PricingOptions` by name. Business code only sees `_options.Rate` — never file paths or `"Pricing:Rate"` strings.

## Prefer

- Keep settings in `appsettings.json` (or env overrides)
- `IOptions<T>` from DI; validate at startup

## Avoid

- `public static double Rate { get; set; }` as app config
- Opening JSON inside every service

## Run

```bash
dotnet run --project samples/Dependency
```

Edit `appsettings.json` → re-run to confirm Options picks up the file.

## How the code works

[docs/10-options-pattern.md](../../../../docs/10-options-pattern.md)
