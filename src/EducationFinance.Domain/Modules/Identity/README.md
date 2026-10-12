# Identity Domain

## Purpose
Defines application users, roles, permissions, and role assignments.

## Entities
- `User`
- `Role`
- `Permission`
- `RoleAssignment`
- `RolePermission`

## Responsibilities
- Represent user and authorization data.
- Define valid account-state and role-assignment rules where applicable.
- Provide domain concepts for permission and role management.

## Boundaries
Application identity and authorization data are distinct from the mechanism used to authenticate a person. Token issuance and external authentication integration are not domain responsibilities.

## Guidelines
Do not introduce password, token, or external identity-provider behavior into domain entities unless the confirmed requirements make that behavior part of this domain.