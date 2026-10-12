# Admin Portal application modules

The Admin Portal owns staff-facing commands and authoritative administrative data.

| Module | Feature IDs |
|---|---|
| Access Control | FEAT-044, FEAT-045 |
| Education Accounts | FEAT-006 to FEAT-009 |
| Schools and Students | FEAT-010 to FEAT-013 |
| Fees and Assessments | FEAT-014, FEAT-015, FEAT-028 |
| Top-Ups | FEAT-016 to FEAT-018 |
| Search and Views | FEAT-019, FEAT-020 |
| Financial Assistance | FEAT-021 to FEAT-025, FEAT-034, FEAT-035 |
| Risk Assessment | FEAT-026, FEAT-027 |
| Budgets and Reporting | FEAT-036 to FEAT-039 |
| Simulation and Support | FEAT-040 |

Feature implementations should use vertical slices under this folder. A slice may contain its command or query, handler, validator, and response model. Parent authentication, Singpass, MyInfo, chatbot, and parent payments do not belong in this service.
