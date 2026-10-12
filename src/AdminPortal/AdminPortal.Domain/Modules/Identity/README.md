# Identity Domain

## Purpose
Defines local internal staff-user identity data.

## Entities
- `StaffUser` stores local credentials as a password hash and contact/address data.
- Dynamic role, permission, and role-assignment tables are intentionally not part of this model.

## Responsibilities
- Represent internal staff identity and account activation state.
- Keep password material as a one-way hash only.
- Leave access authorization to claims/policies outside this domain model.

## Boundaries
Parent identity, Singpass, and MyInfo belong to E-Service Portal. Admin Portal identity contains only HQ and school staff concepts.

## Guidelines
Never store plaintext passwords. Token and external identity-provider behavior belongs to infrastructure/security adapters.
