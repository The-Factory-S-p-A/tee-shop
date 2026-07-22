# TeeShop Products API

A small ASP.NET Core Web API (.NET 10) that serves a catalog of t-shirt products. On first run it
automatically creates a local SQLite database and seeds it with 50 deterministic demo products —
no manual setup required.

## Prerequisites

- **.NET 10 SDK** — [download here](https://dotnet.microsoft.com/download/dotnet/10.0). This project
  targets `net10.0`, so an 8/9 SDK will not build it; make sure `dotnet --version` reports `10.x`
  before your interview.
- **Git**, to clone the repository.
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
- Try `GET /api/products` — it returns the full seeded catalog as JSON.
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
3. You will extend this project live during the interview, so make sure your environment is ready
   to write and debug code comfortably.
4. An AI coding assistant is welcome during the interview (Claude Code, GitHub Copilot, Codex CLI,
   or similar) — please have it installed and authenticated beforehand, since we won't have time to
   set it up on the day.

## Troubleshooting

- **Stale or corrupted data**: stop the API and delete `products.db` (and any `products.db-shm` /
  `products.db-wal` files next to it) from the working directory, then run again — it will be
  recreated and reseeded automatically.
- **Port already in use**: the API is fixed to port `5080` in
  `TeeShop.Api/Properties/launchSettings.json`. If something else on your machine is already using
  it, edit `applicationUrl` in that file to a free port.
