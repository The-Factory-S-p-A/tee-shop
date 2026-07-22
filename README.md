# TeeShop Products API

Small ASP.NET Core (.NET 10) API that serves a catalog of demo t-shirt products from a local SQLite
database, created and seeded automatically on first run. It's the starting point for a live coding
interview: you'll extend it during the session.

The solution is more structured than a single endpoint needs, on purpose: it follows the same
layered architecture and conventions we use in our real projects (`Api → Services → Sql → Common`,
thin controllers, a small Result pattern, EF Core, xUnit tests). Read the code before the interview,
don't just run it.

## Prerequisites

- .NET 10 SDK (`dotnet --version` must report `10.x`)
- Node.js LTS and the Angular CLI (`npm install -g @angular/cli`). Part of the interview involves
  writing UI code against this API. Angular experience is not assumed, but the environment must be
  ready.
- Git and an IDE (Visual Studio, Rider, or VS Code with the C# Dev Kit)

## Run it

```bash
git clone https://github.com/The-Factory-S-p-A/tee-shop.git
cd tee-shop
dotnet build TeeShop.sln
dotnet test TeeShop.sln
dotnet run --project TeeShop.Api
```

Or open the solution in your IDE and press F5.

The API listens on `http://localhost:5080`, Swagger UI at `http://localhost:5080/swagger`.
Try `GET /api/products`. CORS is open, so a local dev server can call the API directly.
Product images come from an external placeholder service, so you need to be online to see them.

## Before the interview

1. Clone, build, run and hit the endpoint.
2. Set a breakpoint in `ProductsController` and confirm you can debug.
3. Create an empty Angular app (`ng new`) and check that `ng serve` works.
4. Bring your AI coding assistant (Claude Code, Copilot, Codex, ...), installed and authenticated.
   You'll use it during the session.

If something doesn't work, contact us before the interview day.

## Troubleshooting

- To reset the data, stop the API and delete `products.db` (plus any `-shm`/`-wal` files). It gets
  recreated on the next run.
- Port 5080 already taken? Change `applicationUrl` in `TeeShop.Api/Properties/launchSettings.json`.
