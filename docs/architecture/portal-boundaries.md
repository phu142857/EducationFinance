# Portal boundaries

This document defines service and data ownership for the Admin Portal and E-Service Portal.

## Runtime boundaries

| Concern | System of record | Other portal |
|---|---|---|
| Staff identity and access claims | Admin Portal | No local copy of staff identity or access data |
| Parent identity, Singpass, MyInfo consent | E-Service Portal | Admin stores only external parent IDs required by a case |
| School, cohort, class, student | Admin Portal | E-Service keeps child/school projections |
| Education account and ledger | Admin Portal | E-Service keeps masked account and transaction projections |
| Fees and assessments | Admin Portal | E-Service keeps charge projections |
| FAS scheme and decision | Admin Portal | E-Service owns drafts and parent-visible status projections |
| Bank payment lifecycle and receipt | E-Service Portal | Admin consumes settlement/refund outcomes |
| CPF transfer | Admin Portal | Not available to E-Service |
| Parent chatbot and notifications | E-Service Portal | Admin may consume authorized audit projections only |

## Database boundary

- Admin Portal uses `AdminPortalConnection` and `AdminPortalDb`.
- E-Service Portal uses `EServicePortalConnection` and `EServicePortalDb`.
- Cross-database foreign keys, joins, credentials, and direct queries are prohibited.
- Admin references an E-Service parent by `ExternalParentId`; this value is not a relational foreign key.
- E-Service read models are eventually consistent and record `LastSyncedAt` where appropriate.

## Access model decision

The latest schema direction removes dynamic `Roles`, `Permissions`, `RolePermissions`, `RoleAssignments`, and `StaffType` from Admin Portal. The detailed HQ/School access matrix in FEAT-044/045 must therefore be enforced by authenticated claims/policies or a future authorization service, not by these database tables.

## Delivery-only requirements

FEAT-052 to FEAT-059 are implemented through tests, pipelines, security evidence, runbooks, and acceptance documentation. They are not runtime business APIs unless separately approved as product functionality.
