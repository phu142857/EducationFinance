# FAS Domain

## Purpose
Defines the domain model for Financial Assistance Scheme (FAS) applications and subsidy policies.

## Entities
- `FasApplication`
- `FasScheme`
- `SubsidyTier`

## Responsibilities
- Define valid application lifecycle transitions.
- Represent scheme configuration and subsidy tiers.
- Enforce domain rules for application decisions and status changes.

## Boundaries
FAS owns assistance applications and subsidy policy concepts. Fee assessments and payments belong to Finance.

## Guidelines
External identity and income verification are integration concerns. The domain should consume verified results as data and must not call external systems directly.