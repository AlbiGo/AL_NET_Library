---
marp: true
title: Dependency Injection — C# Best Practices
theme: default
paginate: true
---

# Dependency Injection

Build once. Inject via constructors. Scope what must be scoped.

**Sample:** `Dependency/`  
**Guide:** `docs/02-dependency-injection.md`

---

# Prefer

- Single **composition root** — build the container **once** at startup
- **Constructor injection** for required deps
- Register **interfaces → implementations**
- Create a **scope** for scoped services (DbContext, repos)
- Typed resolve: `GetRequiredService<T>()`

---

# Avoid

- Rebuilding `ServiceProvider` on every resolve
- `new`ing the whole graph inside controllers
- Service locator deep in business logic
- Capturing scoped services in singletons

---

# See it run

```bash
dotnet run --project Dependency
```

| Type | Role |
| --- | --- |
| `AppServices` | Composition root (Do) |
| `EconomicsController` | Constructor injection (Do) |
| `EconomicsControllerV2` | Manual `new` graph (Don’t contrast) |

---

# Takeaway

> Wire dependencies at the edge of the app.  
> Keep business types free of container details.
