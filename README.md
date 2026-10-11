
# EducationFinance

EducationFinance is a .NET backend for an Education Finance system. It supports the e-Service Portal for parents and the Admin Portal for HQ/MOE and school staff, with role-based access control and school-scoped permissions.

## 1. Project Structure

```text
EducationFinance/
├── EducationFinance.slnx
├── src/
│   ├── EducationFinance.Domain/
│   ├── EducationFinance.Application/
│   ├── EducationFinance.Infrastructure/
│   └── EducationFinance.API/
└── tests/
    ├── EducationFinance.Domain.Tests/
    ├── EducationFinance.Application.Tests/
    └── EducationFinance.API.IntegrationTests/
```

### Project Responsibilities

| Project | Responsibility |
|---|---|
| `EducationFinance.Domain` | Core entities, domain rules, and business invariants. |
| `EducationFinance.Application` | Use cases, application services, DTOs, interfaces, and validation. |
| `EducationFinance.Infrastructure` | EF Core, database access, entity configurations, migrations, and external service implementations. |
| `EducationFinance.API` | HTTP endpoints, controllers, middleware, dependency injection, and API configuration. |
| `EducationFinance.Domain.Tests` | Unit tests for domain logic. |
| `EducationFinance.Application.Tests` | Unit tests for application logic. |
| `EducationFinance.API.IntegrationTests` | API and integration tests. |

Follow the existing folder structure within each project when adding new files.

## 2. Architecture Rules

Dependencies must follow the Clean Architecture direction:

- **Domain:** Must not depend on other project layers.
- **Application:** May depend on Domain.
- **Infrastructure:** Implements persistence and external service interfaces.
- **API:** Connects the layers through dependency injection and exposes HTTP endpoints.
- **Tests:** Test the behavior of their corresponding layer.

Keep controllers thin. Business logic belongs in Domain or Application, while database queries and persistence logic belong in Infrastructure.

Avoid circular dependencies between projects.

## 3. Adding a New Feature

For a feature such as student management, fee assessments, or payments:

1. **Domain:** Add or update entities and business rules when required.
2. **Application:** Add use cases, DTOs, interfaces, and validators.
3. **Infrastructure:** Add entity configurations and implementations for database or external service operations.
4. **API:** Add endpoints/controllers and dependency injection registrations when required.
5. **Tests:** Add tests for the new behavior.

Do not create files in every layer automatically. Only add files that have a clear responsibility.

## 4. File Naming Conventions

Use standard C# naming conventions consistently.

| Item | Convention | Example |
|---|---|---|
| Classes and records | PascalCase | `StudentService.cs` |
| Interfaces | Prefix with `I` | `IStudentRepository.cs` |
| Methods | PascalCase | `GetStudentByIdAsync()` |
| Async methods | Suffix with `Async` | `CreateStudentAsync()` |
| Local variables and parameters | camelCase | `studentId` |
| Private fields | `_camelCase` | `_studentRepository` |
| Request DTOs | Descriptive purpose + `Request` | `CreateStudentRequest.cs` |
| Response DTOs | Descriptive purpose + `Response` | `StudentResponse.cs` |
| Validators | Type name + `Validator` | `CreateStudentRequestValidator.cs` |
| EF Core configurations | Entity name + `Configuration` | `StudentConfiguration.cs` |
| Test classes | Type or feature name + `Tests` | `StudentServiceTests.cs` |
| Migrations | PascalCase migration name | `AddStudentStatus` |

Additional rules:

- Use one main public type per file in most cases.
- The filename should match the main public type.
- Keep namespaces consistent with the project and folder structure.
- Prefer descriptive names over vague names such as `Helper.cs`, `Common.cs`, or `Manager.cs`.
- Avoid unnecessary abbreviations.
- Use English for identifiers, comments, and technical documentation.

## 5. Database and EF Core

The project uses Entity Framework Core with SQL Server.

- Keep entity mapping, indexes, and constraints in Infrastructure.
- Create a new migration whenever the database schema changes.
- Review generated migrations before applying them.
- Never modify a migration that has already been applied to a shared environment; create a new migration instead.
- Do not use `EnsureCreated()` as a replacement for migrations.
- Verify the target connection string before updating a database.

Create a migration from the repository root:

```bash
dotnet ef migrations add AddFeatureName \
  --project src/EducationFinance.Infrastructure \
  --startup-project src/EducationFinance.API \
  --output-dir Persistence/Migrations
```

Apply migrations to the configured development database:

```bash
dotnet ef database update \
  --project src/EducationFinance.Infrastructure \
  --startup-project src/EducationFinance.API
```

Never commit database passwords, production connection strings, tokens, or certificates.

## 6. Build, Test, and Run

Run commands from the repository root.

Restore dependencies:

```bash
dotnet restore
```

Build the solution:

```bash
dotnet build
```

Run all tests:

```bash
dotnet test
```

Run API integration tests only:

```bash
dotnet test tests/EducationFinance.API.IntegrationTests/EducationFinance.API.IntegrationTests.csproj
```

Run the API:

```bash
dotnet run --project src/EducationFinance.API
```

Always inspect the test summary. A successful command with `total: 0` means no tests were executed.

Use Swagger/OpenAPI in the development environment when enabled.

## 7. Configuration and Secrets

- Keep machine-specific configuration local.
- Ensure `appsettings.Development.json` is ignored by Git when it contains local credentials.
- Prefer environment variables or a secret manager for sensitive configuration.
- Use placeholders in example configuration files.
- Never commit real passwords, access tokens, private keys, or production secrets.
- If a secret is committed, rotate it and follow the repository's Git-history cleanup procedure.

Adding a file to `.gitignore` does not stop Git from tracking a file that is already tracked.

## 8. Git and Code Quality

- Use descriptive branch names, such as `feature/student-search` or `fix/payment-validation`.
- Keep commits focused on one logical change.
- Use clear, imperative commit messages, such as `Add student search endpoint`.
- Avoid unrelated refactoring in feature commits.
- Do not commit `bin/`, `obj/`, local database files, logs, IDE caches, or local secrets.
- Add or update tests when behavior changes.
- Handle exceptions explicitly; do not silently swallow errors.
- Validate all external input.
- Enforce authorization on the server for every protected operation. Hiding UI elements is not access control.
- Use EF Core or parameterized SQL queries; never concatenate untrusted input into SQL.
- Never log passwords, access tokens, or sensitive personal data.

Before committing, run:

```bash
dotnet build
dotnet test
git diff --check
git status --short
```

Review staged changes before creating the commit.

## 9. Pull Request Checklist

- [ ] Code follows the architecture and dependency rules.
- [ ] File names and namespaces follow project conventions.
- [ ] No secrets or local-only files are included.
- [ ] Database changes include a reviewed migration.
- [ ] The solution builds successfully.
- [ ] Relevant tests are discovered and pass.
- [ ] `git diff --check` passes.
- [ ] The pull request describes the change and any required migration or configuration steps.
