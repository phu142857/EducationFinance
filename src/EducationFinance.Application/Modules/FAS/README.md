# FAS Module

## Purpose
Manages Financial Assistance Scheme (FAS) applications, eligibility workflows, subsidy tiers, and application decisions.

## Responsibilities
- Manage FAS application lifecycle and status transitions.
- Support application submission and review workflows.
- Apply configured eligibility rules and subsidy tiers.
- Record decision outcomes and rejection reasons.
- Integrate with external income or identity data through defined abstractions.

## Code Organization
- `Commands/`: Application submission, review, and decision operations.
- `Queries/`: Application details, status, and review lists.
- `DTOs/`: Module-specific request and response models.
- `Validators/`: Input and workflow validation.

## Dependencies
- Uses Application persistence and service abstractions.
- External-system communication is implemented in Infrastructure, not in this module.

## Boundaries
FAS determines assistance eligibility and outcomes. Finance handles fee assessment and payment calculations.