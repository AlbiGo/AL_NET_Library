# Dependency injection

Demonstrates a correct composition root versus manual wiring.

```bash
dotnet run --project Dependency
```

## Do

- **`AppServices`** — register once, reuse one `ServiceProvider`, resolve with scopes
- **`EconomicsController`** — constructor injection of `IMathService`
- Concept types in `Concept/` — pure constructor injection without a container

## Don’t (contrast)

- **`EconomicsControllerV2`** — manually `new`s the whole graph; fine for a tiny demo, painful as dependencies grow
- Rebuilding the container on every resolve (removed from this sample)

## Guide

See [docs/02-dependency-injection.md](../docs/02-dependency-injection.md).
