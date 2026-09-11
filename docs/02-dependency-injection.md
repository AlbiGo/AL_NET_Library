# Dependency injection

## Prefer

- A single composition root that builds the container **once** at startup
- Constructor injection for required dependencies
- Registering interfaces to implementations (`IMathService` → `MathService`)
- Creating a **scope** for scoped services (DbContext, repos) and disposing it
- Typed resolve (`GetRequiredService<T>()`), not `dynamic`

## Avoid

- Rebuilding `ServiceCollection` / `ServiceProvider` on every resolve
- `new`ing the full dependency graph inside controllers (except tiny demos of the anti-pattern)
- Service locator deep in business logic when constructor injection is possible
- Capturing scoped services in singletons

## See sample

- [`Dependency/`](../Dependency/) — `AppServices` composition root, `EconomicsController` (Do) vs `EconomicsControllerV2` (manual wiring contrast)

```bash
dotnet run --project Dependency
```
