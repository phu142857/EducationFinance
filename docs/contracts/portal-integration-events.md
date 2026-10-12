# Portal integration contracts

Admin Portal and E-Service Portal do not share projects, DbContexts, tables, or database credentials. They exchange versioned messages and explicit service APIs.

## Admin Portal to E-Service Portal

- `education-account.summary-changed.v1`
- `education-account.transaction-posted.v1`
- `fee.charge-changed.v1`
- `fas.application-status-changed.v1`
- `family.child-profile-changed.v1`

These messages update E-Service read projections. They are not commands and must be safe to replay.

## E-Service Portal to Admin Portal

- `fas.application-submitted.v1`
- `payment.settled.v1`
- `payment.refunded.v1`
- `parent.profile-changed.v1`

## Synchronous operations

- Education-account balance payment calls Admin Portal because Admin owns the ledger and must validate and debit atomically.
- Assisted-registration verification is initiated by a School User in Admin Portal and completed through an authenticated E-Service command.
- HQ parent-view simulation uses a read-only E-Service endpoint and a dedicated service identity.

Every request or message carries a correlation ID, contract version, originating service, occurred-at timestamp, and idempotency/message ID. Each service writes outgoing messages to its own outbox and records consumed IDs in its own inbox.
