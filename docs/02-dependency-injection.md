# Dependency injection

## What it is

**Dependency injection (DI)** means a type does not create its own collaborators. Instead, required services are passed in (usually via the constructor). A **composition root** at startup registers mappings (`IMathService` → `MathService`) and builds one container.

## Why it matters

- Swapping implementations (real DB vs fake) without editing callers
- Clear lifetimes: scoped DbContext per request/unit of work
- Testability: inject mocks

## Why this example

**Math / economics controller** is a tiny graph (`Controller → Service → Repo → Context`) that still shows every DI idea you need: composition root, lifetimes, constructor injection, and a manual-`new` contrast. A real web app would bury the lesson in frameworks and HTTP noise.

## How the sample code works

**Sample:** [`samples/Dependency/`](../samples/Dependency/)

### 1. Composition root — build once

`AppServices.Configure` creates a `ServiceCollection`, registers types, then builds a single `ServiceProvider`. Later calls return early so the container is not rebuilt.

```csharp
services.AddScoped<IMathService, MathService>();
services.AddTransient<EconomicsController>();
_provider = services.BuildServiceProvider();
```

- **Scoped** — one instance per scope (good for DbContext / repos)
- **Transient** — new instance each resolve (fine for a lightweight controller)

### 2. Constructor injection — the “Do”

`EconomicsController` asks for `IMathService`. The container constructs `MathService` (and its `IMathRepo`) automatically.

```csharp
public EconomicsController(IMathService mathService)
{
    _mathService = mathService;
}
```

### 3. Manual wiring — the “Don’t” contrast

`EconomicsControllerV2` does `new MathService(new MathRepo(new MathDBContext()))`. That works for three types and becomes painful as the graph grows.

### 4. Program entry

```bash
dotnet run --project samples/Dependency
```

`Program.cs` calls `AppServices.Configure()`, opens a **scope**, resolves `EconomicsController`, and runs a calculation. The scope is disposed afterward so scoped services are cleaned up.

## Prefer

- One composition root; constructor injection; scopes for DbContext-like services

## Avoid

- Rebuilding the provider on every resolve; `dynamic` service location; scoped services captured by singletons
