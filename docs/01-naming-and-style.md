# Naming and style

## What it is

Consistent naming and formatting so readers can scan code quickly. Style is enforced by [`.editorconfig`](../.editorconfig) and [`Directory.Build.props`](../Directory.Build.props) (nullable enabled, analyzers on).

## Why it matters

In a teaching repo (and in teams), inconsistent names hide bugs: people misread `async` methods, confuse interfaces with classes, or miss nullability.

## How it shows up in samples

| Rule | Example in this repo |
| --- | --- |
| Types / methods PascalCase | `ServiceGarage`, `GetInstance` |
| Interfaces start with `I` | `IMathService`, `IBaseClassA` |
| Private fields `_camelCase` | `_car`, `_provider`, `_lockObject` |
| Async methods end with `Async` | `MakePastaAsync`, `SaveChangesAsync` |
| Nullable when null is allowed | `string?`, `CarServiceDelegate?` |

## Prefer

- Intention-revealing names (`PrepareIngredients`, not `Prep`)
- Explicit nullability (`string?` when null is valid)

## Avoid

- Disabling nullable/analyzers without a documented reason
- Mixing formatting styles across files
