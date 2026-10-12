# E-Service Portal application modules

The E-Service Portal owns parent-facing journeys and local read projections received from the Admin Portal.

| Module | Feature IDs |
|---|---|
| Identity | FEAT-001, FEAT-002 |
| Parent and Family | FEAT-003, FEAT-004 |
| Education Account Views | FEAT-005 |
| FAS Application | FEAT-029 to FEAT-032 |
| Chatbot | FEAT-041 to FEAT-043 |
| Payments | FEAT-047 to FEAT-049 |

Feature implementations should use vertical slices under this folder. Administrative configuration, account lifecycle, top-up execution, fee generation, budget management, staff RBAC, CPF transfer, and ML risk decisions do not belong in this service.
