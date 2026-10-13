# EducationFinance Clean Architecture

## Solution overview

EducationFinance contains two independent portals:

- **AdminPortal** is the backend boundary for HQ administrators and school users.
- **EServicePortal** is the backend boundary for parents.

Each portal owns four production projects and three test projects. The solution contains 14 projects:

| Portal | Production projects | Test projects |
| --- | --- | --- |
| AdminPortal | `AdminPortal.API`, `AdminPortal.Application`, `AdminPortal.Domain`, `AdminPortal.Infrastructure` | `AdminPortal.Domain.Tests`, `AdminPortal.Application.Tests`, `AdminPortal.API.IntegrationTests` |
| EServicePortal | `EServicePortal.API`, `EServicePortal.Application`, `EServicePortal.Domain`, `EServicePortal.Infrastructure` | `EServicePortal.Domain.Tests`, `EServicePortal.Application.Tests`, `EServicePortal.API.IntegrationTests` |

The current implementation is a persistence and hosting foundation. Its directory skeleton reserves consistent locations for future features; it does **not** mean those business features have been implemented.

## Dependency direction

Dependencies point inward and are enforced independently for each portal:

```text
API
|-- Application
`-- Infrastructure

Infrastructure
|-- Application
`-- Domain

Application
`-- Domain

Domain
`-- no project references
```

The API project is the composition root. It is the only production layer that composes Application and Infrastructure registrations.

Forbidden references include:

- Domain to Application, Infrastructure, or API.
- Application to Infrastructure or API.
- Any AdminPortal project to an EServicePortal project.
- Any EServicePortal project to an AdminPortal project.

## Layer responsibilities

### API

The API layer contains Minimal API endpoint mapping, HTTP request/response contracts, authentication and authorization metadata, HTTP status mapping, middleware, exception handling, OpenAPI configuration, and the composition root.

Endpoints delegate use-case execution to Application services. They must not query a DbContext, implement business rules, or return Domain entities directly. Both portals use Minimal APIs; do not add MVC Controllers or mix endpoint styles without a separate architecture decision.

### Application

The Application layer contains application service implementations, optional service interfaces, use-case-specific repository interfaces, DTOs, validation, orchestration, and portal-specific service registration.

Application may depend on Domain only. It must not reference EF Core, SQL Server, Infrastructure, API, or ASP.NET Core HTTP types. Service interfaces are optional when they do not provide a useful boundary. Generic repositories and Unit of Work abstractions are prohibited.

### Domain

The Domain layer contains entities and their invariants, value objects and enums when justified, domain exceptions, and domain services for behavior that does not naturally belong to one entity.

The existing 21 AdminPortal entities and 11 EServicePortal entities remain under `Domain/Entities`. Do not add speculative behavior merely to populate reserved folders. Domain remains independent of EF Core and ASP.NET Core.

### Infrastructure

Infrastructure contains EF Core DbContexts, `IEntityTypeConfiguration<T>` mappings, migrations, model snapshots, repository implementations, external service adapters, and database/integration configuration.

Repository implementations depend on interfaces declared by Application, never the reverse. Infrastructure must not expose EF Core or SQL Server details through Application interfaces.

## Final folder organization

Existing implementation files remain in place. Empty reserved directories are tracked with `.gitkeep` and receive real files only when an approved feature requires them.

### AdminPortal

```text
src/AdminPortal/
|-- AdminPortal.API/
|   |-- Endpoints/
|   |   |-- Schools/ Cohorts/ Classes/ Students/
|   |   |-- EducationAccounts/ Fees/ TopUps/ FAS/
|   |   `-- Budgets/ Administration/              # reserved
|   |-- Contracts/Schools/                         # reserved
|   |-- Middleware/                                # reserved
|   |-- OpenApi/                                   # implemented
|   |-- Properties/
|   `-- Program.cs
|-- AdminPortal.Application/
|   |-- Interfaces/Repositories/                   # reserved
|   |-- Interfaces/Services/                       # reserved
|   |-- Services/Schools/                          # reserved
|   |-- DTOs/Schools/                              # reserved
|   |-- Validators/Schools/                        # reserved
|   `-- DependencyInjection.cs
|-- AdminPortal.Domain/
|   |-- Entities/                                  # implemented
|   |-- Enums/ Exceptions/ Services/               # reserved
|   `-- AdminPortal.Domain.csproj
`-- AdminPortal.Infrastructure/
    |-- Persistence/
    |   |-- AdminPortalDbContext.cs
    |   |-- Configurations/
    |   |-- Repositories/                          # reserved
    |   `-- Migrations/
    |-- Integrations/                              # reserved
    `-- DependencyInjection.cs
```

### EServicePortal

```text
src/EServicePortal/
|-- EServicePortal.API/
|   |-- Endpoints/
|   |   |-- Parents/ Families/ EducationAccounts/
|   |   `-- Fees/ FAS/ Payments/                  # reserved
|   |-- Contracts/ Middleware/                     # reserved
|   |-- OpenApi/                                   # implemented
|   |-- Properties/
|   `-- Program.cs
|-- EServicePortal.Application/
|   |-- Interfaces/Repositories/                   # reserved
|   |-- Interfaces/Services/                       # reserved
|   |-- Services/ DTOs/ Validators/                # reserved
|   `-- DependencyInjection.cs
|-- EServicePortal.Domain/
|   |-- Entities/                                  # implemented
|   |-- Enums/ Exceptions/ Services/               # reserved
|   `-- EServicePortal.Domain.csproj
`-- EServicePortal.Infrastructure/
    |-- Persistence/
    |   |-- EServicePortalDbContext.cs
    |   |-- Configurations/
    |   |-- Repositories/                          # reserved
    |   `-- Migrations/
    |-- Integrations/                              # reserved
    `-- DependencyInjection.cs
```

