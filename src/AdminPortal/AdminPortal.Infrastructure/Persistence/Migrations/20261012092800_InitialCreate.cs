using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPortal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FASSchemes",
                columns: table => new
                {
                    scheme_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    scheme_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    income_ceiling = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    rules_json = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FASSchemes", x => x.scheme_id);
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
                name: "Schools",
                columns: table => new
                {
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    school_code = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    school_name = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    contact_number = table.Column<string>(type: "varchar(20)", unicode: false, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.school_id);
                });

            migrationBuilder.CreateTable(
                name: "StaffUsers",
                columns: table => new
                {
                    staff_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    username = table.Column<string>(type: "varchar(100)", unicode: false, nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(20)", unicode: false, nullable: true),
                    password_hash = table.Column<string>(type: "varchar(255)", unicode: false, nullable: false),
                    address = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffUsers", x => x.staff_user_id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    birth_cert_number = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    full_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    date_of_birth = table.Column<DateOnly>(type: "date", nullable: false),
                    citizenship = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    enrollment_status = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.student_id);
                });

            migrationBuilder.CreateTable(
                name: "TopUpRules",
                columns: table => new
                {
                    rule_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    rule_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    target_age = table.Column<int>(type: "int", nullable: true),
                    target_level = table.Column<string>(type: "varchar(50)", unicode: false, nullable: true),
                    top_up_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    frequency = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpRules", x => x.rule_id);
                });

            migrationBuilder.CreateTable(
                name: "FASApplications",
                columns: table => new
                {
                    application_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    external_parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    scheme_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    reference_no = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    channel = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    status = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    household_income = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    ml_risk_classification = table.Column<string>(type: "varchar(20)", unicode: false, nullable: true),
                    rejection_reason = table.Column<string>(type: "nvarchar(500)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FASApplications", x => x.application_id);
                    table.ForeignKey(
                        name: "FK_FASApplications_FASSchemes_scheme_id",
                        column: x => x.scheme_id,
                        principalTable: "FASSchemes",
                        principalColumn: "scheme_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubsidyTiers",
                columns: table => new
                {
                    tier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    scheme_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tier_name = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    fee_component_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    subsidy_percentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    fixed_subsidy_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubsidyTiers", x => x.tier_id);
                    table.ForeignKey(
                        name: "FK_SubsidyTiers_FASSchemes_scheme_id",
                        column: x => x.scheme_id,
                        principalTable: "FASSchemes",
                        principalColumn: "scheme_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    budget_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    budget_type = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    allocated_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    utilized_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0.00m),
                    fiscal_year = table.Column<int>(type: "int", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.budget_id);
                    table.ForeignKey(
                        name: "FK_Budgets_Schools_school_id",
                        column: x => x.school_id,
                        principalTable: "Schools",
                        principalColumn: "school_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cohorts",
                columns: table => new
                {
                    cohort_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cohort_year = table.Column<int>(type: "int", nullable: false),
                    level_name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohorts", x => x.cohort_id);
                    table.ForeignKey(
                        name: "FK_Cohorts_Schools_school_id",
                        column: x => x.school_id,
                        principalTable: "Schools",
                        principalColumn: "school_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AuditEvents",
                columns: table => new
                {
                    event_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    staff_user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    action_type = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    target_entity = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    entity_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    old_values = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    new_values = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ip_address = table.Column<string>(type: "varchar(45)", unicode: false, nullable: true),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEvents", x => x.event_id);
                    table.ForeignKey(
                        name: "FK_AuditEvents_StaffUsers_staff_user_id",
                        column: x => x.staff_user_id,
                        principalTable: "StaffUsers",
                        principalColumn: "staff_user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EducationAccounts",
                columns: table => new
                {
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_number = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0.00m),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    auto_created_at = table.Column<DateOnly>(type: "date", nullable: true),
                    scheduled_closure_date = table.Column<DateOnly>(type: "date", nullable: true),
                    cpf_transfer_status = table.Column<string>(type: "varchar(30)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAccounts", x => x.account_id);
                    table.ForeignKey(
                        name: "FK_EducationAccounts_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FamilyLinks",
                columns: table => new
                {
                    link_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    external_parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    relationship_type = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    is_primary = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyLinks", x => x.link_id);
                    table.ForeignKey(
                        name: "FK_FamilyLinks_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    cohort_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    class_name = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    academic_year = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.class_id);
                    table.ForeignKey(
                        name: "FK_Classes_Cohorts_cohort_id",
                        column: x => x.cohort_id,
                        principalTable: "Cohorts",
                        principalColumn: "cohort_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeeConfigurations",
                columns: table => new
                {
                    configuration_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    cohort_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fee_type = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    component_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    academic_term = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    payment_deadline = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeConfigurations", x => x.configuration_id);
                    table.ForeignKey(
                        name: "FK_FeeConfigurations_Cohorts_cohort_id",
                        column: x => x.cohort_id,
                        principalTable: "Cohorts",
                        principalColumn: "cohort_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                columns: table => new
                {
                    transaction_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    transaction_type = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    balance_after = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    reference_no = table.Column<string>(type: "varchar(50)", unicode: false, nullable: true),
                    description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.transaction_id);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_EducationAccounts_account_id",
                        column: x => x.account_id,
                        principalTable: "EducationAccounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeeAssessments",
                columns: table => new
                {
                    assessment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    total_billed_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    subsidy_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0.00m),
                    payable_amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    payment_status = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeAssessments", x => x.assessment_id);
                    table.ForeignKey(
                        name: "FK_FeeAssessments_EducationAccounts_account_id",
                        column: x => x.account_id,
                        principalTable: "EducationAccounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StudentClasses",
                columns: table => new
                {
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    assigned_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    ended_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentClasses", x => new { x.student_id, x.class_id });
                    table.ForeignKey(
                        name: "FK_StudentClasses_Classes_class_id",
                        column: x => x.class_id,
                        principalTable: "Classes",
                        principalColumn: "class_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentClasses_Students_student_id",
                        column: x => x.student_id,
                        principalTable: "Students",
                        principalColumn: "student_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    payment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    assessment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    payment_method = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    receipt_reference = table.Column<string>(type: "varchar(50)", unicode: false, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.payment_id);
                    table.ForeignKey(
                        name: "FK_Payments_EducationAccounts_account_id",
                        column: x => x.account_id,
                        principalTable: "EducationAccounts",
                        principalColumn: "account_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payments_FeeAssessments_assessment_id",
                        column: x => x.assessment_id,
                        principalTable: "FeeAssessments",
                        principalColumn: "assessment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_account_id",
                table: "AccountTransactions",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_AuditEvents_staff_user_id",
                table: "AuditEvents",
                column: "staff_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_school_id",
                table: "Budgets",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_cohort_id",
                table: "Classes",
                column: "cohort_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_school_id",
                table: "Cohorts",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_EducationAccounts_student_id",
                table: "EducationAccounts",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "UQ_EducationAccounts_AccountNumber",
                table: "EducationAccounts",
                column: "account_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyLinks_student_id",
                table: "FamilyLinks",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_FASApplications_scheme_id",
                table: "FASApplications",
                column: "scheme_id");

            migrationBuilder.CreateIndex(
                name: "UQ_FASApplications_ReferenceNo",
                table: "FASApplications",
                column: "reference_no",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeeAssessments_account_id",
                table: "FeeAssessments",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_FeeConfigurations_cohort_id",
                table: "FeeConfigurations",
                column: "cohort_id");

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_processed_at",
                table: "OutboxMessages",
                column: "processed_at");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_account_id",
                table: "Payments",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_assessment_id",
                table: "Payments",
                column: "assessment_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Schools_SchoolCode",
                table: "Schools",
                column: "school_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_StaffUsers_Username",
                table: "StaffUsers",
                column: "username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_class_id",
                table: "StudentClasses",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Students_BirthCertNumber",
                table: "Students",
                column: "birth_cert_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubsidyTiers_scheme_id",
                table: "SubsidyTiers",
                column: "scheme_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransactions");

            migrationBuilder.DropTable(
                name: "AuditEvents");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "FamilyLinks");

            migrationBuilder.DropTable(
                name: "FASApplications");

            migrationBuilder.DropTable(
                name: "FeeConfigurations");

            migrationBuilder.DropTable(
                name: "InboxMessages");

            migrationBuilder.DropTable(
                name: "OutboxMessages");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "StudentClasses");

            migrationBuilder.DropTable(
                name: "SubsidyTiers");

            migrationBuilder.DropTable(
                name: "TopUpRules");

            migrationBuilder.DropTable(
                name: "StaffUsers");

            migrationBuilder.DropTable(
                name: "FeeAssessments");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "FASSchemes");

            migrationBuilder.DropTable(
                name: "EducationAccounts");

            migrationBuilder.DropTable(
                name: "Cohorts");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
