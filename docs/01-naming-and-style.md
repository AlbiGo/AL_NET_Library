# Naming and style

Code style is enforced by [`.editorconfig`](../.editorconfig) and [`Directory.Build.props`](../Directory.Build.props) (nullable, analyzers, latest analysis level).

## Prefer

- PascalCase for types, methods, and properties; interfaces prefixed with `I`
- `_camelCase` for private fields
- `Async` suffix on async methods (`MakePastaAsync`)
- Explicit nullability (`string?` when null is allowed)
- Clear, intention-revealing names over abbreviations (`PrepareIngredients`, not `PrepIngr`)

## Avoid

- Hungarian notation or cryptic single-letter names outside tiny loop indexes
- Public mutable static state without a clear lifetime story
- Disabling nullable or analyzers project-wide without a documented reason
- Mixing formatting styles across files (let EditorConfig own it)

## See also

- Root [`.editorconfig`](../.editorconfig)
- [Microsoft C# coding conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)
