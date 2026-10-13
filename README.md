# EducationFinance

EducationFinance is a .NET 10 backend foundation containing two independent portals:

- **AdminPortal** serves HQ administrators and school users.
- **EServicePortal** serves parents.

Each portal has its own Domain, Application, Infrastructure, and API projects. There are no shared projects or cross-portal project references.

## Prerequisites

- .NET SDK 10

## Build and test

Run from the repository root:

```bash
dotnet restore
dotnet tool restore
dotnet build
dotnet test
```

## Run the APIs

```bash
dotnet run --project src/AdminPortal/AdminPortal.API
dotnet run --project src/EServicePortal/EServicePortal.API
```

Each API exposes:

- `GET /health` for a basic host health response.
- `GET /openapi/v1.json` for its OpenAPI document.
- `GET /swagger` for Swagger UI with Bearer JWT authorization support.

Use the Swagger UI **Authorize** button and enter the JWT access token only; the UI adds the `Bearer` prefix. Token issuance and login workflows are not implemented by this foundation.

JWT validation can be connected to an identity provider through configuration:

```bash
Authentication__Authority="https://identity-provider.example" \
Authentication__Audience="education-finance-api" \
dotnet run --project src/AdminPortal/AdminPortal.API
```

The same keys apply to EServicePortal. `Authentication__RequireHttpsMetadata` defaults to `true` and should only be disabled for a local HTTP identity provider.

## Database configuration

The portals use separate SQL Server databases and configuration keys. Supply connection strings through environment variables, user secrets, or deployment configuration; do not commit credentials.

```bash
ConnectionStrings__AdminPortalDb="..." dotnet run --project src/AdminPortal/AdminPortal.API
ConnectionStrings__EServicePortalDb="..." dotnet run --project src/EServicePortal/EServicePortal.API
```

The initial migrations are source-controlled independently under each Infrastructure project's `Persistence/Migrations` directory. They are not applied automatically.

Business workflows, authentication, and external integrations remain outside this persistence-foundation phase.

See [Clean Architecture](docs/architecture/clean-architecture.md) for layer responsibilities and dependency rules.
