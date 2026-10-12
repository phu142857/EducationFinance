# Education Domain

## Purpose
Defines the domain model for education accounts, account transactions, and automatic top-up rules.

## Entities
- `EducationAccount`
- `AccountTransaction`
- `TopUpRule`

## Responsibilities
- Define valid education-account states and lifecycle rules.
- Protect balance and transaction invariants.
- Define valid top-up rule conditions and constraints.

## Boundaries
Education owns education-account balances and account transactions. Fee assessment and payment processing belong to Finance.

## Guidelines
Business rules should live in domain behavior where appropriate, rather than being scattered across API controllers. Persistence mapping belongs to Infrastructure.