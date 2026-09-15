# Dependency injection

Build the container once at a composition root, inject dependencies through constructors, use scopes for DbContext-style lifetimes, and bind settings with the Options pattern (`IOptions<T>`) instead of static config.

```bash
dotnet run --project samples/Dependency
```

## Why this example

A tiny `Controller → Service → Repo → Context` graph shows composition root, lifetimes, and constructor injection without ASP.NET/HTTP noise. `EconomicsControllerV2` is the manual-`new` contrast.

**Options:** `appsettings.json` → `PricingOptions` → `IOptions<T>` (Prefer). `StaticPricingConfig` is the Avoid contrast.

## Explaining `EconomicsController`

**Prefer:** ctor takes `IMathService`. Container builds the graph. Controller never `new`s collaborators.

**Avoid (`EconomicsControllerV2`):** manually `new MathService(new MathRepo(new MathDBContext()))` — brittle as the graph grows.

`AppServices` registers once and builds one `ServiceProvider`. Resolve inside a scope for scoped DbContext/repos.

**Takeaway:** ask for abstractions; composition root wires concretes.

## Explaining Options

| Type | Verdict |
| --- | --- |
| `PricingService(IOptions<PricingOptions>)` | Prefer — typed, injectable, validated at start |
| `StaticPricingConfig` / `PricingServiceAvoid` | Avoid — global mutable; no startup validation |

Details: [Implementation/Options/README.md](Implementation/Options/README.md) · [docs/10-options-pattern.md](../../docs/10-options-pattern.md)

## Do

- **`AppServices`** — register once, reuse one `ServiceProvider`, resolve with scopes; bind Options here
- **`EconomicsController`** — constructor injection of `IMathService`
- **`PricingService`** — constructor injection of `IOptions<PricingOptions>`
- Concept types in `Concept/` — pure constructor injection without a container

## Don’t (contrast)

- **`EconomicsControllerV2`** — manually `new`s the whole graph
- **`StaticPricingConfig`** — mutable static settings
- Rebuilding the container on every resolve

## Guide

- [docs/02-dependency-injection.md](../../docs/02-dependency-injection.md)
- [docs/10-options-pattern.md](../../docs/10-options-pattern.md)
