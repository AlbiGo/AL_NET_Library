# Dependency injection

Build the container once at a composition root, inject dependencies through constructors, use scopes for DbContext-style lifetimes, and avoid rebuilding the provider or manually `new`ing large graphs.

```bash
dotnet run --project samples/Dependency
```

## Why this example

A tiny `Controller → Service → Repo → Context` graph shows composition root, lifetimes, and constructor injection without ASP.NET/HTTP noise. `EconomicsControllerV2` is the manual-`new` contrast.

## Explaining `EconomicsController`

**Prefer:** ctor takes `IMathService`. Container builds the graph. Controller never `new`s collaborators.

**Avoid (`EconomicsControllerV2`):** manually `new MathService(new MathRepo(new MathDBContext()))` — brittle as the graph grows.

`AppServices` registers once and builds one `ServiceProvider`. Resolve inside a scope for scoped DbContext/repos.

**Takeaway:** ask for abstractions; composition root wires concretes.

## Do

- **`AppServices`** — register once, reuse one `ServiceProvider`, resolve with scopes
- **`EconomicsController`** — constructor injection of `IMathService`
- Concept types in `Concept/` — pure constructor injection without a container

## Don’t (contrast)

- **`EconomicsControllerV2`** — manually `new`s the whole graph; fine for a tiny demo, painful as dependencies grow
- Rebuilding the container on every resolve (removed from this sample)

## Guide

See [docs/02-dependency-injection.md](../../docs/02-dependency-injection.md).

## How the code works

Step-by-step explanation of this sample: [docs/02-dependency-injection.md](../../docs/02-dependency-injection.md).
