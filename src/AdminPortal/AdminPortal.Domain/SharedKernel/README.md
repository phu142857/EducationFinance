# Shared Kernel

## Purpose
Contains small domain abstractions and concepts shared across business modules.

## Contents
- Base entity and aggregate abstractions.
- Domain events and domain-level exceptions.
- Shared value objects or domain concepts only when genuinely needed by multiple modules.

## Guidelines
- Keep the shared kernel small and stable.
- Do not place application services, repositories, database configurations, HTTP models, or integration clients here.
- Avoid introducing abstractions solely for hypothetical future requirements.
- Do not force existing entities to inherit from a base class when their identity model differs.

## Dependencies
The Shared Kernel must not depend on Application, Infrastructure, or API layers.