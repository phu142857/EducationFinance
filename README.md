# EducationFinance

EducationFinance is a .NET 10 backend foundation containing two independent portals:

- **AdminPortal** serves HQ administrators and school users.
- **EServicePortal** serves parents.

Each portal has its own Domain, Application, Infrastructure, and API projects. There are no shared projects or cross-portal project references.

## Prerequisites

- .NET SDK 10
- Docker and Docker Compose
- SQL Server 2022 (provided through Docker Compose)
- EF Core CLI tool (`dotnet-ef`, restored from the local tool manifest)

## Build and test

Run from the repository root:

```bash
dotnet restore
dotnet tool restore
dotnet build
dotnet test
```

## Database setup

EducationFinance uses two separate SQL Server databases:

| Portal | Database | Connection string key |
|---|---|---|
| AdminPortal | `AdminPortalDB` | `ConnectionStrings:AdminPortalDb` |
| EServicePortal | `EServicePortalDB` | `ConnectionStrings:EServicePortalDb` |

## Database migrations

Each portal maintains its own EF Core DbContext and migration history.

- **AdminPortal:** `AdminPortalDbContext`
- **EServicePortal:** `EServicePortalDbContext`

Migrations are stored in each portal's Infrastructure project.

All commands below must be executed from the repository root.

### 1. Restore the EF Core CLI tool

```bash
dotnet tool restore
```

Verify the installed EF Core CLI:

```bash
dotnet ef --version
```

### 2. Apply existing migrations

Apply migrations to **AdminPortalDB**:

```bash
dotnet ef database update \
  --project src/AdminPortal/AdminPortal.Infrastructure \
  --startup-project src/AdminPortal/AdminPortal.API \
  --context AdminPortalDbContext
```

Apply migrations to **EServicePortalDB**:

```bash
dotnet ef database update \
  --project src/EServicePortal/EServicePortal.Infrastructure \
  --startup-project src/EServicePortal/EServicePortal.API \
  --context EServicePortalDbContext
```

These commands apply pending migrations to their respective databases.

If the database is already up to date, EF Core will not reapply completed migrations.

### 3. Create a new migration

When an approved database schema change is implemented, create a new migration for the corresponding portal.

**AdminPortal:**

```bash
dotnet ef migrations add <MigrationName> \
  --project src/AdminPortal/AdminPortal.Infrastructure \
  --startup-project src/AdminPortal/AdminPortal.API \
  --context AdminPortalDbContext \
  --output-dir Persistence/Migrations
```

**EServicePortal:**

```bash
dotnet ef migrations add <MigrationName> \
  --project src/EServicePortal/EServicePortal.Infrastructure \
  --startup-project src/EServicePortal/EServicePortal.API \
  --context EServicePortalDbContext \
  --output-dir Persistence/Migrations
```

Replace `<MigrationName>` with a meaningful name, such as `AddSchoolStatus`.

Always review generated migration operations before applying them.

### 4. Apply a newly created migration

After reviewing the migration, run the appropriate `database update` command from Step 2.

### 5. List existing migrations

**AdminPortal:**

```bash
dotnet ef migrations list \
  --project src/AdminPortal/AdminPortal.Infrastructure \
  --startup-project src/AdminPortal/AdminPortal.API \
  --context AdminPortalDbContext
```

**EServicePortal:**

```bash
dotnet ef migrations list \
  --project src/EServicePortal/EServicePortal.Infrastructure \
  --startup-project src/EServicePortal/EServicePortal.API \
  --context EServicePortalDbContext
```

### 6. Check for pending model changes

**AdminPortal:**

```bash
dotnet ef migrations has-pending-model-changes \
  --project src/AdminPortal/AdminPortal.Infrastructure \
  --startup-project src/AdminPortal/AdminPortal.API \
  --context AdminPortalDbContext
```

**EServicePortal:**

```bash
dotnet ef migrations has-pending-model-changes \
  --project src/EServicePortal/EServicePortal.Infrastructure \
  --startup-project src/EServicePortal/EServicePortal.API \
  --context EServicePortalDbContext
```

This verifies whether the current EF Core model has changes that are not yet represented by a migration.

### 7. Remove the latest unapplied migration

If a migration was created by mistake and has **not been applied to the database**, it can be removed.

**AdminPortal:**

```bash
dotnet ef migrations remove \
  --project src/AdminPortal/AdminPortal.Infrastructure \
  --startup-project src/AdminPortal/AdminPortal.API \
  --context AdminPortalDbContext
```

**EServicePortal:**

```bash
dotnet ef migrations remove \
  --project src/EServicePortal/EServicePortal.Infrastructure \
  --startup-project src/EServicePortal/EServicePortal.API \
  --context EServicePortalDbContext
```

Do not remove migrations that have already been applied to shared or production databases without a reviewed rollback plan.

### 8. Roll back to an earlier migration

EF Core supports updating a database to a specific migration:

```bash
dotnet ef database update <TargetMigrationName> \
  --project src/AdminPortal/AdminPortal.Infrastructure \
  --startup-project src/AdminPortal/AdminPortal.API \
  --context AdminPortalDbContext
```

Replace `<TargetMigrationName>` with an existing migration name.

**Warning:** Rolling back migrations can remove schema objects or data. Back up the database and review the migration's `Down()` operations before executing a rollback. Production changes should use a reviewed deployment and recovery plan.

## Migration guidelines

- Keep AdminPortal and EServicePortal migrations separate.
- Define Entity Framework Core mappings in Infrastructure.
- Do not introduce cross-database foreign keys.
- Do not modify applied migration files or model snapshots manually.
- Create migrations only for approved schema changes.
- Review generated SQL and schema changes before applying migrations to production.
- Never use `database drop` or destructive Docker volume commands as part of normal setup.
- Use separate, isolated databases when running tests that modify persistent data.

## Run the APIs

Start AdminPortal:

```bash
dotnet run --project src/AdminPortal/AdminPortal.API
```

Start EServicePortal in a separate terminal:

```bash
dotnet run --project src/EServicePortal/EServicePortal.API
```

Each API exposes:

- `GET /health` for a basic host health response.
- `GET /openapi/v1.json` for its OpenAPI document.
- `GET /swagger` for Swagger UI with Bearer JWT authorization support.

See [Clean Architecture](docs/architecture/clean-architecture.md) for layer responsibilities and dependency rules.