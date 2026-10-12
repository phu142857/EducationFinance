# Organization Domain

## Purpose
Defines the organizational structure and relationships among schools, cohorts, classes, students, and families.

## Entities
- `School`
- `Cohort`
- `Class`
- `Student`
- `FamilyLink`

## Responsibilities
- Represent school and cohort structure.
- Define valid class and student relationships.
- Represent family relationships associated with students.

## Boundaries
Organization owns school, class, student, and family-link data. Education, Finance, and FAS may use this information but should not redefine its ownership.

## Guidelines
Relationship constraints should be enforced through appropriate domain rules and persistence constraints. Database mapping belongs to Infrastructure.