# Contributing

Thanks for improving this curriculum.

## Guidelines

1. Branch from `dev` (or the default working branch) and open a PR.
2. Prefer descriptive branches: `username/(feature|bug|fix|docs)/short-description`.
3. When you change guidance, update the matching page under `docs/` (What / Why / Explaining / Prefer-Avoid).
4. Keep samples runnable on **.NET 8** (`dotnet build AL_NET_Library.sln`).
5. Do not promote `_archive/` into the solution — rewrite Prefer/Avoid samples under `samples/` instead.
6. Match existing teaching style: short Prefer/Avoid, explain why the example was chosen, XML docs on key types.

## Checklist before PR

- [ ] `dotnet build AL_NET_Library.sln` succeeds
- [ ] New or changed sample has a README with **Why this example**
- [ ] Docs index (`docs/README.md`) links the topic if it is new
- [ ] No secrets or local DB paths committed
