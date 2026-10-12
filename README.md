# EducationFinance

EducationFinance contains two fully independent .NET services:

- **Admin Portal** serves HQ/MOE and school staff operations.
- **E-Service Portal** serves parents and citizens.

Each portal follows Clean Architecture and owns its Domain, Application, Infrastructure, API, configuration, EF Core migrations, and tests. There is intentionally no shared application or domain layer between the portals.

## Project structure

```text
EducationFinance/
|-- EducationFinance.slnx
|-- src/
|   |-- AdminPortal/
|   |   |-- AdminPortal.Domain/
|   |   |-- AdminPortal.Application/
|   |   |-- AdminPortal.Infrastructure/
|   |   `-- AdminPortal.API/
|   `-- EServicePortal/
|       |-- EServicePortal.Domain/
|       |-- EServicePortal.Application/
|       |-- EServicePortal.Infrastructure/
|       `-- EServicePortal.API/
`-- tests/
    |-- AdminPortal/
    |   |-- AdminPortal.Domain.Tests/
    |   |-- AdminPortal.Application.Tests/
    |   `-- AdminPortal.API.IntegrationTests/
    `-- EServicePortal/
        |-- EServicePortal.Domain.Tests/
        |-- EServicePortal.Application.Tests/
        `-- EServicePortal.API.IntegrationTests/
```

## Architecture rules

The dependency direction is the same inside each portal:

```text
API -> Application <- Infrastructure
          |
        Domain
```

- Domain has no dependency on another layer.
- Application depends only on its portal's Domain project.
- Infrastructure implements its portal's persistence and integration abstractions.
- API is the composition root and exposes only that portal's endpoints.
- A project in one portal must not reference a project in the other portal.
- Cross-portal communication, when needed, must use an explicit external contract such as HTTP or messaging rather than a project reference.

Keep controllers thin. Put business rules in Domain, use cases in Application, persistence and external integrations in Infrastructure, and HTTP concerns in API.

## Configuration

The services use independent connection-string keys and local ports:

| Service | Database | Connection string | HTTP | HTTPS |
|---|---|---|---:|---:|
| Admin Portal | `AdminPortalDb` | `AdminPortalConnection` | `5183` | `7101` |
| E-Service Portal | `EServicePortalDb` | `EServicePortalConnection` | `5184` | `7100` |

The database users must be different. Neither service may query, join, migrate, or hold credentials for the other service database. Cross-portal data moves through versioned APIs/events and the local outbox/inbox tables described in `docs/contracts/portal-integration-events.md`.

Keep real credentials in environment variables or local secret storage. Do not commit passwords, tokens, certificates, or production connection strings.

## Build and test

Run from the repository root:

```bash
dotnet restore
dotnet tool restore
dotnet build
dotnet test
```

Run one service:

```bash
dotnet run --project src/AdminPortal/AdminPortal.API
dotnet run --project src/EServicePortal/EServicePortal.API
```

Run one portal's tests:

```bash
dotnet test tests/AdminPortal/AdminPortal.API.IntegrationTests
dotnet test tests/EServicePortal/EServicePortal.API.IntegrationTests
```

Always inspect the test summary. A successful command with zero discovered tests does not validate behavior.

## EF Core migrations

Admin Portal:

```bash
dotnet tool run dotnet-ef migrations add AddFeatureName \
  --context AdminDbContext \
  --project src/AdminPortal/AdminPortal.Infrastructure \
  --startup-project src/AdminPortal/AdminPortal.API \
  --output-dir Persistence/Migrations
```

E-Service Portal:

```bash
dotnet tool run dotnet-ef migrations add AddFeatureName \
  --context EServiceDbContext \
  --project src/EServicePortal/EServicePortal.Infrastructure \
  --startup-project src/EServicePortal/EServicePortal.API \
  --output-dir Persistence/Migrations
```

Review generated migrations before applying them. Never edit an already-applied shared-environment migration; create a new migration instead.

## Naming and code quality

- Use PascalCase for types and files, camelCase for locals and parameters, and `_camelCase` for private fields.
- Prefix interfaces with `I` and suffix asynchronous methods with `Async`.
- Use descriptive request, response, validator, and EF configuration names.
- Validate all external input and enforce authorization on the server.
- Use EF Core or parameterized SQL; never concatenate untrusted input into SQL.
- Do not log passwords, access tokens, or sensitive personal data.
- Add tests in the same portal as the behavior being changed.

Before committing, run:

```bash
dotnet build
dotnet test
git diff --check
git status --short
```