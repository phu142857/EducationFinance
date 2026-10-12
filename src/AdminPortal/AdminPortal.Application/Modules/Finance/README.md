# Finance Module

## Purpose
Manages fee configurations, fee assessments, payments, and budgets.

## Responsibilities
- Maintain fee configuration and billing periods.
- Calculate fee assessments and payable amounts.
- Track payment status and payment references.
- Manage budget information and financial reporting queries.
- Coordinate with Education when an education account is used for payment.

## Code Organization
- `Commands/`: Fee configuration, assessment, payment, and budget operations.
- `Queries/`: Assessments, payment history, outstanding fees, and budget summaries.
- `DTOs/`: Module-specific request and response models.
- `Validators/`: Input and financial-operation validation.

## Dependencies
- Uses Application persistence and service abstractions.
- Financial operations must preserve transaction consistency and auditability.

## Boundaries
Finance owns fees and payment records. Education owns education-account balances and account transactions. Cross-module operations must use explicit contracts rather than directly depending on another module's internal implementation.