## Endpoint, service, and repository flow

```text
Minimal API endpoint
    -> Application service
        -> Application repository interface
            -> Infrastructure EF Core repository
                -> portal SQL Server database
```

- An endpoint translates an HTTP contract into Application input and maps the result to an HTTP response.
- An Application service validates and coordinates the use case.
- A repository interface describes only persistence operations required by that use case.
- An Infrastructure repository implements the interface with the portal DbContext.
- Domain entities own confirmed invariants and state transitions.

No layer may bypass this direction by injecting a DbContext into an endpoint or referencing an Infrastructure repository from Application.

## Interfaces and dependency injection

- Application repository interfaces belong under `Interfaces/Repositories`.
- Optional Application service interfaces belong under `Interfaces/Services`.
- Application service implementations belong under `Services/<Module>`.
- Infrastructure repository implementations belong under `Persistence/Repositories`.
- `AddAdminPortalApplication` and `AddEServicePortalApplication` register Application services.
- `AddAdminPortalInfrastructure` and `AddEServicePortalInfrastructure` register DbContexts, repositories, and external adapters.
- Each API `Program.cs` calls both registration methods.

Prefer constructor injection. Register interfaces against implementations at the outermost layer that owns the implementation. Do not use a service locator.

## DTO and HTTP contract separation

- API contracts belong under `API/Contracts/<Module>` and may include HTTP-specific concerns.
- Application DTOs belong under `Application/DTOs/<Module>` and remain independent of HTTP and EF Core.
- Domain entities are not API contracts and must not be serialized directly.
- EF entities and projections must not leak from Infrastructure to callers.

Use explicit mapping initially. Do not add AutoMapper automatically.

## Naming conventions

- Endpoint modules: `<Resource>Endpoints`.
- API contracts: `<Action><Resource>Request` and `<Resource>Response`.
- Services: `<Resource>Service`; optional interfaces use `I<Resource>Service`.
- Repository interfaces and implementations: `I<Resource>Repository` and `<Resource>Repository`.
- DTOs: `<Resource>Dto` or `<Action><Resource>Dto`.
- Validators: `<Action><Resource>Validator`.
- EF mappings: `<Entity>Configuration`.

Use an `Async` suffix and accept a `CancellationToken` for I/O-bound methods.

## Test organization

```text
tests/<Portal>/
|-- <Portal>.Domain.Tests/             # invariants and domain services
|-- <Portal>.Application.Tests/
|   `-- Services/<Module>/
`-- <Portal>.API.IntegrationTests/
    |-- Endpoints/<Module>/
    `-- Persistence/Repositories/
```

- Domain tests exercise entity behavior without Infrastructure.
- Application tests exercise services and validators with focused fakes.
- Endpoint integration tests exercise HTTP contracts, authentication, authorization, and status mapping.
- Persistence integration tests exercise repositories and SQL Server behavior.
- Architecture tests enforce dependency direction and portal isolation without a third-party framework.

## Adding a new business module

Add files only when an approved use case requires them:

1. Confirm business rules and portal ownership.
2. Add or enrich Domain behavior without EF Core dependencies.
3. Define Application DTOs and a use-case-specific repository interface.
4. Implement the Application service and validation.
5. Implement the repository in Infrastructure with the existing DbContext.
6. Register Application and Infrastructure implementations in their respective DI extensions.
7. Add API request/response contracts and a Minimal API endpoint module.
8. Map the endpoint module from the API composition root.
9. Add Domain, Application, endpoint, and persistence tests as appropriate.
10. Create a migration only when an approved schema change requires one.

### Future Schools example — not implemented

These names illustrate placement only. The files do not currently exist and this example does not authorize implementation:

```text
AdminPortal.API/Endpoints/Schools/SchoolEndpoints.cs
AdminPortal.API/Contracts/Schools/CreateSchoolRequest.cs
AdminPortal.API/Contracts/Schools/SchoolResponse.cs
AdminPortal.Application/Interfaces/Repositories/ISchoolRepository.cs
AdminPortal.Application/Interfaces/Services/ISchoolService.cs
AdminPortal.Application/Services/Schools/SchoolService.cs
AdminPortal.Application/DTOs/Schools/SchoolDto.cs
AdminPortal.Application/DTOs/Schools/CreateSchoolDto.cs
AdminPortal.Application/Validators/Schools/CreateSchoolValidator.cs
AdminPortal.Infrastructure/Persistence/Repositories/SchoolRepository.cs
```

No MediatR, CQRS framework, generic repository, Unit of Work framework, AutoMapper, or FluentValidation dependency is implied.

## Portal and database boundaries

AdminPortal and EServicePortal communicate only through explicitly approved external contracts. They must not reference one another's projects, access one another's DbContext, or create cross-database foreign keys.

Each portal owns a separate SQL Server database and connection-string key: `AdminPortalDb` or `EServicePortalDb`. Migrations remain portal-specific and are never applied automatically during startup.
