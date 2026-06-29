# FullWebsite

A small ASP.NET Core product catalog with an admin back office, built during my internship at Virtual Data IT (Jordan), June–July 2025.

This was my internship assignment (the mandatory 160-hour kind), and I used it to get hands-on with a real ASP.NET Core MVC stack rather than just tutorials. It's a two-project setup: a web app for managing products and categories, backed by a domain library that holds the data model, EF Core migrations, and the repository/service layers. Nothing fancy feature-wise, but I deliberately structured it the way a real team would so I could learn the patterns properly.

## What it does

- CRUD for **Products** and **Categories** through an admin area (`AdminV`)
- A public-facing **User** area as the default landing route
- Login / registration / account management via ASP.NET Core Identity (the scaffolded Identity UI under `Areas/Identity`)
- Products belong to a category, with prices and validation handled through data annotations on the entities

## How it's put together

- **Admin** – the web project. ASP.NET Core MVC with Areas, Razor Pages for Identity, Bootstrap + jQuery on the front end.
- **Domain** – a class library with the entities, DTOs, `ApplicationDbContext`, EF Core migrations, and the repository + service interfaces/implementations.

The reason I split it this way: I wanted the controllers to stay thin. Controllers talk to services, services talk to repositories, and the repositories are where EF Core lives. DTOs sit in between so the views aren't binding straight to entities. It's more layers than this size of app strictly needs, but learning that flow was the whole point.

**Stack:** C# on .NET 9, ASP.NET Core MVC, EF Core 9 (SQL Server), ASP.NET Core Identity.

## Running it

You'll need the .NET 9 SDK and a SQL Server instance you can point at.

There's no `.sln` in the repo, so run the web project directly (it references `Domain` automatically):

```bash
# from the repo root
cd Admin

# set your own connection string (don't commit it)
dotnet user-secrets set "ConnectionStrings:DefaultConnection" \
  "Server=YOUR_SERVER;Database=YOUR_DB;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True"

# apply migrations, then run
dotnet ef database update
dotnet run
```

It comes up on `http://localhost:5093` (or `https://localhost:7075`). The default route lands on the User home page; the admin CRUD lives under `/AdminV/Product` and `/AdminV/Category`.

> Heads up: the repo currently ships a real-looking connection string in `Admin/appsettings.json`. That was sloppy of me — it should live in user-secrets or environment variables, which is what the command above does. If you're cloning this, override it and don't rely on what's checked in.

## Things I'd clean up

- Move the connection string out of `appsettings.json` for good and scrub it from history.
- The Sqlite EF Core package is referenced but the app only wires up SQL Server — I added it while experimenting and never finished switching providers.
- Drop the leftover scaffolding stubs (`Class1.cs`, `Interface1.cs`) and the stray legacy `Microsoft.AspNet.Mvc` package that isn't actually used.

It's a learning project from my internship, so I'm keeping it honest about what's polished and what isn't.
