# Organization Module

## Purpose
Manages schools, cohorts, classes, students, and family relationships.

## Responsibilities
- Maintain school and cohort information.
- Manage classes and student assignments.
- Maintain student records and family relationships.
- Provide organizational information for education, finance, and FAS workflows.

## Code Organization
- `Commands/`: School, cohort, class, student, and family-link operations.
- `Queries/`: Organization structure, student details, and family relationships.
- `DTOs/`: Module-specific request and response models.
- `Validators/`: Input and relationship validation.

## Dependencies
- Uses Application persistence and shared abstractions.
- Does not directly handle HTTP concerns or configure database connections.

## Boundaries
Organization owns school and student relationships. Education, Finance, and FAS consume this information through explicit application-level contracts where necessary.