# Finance Domain

## Purpose
Defines the domain model for fee configuration, fee assessment, payments, and budgets.

## Entities
- `FeeConfiguration`
- `FeeAssessment`
- `Payment`
- `Budget`

## Responsibilities
- Define fee assessment and payable-amount invariants.
- Represent payment lifecycle and valid status transitions.
- Protect financial consistency and budget constraints.

## Boundaries
Finance owns fees, assessments, payments, and budgets. Education owns education-account balances and account transactions.

## Guidelines
Financial calculations must use appropriate decimal precision. Persistence mapping and database transaction coordination belong to Infrastructure and the application execution flow, respectively.