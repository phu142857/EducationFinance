# Admin Portal database (`AdminPortalDb`)

Nguồn: migration `20261012092800_InitialCreate` của `AdminPortal.Infrastructure`.

Admin Portal sở hữu dữ liệu quản trị, trường/lớp/học sinh, tài khoản giáo dục gốc, cấu hình phí, ngân sách, FAS và RBAC. `external_parent_id` là tham chiếu logic sang E-Service, không phải foreign key xuyên database. Admin Portal không lưu Singpass/MyInfo.

```mermaid
erDiagram
    Schools {
        uniqueidentifier school_id PK
        varchar school_code UK
        nvarchar school_name
        nvarchar address
        varchar contact_number
        datetime2 created_at
    }
    Cohorts {
        uniqueidentifier cohort_id PK
        uniqueidentifier school_id FK
        int cohort_year
        nvarchar level_name
        bit is_active
    }
    Classes {
        uniqueidentifier class_id PK
        uniqueidentifier cohort_id FK
        varchar class_name
        varchar academic_year
    }
    Students {
        uniqueidentifier student_id PK
        varchar birth_cert_number UK
        nvarchar full_name
        date date_of_birth
        varchar citizenship
        StudentEnrollmentStatus enrollment_status
        datetime2 created_at
    }
    StudentClasses {
        uniqueidentifier student_id PK, FK
        uniqueidentifier class_id PK, FK
        datetime2 assigned_at
        datetime2 ended_at
    }
    FamilyLinks {
        uniqueidentifier link_id PK
        uniqueidentifier external_parent_id "E-Service reference; no FK"
        uniqueidentifier student_id FK
        RelationshipType relationship_type
        bit is_primary
    }
    EducationAccounts {
        uniqueidentifier account_id PK
        uniqueidentifier student_id FK
        varchar account_number UK
        decimal balance
        EducationAccountStatus status
        date auto_created_at
        date scheduled_closure_date
        CpfTransferStatus cpf_transfer_status
    }
    AccountTransactions {
        uniqueidentifier transaction_id PK
        uniqueidentifier account_id FK
        AccountTransactionType transaction_type
        decimal amount
        decimal balance_after
        varchar reference_no
        nvarchar description
        datetime2 created_at
    }
    FeeAssessments {
        uniqueidentifier assessment_id PK
        uniqueidentifier account_id FK
        decimal total_billed_amount
        decimal subsidy_amount
        decimal payable_amount
        FeeAssessmentPaymentStatus payment_status
        datetime2 created_at
    }
    Payments {
        uniqueidentifier payment_id PK
        uniqueidentifier assessment_id FK
        uniqueidentifier account_id FK
        varchar payment_method
        decimal amount
        PaymentStatus status
        varchar receipt_reference
        datetime2 created_at
    }
    FeeConfigurations {
        uniqueidentifier configuration_id PK
        uniqueidentifier cohort_id FK
        FeeType fee_type
        nvarchar component_name
        decimal amount
        varchar academic_term
        date payment_deadline
    }
    Budgets {
        uniqueidentifier budget_id PK
        uniqueidentifier school_id FK
        BudgetType budget_type
        decimal allocated_amount
        decimal utilized_amount
        int fiscal_year
        datetime2 updated_at
    }
    TopUpRules {
        uniqueidentifier rule_id PK
        nvarchar rule_name
        int target_age
        varchar target_level
        decimal top_up_amount
        varchar frequency
        bit is_active
        datetime2 created_at
    }
    FASSchemes {
        uniqueidentifier scheme_id PK
        nvarchar scheme_name
        decimal income_ceiling
        nvarchar rules_json
        bit is_active
    }
    SubsidyTiers {
        uniqueidentifier tier_id PK
        uniqueidentifier scheme_id FK
        varchar tier_name
        nvarchar fee_component_name
        decimal subsidy_percentage
        decimal fixed_subsidy_amount
    }
    FASApplications {
        uniqueidentifier application_id PK
        uniqueidentifier external_parent_id "E-Service reference; no FK"
        uniqueidentifier scheme_id FK
        varchar reference_no UK
        varchar channel
        FasApplicationStatus status
        decimal household_income
        varchar ml_risk_classification
        nvarchar rejection_reason
        datetime2 created_at
        datetime2 updated_at
    }
    StaffUsers {
        uniqueidentifier staff_user_id PK
        varchar username UK
        nvarchar name
        varchar email
        varchar phone_number
        varchar password_hash
        nvarchar address
        bit is_active
        datetime2 created_at
        datetime2 updated_at
    }
    AuditEvents {
        uniqueidentifier event_id PK
        uniqueidentifier staff_user_id FK
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

    Schools ||--o{ Cohorts : contains
    Cohorts ||--o{ Classes : groups
    Students ||--o{ StudentClasses : memberships
    Classes ||--o{ StudentClasses : memberships
    Students ||--o{ FamilyLinks : linked_to
    Students ||--o{ EducationAccounts : owns
    EducationAccounts ||--o{ AccountTransactions : records
    EducationAccounts ||--o{ FeeAssessments : charged_to
    FeeAssessments ||--o{ Payments : settled_by
    EducationAccounts o|--o{ Payments : funds
    Cohorts o|--o{ FeeConfigurations : scopes
    Schools o|--o{ Budgets : receives
    FASSchemes ||--o{ SubsidyTiers : defines
    FASSchemes ||--o{ FASApplications : evaluates
    StaffUsers o|--o{ AuditEvents : performs
```

## Ranh giới dữ liệu

- Không có bảng `ParentAccounts`, Singpass, MyInfo, chatbot hay hồ sơ phụ huynh.
- `FamilyLinks.external_parent_id` và `FASApplications.external_parent_id` chỉ là ID tích hợp; đồng bộ qua API/event + outbox/inbox.
- `TopUpRules`, `Budgets` và cấu hình phí chỉ thuộc Admin Portal.
- Không còn các bảng role/permission/role-assignment hoặc cột staff type; access matrix sẽ được thực thi bằng claims/policies bên ngoài schema này.
