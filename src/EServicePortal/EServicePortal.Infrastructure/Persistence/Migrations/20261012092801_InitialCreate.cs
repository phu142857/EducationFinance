using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EServicePortal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssistedRegistrations",
                columns: table => new
                {
                    registration_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    registration_code_hash = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    parent_identification_hash = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    child_birth_certificate_hash = table.Column<string>(type: "varchar(128)", unicode: false, maxLength: 128, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    verified_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    verified_by_staff_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssistedRegistrations", x => x.registration_id);
                });

            migrationBuilder.CreateTable(
                name: "ChildProfiles",
                columns: table => new
                {
                    child_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    external_student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    school_name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    is_eligible = table.Column<bool>(type: "bit", nullable: false),
                    last_synced_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildProfiles", x => x.child_id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                columns: table => new
                {
                    message_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    consumer = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    processed_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => new { x.message_id, x.consumer });
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table => new
                {
                    message_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    message_type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    occurred_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    processed_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    retry_count = table.Column<int>(type: "int", nullable: false),
                    last_error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.message_id);
                });

            migrationBuilder.CreateTable(
                name: "ParentAccounts",
                columns: table => new
                {
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    singpass_uin_fin = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "varchar(254)", unicode: false, maxLength: 254, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentAccounts", x => x.parent_id);
                });

            migrationBuilder.CreateTable(
                name: "EducationAccountSummaries",
                columns: table => new
                {
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    child_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_number_masked = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    last_synced_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAccountSummaries", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_EducationAccountSummaries_ChildProfiles_child_id",
                        column: x => x.child_id,
                        principalTable: "ChildProfiles",
                        principalColumn: "child_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditEvents",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    action_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    target_entity = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    entity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    old_values = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    new_values = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip_address = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEvents", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_AuditEvents_ParentAccounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "ParentAccounts",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChatConversations",
                columns: table => new
                {
                    conversation_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    consent_to_fas_prefill = table.Column<bool>(type: "bit", nullable: false),
                    started_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ended_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatConversations", x => x.conversation_id);
                    table.ForeignKey(
                        name: "FK_ChatConversations_ParentAccounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "ParentAccounts",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamilyLinks",
                columns: table => new
                {
                    link_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    child_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    relationship_type = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    is_primary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyLinks", x => x.link_id);
                    table.ForeignKey(
                        name: "FK_FamilyLinks_ChildProfiles_child_id",
                        column: x => x.child_id,
                        principalTable: "ChildProfiles",
                        principalColumn: "child_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FamilyLinks_ParentAccounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "ParentAccounts",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FasApplications",
                columns: table => new
                {
                    application_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reference_number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    is_draft = table.Column<bool>(type: "bit", nullable: false),
                    household_income = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    declaration_accepted = table.Column<bool>(type: "bit", nullable: false),
                    submitted_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasApplications", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_FasApplications_ParentAccounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "ParentAccounts",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParentNotifications",
                columns: table => new
                {
                    notification_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    notification_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    sent_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentNotifications", x => x.notification_id);
                    table.ForeignKey(
                        name: "FK_ParentNotifications_ParentAccounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "ParentAccounts",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ParentProfiles",
                columns: table => new
                {
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    residential_address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    mailing_address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentProfiles", x => x.parent_id);
                    table.ForeignKey(
                        name: "FK_ParentProfiles_ParentAccounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "ParentAccounts",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactionViews",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_type = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    transaction_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactionViews", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_AccountTransactionViews_EducationAccountSummaries_account_id",
                        column: x => x.account_id,
                        principalTable: "EducationAccountSummaries",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChargeSummaries",
                columns: table => new
                {
                    charge_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    external_assessment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    outstanding_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    last_synced_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargeSummaries", x => x.charge_id);
                    table.ForeignKey(
                        name: "FK_ChargeSummaries_EducationAccountSummaries_account_id",
                        column: x => x.account_id,
                        principalTable: "EducationAccountSummaries",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    payment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    charge_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    external_account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_method = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: false),
                    idempotency_key = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    provider_reference = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_Payments_ChargeSummaries_charge_id",
                        column: x => x.charge_id,
                        principalTable: "ChargeSummaries",
                        principalColumn: "charge_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_ParentAccounts_parent_id",
                        column: x => x.parent_id,
                        principalTable: "ParentAccounts",
                        principalColumn: "parent_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    receipt_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    payment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reference_number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    issued_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.receipt_id);
                    table.ForeignKey(
                        name: "FK_Receipts_Payments_payment_id",
                        column: x => x.payment_id,
                        principalTable: "Payments",
                        principalColumn: "payment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactionViews_account_id",
                table: "AccountTransactionViews",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_AssistedRegistrations_registration_code_hash",
                table: "AssistedRegistrations",
                column: "registration_code_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuditEvents_parent_id",
                table: "AuditEvents",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeSummaries_account_id",
                table: "ChargeSummaries",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_ChargeSummaries_external_assessment_id",
                table: "ChargeSummaries",
                column: "external_assessment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatConversations_parent_id",
                table: "ChatConversations",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_ChildProfiles_external_student_id",
                table: "ChildProfiles",
                column: "external_student_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationAccountSummaries_child_id",
                table: "EducationAccountSummaries",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyLinks_child_id",
                table: "FamilyLinks",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyLinks_parent_id_child_id",
                table: "FamilyLinks",
                columns: new[] { "parent_id", "child_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FasApplications_parent_id",
                table: "FasApplications",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_FasApplications_reference_number",
                table: "FasApplications",
                column: "reference_number",
                unique: true,
                filter: "[reference_number] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_processed_at",
                table: "OutboxMessages",
                column: "processed_at");

            migrationBuilder.CreateIndex(
                name: "IX_ParentAccounts_singpass_uin_fin",
                table: "ParentAccounts",
                column: "singpass_uin_fin",
                unique: true,
                filter: "[singpass_uin_fin] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ParentNotifications_parent_id",
                table: "ParentNotifications",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_charge_id",
                table: "Payments",
                column: "charge_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_idempotency_key",
                table: "Payments",
                column: "idempotency_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_parent_id",
                table: "Payments",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_payment_id",
                table: "Receipts",
                column: "payment_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_reference_number",
                table: "Receipts",
                column: "reference_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransactionViews");

            migrationBuilder.DropTable(
                name: "AssistedRegistrations");

            migrationBuilder.DropTable(
                name: "AuditEvents");

            migrationBuilder.DropTable(
                name: "ChatConversations");

            migrationBuilder.DropTable(
                name: "FamilyLinks");

            migrationBuilder.DropTable(
                name: "FasApplications");

            migrationBuilder.DropTable(
                name: "InboxMessages");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "ParentNotifications");

            migrationBuilder.DropTable(
                name: "ParentProfiles");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "ChargeSummaries");

            migrationBuilder.DropTable(
                name: "ParentAccounts");

            migrationBuilder.DropTable(
                name: "EducationAccountSummaries");

            migrationBuilder.DropTable(
                name: "ChildProfiles");
        }
    }
}
