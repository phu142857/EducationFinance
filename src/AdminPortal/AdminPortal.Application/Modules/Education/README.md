# Education Module

## Purpose
Manages education accounts, account balances, transactions, and automatic top-up rules.

## Responsibilities
- Manage education account lifecycle and status.
- Track account balances and financial transactions.
- Define and apply automatic top-up rules.
- Provide account transaction history and balance inquiries.

## Code Organization
- `Commands/`: Account creation, status changes, and top-up operations.
- `Queries/`: Account details, balances, and transaction history.
- `DTOs/`: Request and response models specific to this module.
- `Validators/`: Input and business-operation validation.

## Dependencies
- Uses Application abstractions for persistence and shared services.
- Does not directly access HTTP requests or configure database connections.
- Coordinates with Finance through explicit application-level contracts when required.

## Boundaries
Payment processing and fee assessment belong to Finance. Education owns the education-account lifecycle and its transactions.