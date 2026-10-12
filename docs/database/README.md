# Database diagrams

- `admin-portal.dbml`: DBML source for `AdminPortalDb`, ready for dbdiagram.io.
- `e-service-portal.dbml`: DBML source for `EServicePortalDb`, ready for dbdiagram.io.
- `admin-portal-db-diagram.md`: Mermaid ER diagram for Admin Portal.
- `e-service-portal-db-diagram.md`: Mermaid ER diagram for E-Service Portal.

The DBML enum types represent C# enums persisted as readable strings through Entity Framework Core `HasConversion<string>()` mappings. Cross-portal identifiers are logical integration references only and never database foreign keys.
