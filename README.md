# TeeShop Products API

A small ASP.NET Core Web API (.NET 10) that exposes a catalog of fake/demo t-shirt products, backed
by a local SQLite database that is created and seeded automatically on first run. It's the starting
point for a live coding interview — you'll extend it during the session.

## Why it looks like this

For a single read-only endpoint, this solution is deliberately more structured than the use case
strictly requires: a layered architecture, a small Result pattern for propagating success/failure
across layers, resolver-based dependency wiring, and a dedicated test project. That's on purpose —
the codebase mirrors the conventions we actually use for real projects at The Factory, so working in
it gives you an honest preview of the patterns you'd work with day to day here, rather than a
synthetic toy built just for the occasion.

## Prerequisites

- **.NET 10 SDK** — [download here](https://dotnet.microsoft.com/download/dotnet/10.0). This project
  targets `net10.0`, so an 8/9 SDK will not build it; make sure `dotnet --version` reports `10.x`
  before your interview.
- **Git**, to clone the repository.
- **Node.js (LTS) and the Angular CLI** (`npm install -g @angular/cli`) — part of the interview
  involves writing browser-based UI code against this API, and Angular is the framework we use.
  Don't worry if Angular isn't your main framework: prior Angular experience is not assumed, but
  the working environment is.
- An IDE of your choice: Visual Studio 2022, JetBrains Rider, or VS Code with the C# Dev Kit
  extension.

## Getting started

```bash
git clone <this-repo-url>
cd interview-product-challenge
dotnet build TeeShop.sln
dotnet test TeeShop.sln
dotnet run --project TeeShop.Api
```

Or open `TeeShop.sln` in your IDE and press F5 (the `http` launch profile is already configured).

The API listens on `http://localhost:5080`.

## What to expect

- Swagger UI is available at `http://localhost:5080/swagger` and documents the API surface.
- Try `GET /api/products` — it returns the full seeded catalog as JSON. Product images are placeholder
  photos loaded from an external service, so you'll need an internet connection to see them rendered
  (the data itself works fine offline).
- The solution follows a small layered architecture:

  ```
  TeeShop.Api → TeeShop.Services → TeeShop.Sql → TeeShop.Common
  ```

  Controllers stay thin and delegate to the service layer, which delegates to a SQL/repository
  layer backed by EF Core and SQLite, all sharing DTOs and a small Result pattern defined in
  `TeeShop.Common`.
- CORS is configured permissively, so a browser-based client on a different origin (e.g. a local
  dev server) can call the API without extra configuration on your side.

## Before your interview

Please do this ahead of time so we don't spend interview time on environment setup:

1. Clone the repository, confirm `dotnet build TeeShop.sln` succeeds, and run the API.
2. Confirm you can set a breakpoint (e.g. inside `ProductsController`) and hit it in debug mode
   from your IDE.
3. Spend a few minutes reading through the existing code, not just running it — you'll move faster
   during the interview if the layering and the Result pattern already feel familiar.
4. You will extend this project live during the interview, so make sure your environment is ready
   to write and debug code comfortably.
5. Have an empty Angular app ready to go: run `ng new`, confirm `ng serve` works, and keep that
   workspace at hand. We don't want you spending interview time on scaffolding and `npm install`.
6. **Bring an AI coding assistant** (Claude Code, GitHub Copilot, Codex CLI, or similar) — this is
   **required**, not optional: the interview is scoped assuming you'll use one, and without it we
   simply won't have time to finish. It also reflects how we actually work day to day. Have it
   installed and authenticated beforehand (free tiers are fine if you don't have a subscription),
   since we won't set one up on the day.

## Troubleshooting

- **Stale or corrupted data**: stop the API and delete `products.db` (and any `products.db-shm` /
  `products.db-wal` files next to it) from the working directory, then run again — it will be
  recreated and reseeded automatically.
- **Port already in use**: the API is fixed to port `5080` in
  `TeeShop.Api/Properties/launchSettings.json`. If something else on your machine is already using
  it, edit `applicationUrl` in that file to a free port.
- If anything above doesn't work for you, reach out to whoever scheduled the interview before the
  day itself so we can sort it out rather than losing time during the session.
