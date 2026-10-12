# E-Service Portal database (`EServicePortalDb`)

Nguồn: migration `20261012092801_InitialCreate` của `EServicePortal.Infrastructure`.

E-Service Portal sở hữu danh tính phụ huynh, Singpass, hồ sơ gia đình, đăng ký hỗ trợ, FAS phía người dân, thanh toán và các read model được đồng bộ từ Admin Portal. Các cột bắt đầu bằng `external_` không tạo foreign key sang Admin database.

```mermaid
erDiagram
    ParentAccounts {
        uniqueidentifier parent_id PK
        varchar singpass_uin_fin UK
        nvarchar name
        varchar email
        varchar phone_number
        ParentAccountStatus status
        datetime2 created_at
        datetime2 updated_at
    }
    ParentProfiles {
        uniqueidentifier parent_id PK, FK
        nvarchar residential_address
        nvarchar mailing_address
        datetime2 updated_at
    }
    AssistedRegistrations {
        uniqueidentifier registration_id PK
        varchar registration_code_hash UK
        varchar parent_identification_hash
        varchar child_birth_certificate_hash
        AssistedRegistrationStatus status
        datetime2 expires_at
        datetime2 verified_at
        uniqueidentifier verified_by_staff_user_id "Admin reference; no FK"
        datetime2 created_at
    }
    ChildProfiles {
        uniqueidentifier child_id PK
        uniqueidentifier external_student_id UK "Admin reference; no FK"
        nvarchar name
        uniqueidentifier school_id "Admin reference; no FK"
        nvarchar school_name
        bit is_eligible
        datetime2 last_synced_at
    }
    FamilyLinks {
        uniqueidentifier link_id PK
        uniqueidentifier parent_id FK
        uniqueidentifier child_id FK
        RelationshipType relationship_type
        bit is_primary
    }
    EducationAccountSummaries {
        uniqueidentifier account_id PK
        uniqueidentifier child_id FK
        varchar account_number_masked
        decimal balance
        EducationAccountStatus status
        datetime2 last_synced_at
    }
    AccountTransactionViews {
        uniqueidentifier transaction_id PK
        uniqueidentifier account_id FK
        AccountTransactionType transaction_type
        decimal amount
        nvarchar description
        datetime2 transaction_date
    }
    ChargeSummaries {
        uniqueidentifier charge_id PK
        uniqueidentifier account_id FK
        uniqueidentifier external_assessment_id UK "Admin reference; no FK"
        nvarchar description
        decimal amount
        decimal outstanding_amount
        ChargeStatus status
        datetime2 last_synced_at
    }
    FasApplications {
        uniqueidentifier application_id PK
        uniqueidentifier parent_id FK
        varchar reference_number UK
        FasApplicationStatus status
        bit is_draft
        decimal household_income
        bit declaration_accepted
        datetime2 submitted_at
        datetime2 updated_at
    }
    Payments {
        uniqueidentifier payment_id PK
        uniqueidentifier parent_id FK
        uniqueidentifier charge_id FK
        uniqueidentifier external_account_id "Admin reference; no FK"
        varchar payment_method
        decimal amount
        PaymentStatus status
        varchar idempotency_key UK
        varchar provider_reference
        datetime2 created_at
        datetime2 updated_at
    }
    Receipts {
        uniqueidentifier receipt_id PK
        uniqueidentifier payment_id FK, UK
        varchar reference_number UK
        decimal amount
        datetime2 issued_at
    }
    ChatConversations {
        uniqueidentifier conversation_id PK
        uniqueidentifier parent_id FK
        ConversationStatus status
        bit consent_to_fas_prefill
        datetime2 started_at
        datetime2 ended_at
    }
    ParentNotifications {
        uniqueidentifier notification_id PK
        uniqueidentifier parent_id FK
        NotificationType notification_type
        nvarchar subject
        NotificationStatus status
        datetime2 created_at
        datetime2 sent_at
    }
    AuditEvents {
        uniqueidentifier event_id PK
        uniqueidentifier parent_id FK
        AuditActionType action_type
        varchar target_entity
        uniqueidentifier entity_id
        nvarchar old_values
        nvarchar new_values
        varchar ip_address
        datetime2 timestamp
    }
    OutboxMessages {
        uniqueidentifier message_id PK
        OutboxMessageType message_type
        nvarchar payload
        datetime2 occurred_at
        datetime2 processed_at
        int retry_count
        nvarchar last_error
    }
    InboxMessages {
        uniqueidentifier message_id PK
        nvarchar consumer PK
        datetime2 processed_at
    }

    ParentAccounts ||--|| ParentProfiles : has
    ParentAccounts ||--o{ FamilyLinks : links
    ChildProfiles ||--o{ FamilyLinks : belongs_to
    ChildProfiles ||--o{ EducationAccountSummaries : displays
    EducationAccountSummaries ||--o{ AccountTransactionViews : displays
    EducationAccountSummaries ||--o{ ChargeSummaries : displays
    ParentAccounts ||--o{ FasApplications : submits
    ParentAccounts ||--o{ Payments : initiates
    ChargeSummaries ||--o{ Payments : paid_by
    Payments ||--o| Receipts : produces
    ParentAccounts ||--o{ ChatConversations : starts
    ParentAccounts ||--o{ ParentNotifications : receives
    ParentAccounts o|--o{ AuditEvents : performs
```

## Ranh giới dữ liệu

- Singpass và hồ sơ phụ huynh chỉ tồn tại trong E-Service Portal.
- `ChildProfiles`, `EducationAccountSummaries`, `AccountTransactionViews` và `ChargeSummaries` là read model cục bộ, không truy vấn trực tiếp Admin database.
- `verified_by_staff_user_id`, `external_student_id`, `school_id`, `external_assessment_id` và `external_account_id` chỉ là ID tích hợp; không có foreign key xuyên database.
- E-Service Portal không chứa RBAC nhân viên, ngân sách, quy tắc top-up hay cấu hình phí quản trị.
