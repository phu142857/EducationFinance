# Bank integration

E-Service Portal owns online payment sessions, signed callbacks, settlement status, refunds, and idempotency for FEAT-047 to FEAT-049. Admin Portal receives payment outcome events; it must not call the bank gateway directly.
