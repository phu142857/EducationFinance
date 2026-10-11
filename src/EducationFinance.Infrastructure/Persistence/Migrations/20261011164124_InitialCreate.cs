using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EducationFinance.Infrastructure.Persistence.Migrations
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
                name: "Permissions",
                columns: table => new
                {
                    permission_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permission_code = table.Column<string>(type: "varchar(100)", unicode: false, nullable: false),
                    module = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_code = table.Column<string>(type: "varchar(50)", unicode: false, nullable: false),
                    description = table.Column<string>(type: "nvarchar(255)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.role_id);
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
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    singpass_uin_fin = table.Column<string>(type: "varchar(20)", unicode: false, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, nullable: true),
                    phone_number = table.Column<string>(type: "varchar(20)", unicode: false, nullable: true),
                    user_type = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
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
                name: "RolePermissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false),
                    permission_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "Permissions",
                        principalColumn: "permission_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id",
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
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                        name: "FK_AuditEvents_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FASApplications",
                columns: table => new
                {
                    application_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_FASApplications_Users_parent_id",
                        column: x => x.parent_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleAssignments",
                columns: table => new
                {
                    assignment_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    assigned_at = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "SYSDATETIME()"),
                    expires_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAssignments", x => x.assignment_id);
                    table.ForeignKey(
                        name: "FK_RoleAssignments_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleAssignments_Schools_school_id",
                        column: x => x.school_id,
                        principalTable: "Schools",
                        principalColumn: "school_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoleAssignments_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_Classes_Schools_school_id",
                        column: x => x.school_id,
                        principalTable: "Schools",
                        principalColumn: "school_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FeeConfigurations",
                columns: table => new
                {
                    configuration_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    cohort_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    fee_type = table.Column<string>(type: "varchar(30)", unicode: false, nullable: false),
                    component_name = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    academic_term = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false)
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
                    table.ForeignKey(
                        name: "FK_FeeConfigurations_Schools_school_id",
                        column: x => x.school_id,
                        principalTable: "Schools",
                        principalColumn: "school_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    school_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    class_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Students_Classes_class_id",
                        column: x => x.class_id,
                        principalTable: "Classes",
                        principalColumn: "class_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Students_Schools_school_id",
                        column: x => x.school_id,
                        principalTable: "Schools",
                        principalColumn: "school_id",
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
                    parent_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.ForeignKey(
                        name: "FK_FamilyLinks_Users_parent_id",
                        column: x => x.parent_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
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
                    student_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    account_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    term = table.Column<string>(type: "varchar(20)", unicode: false, nullable: false),
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
                    table.ForeignKey(
                        name: "FK_FeeAssessments_Students_student_id",
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
                name: "IX_AuditEvents_user_id",
                table: "AuditEvents",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_school_id",
                table: "Budgets",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_cohort_id",
                table: "Classes",
                column: "cohort_id");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_school_id",
                table: "Classes",
                column: "school_id");

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
                name: "IX_FamilyLinks_parent_id",
                table: "FamilyLinks",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyLinks_student_id",
                table: "FamilyLinks",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_FASApplications_parent_id",
                table: "FASApplications",
                column: "parent_id");

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
                name: "IX_FeeAssessments_student_id",
                table: "FeeAssessments",
                column: "student_id");

            migrationBuilder.CreateIndex(
                name: "IX_FeeConfigurations_cohort_id",
                table: "FeeConfigurations",
                column: "cohort_id");

            migrationBuilder.CreateIndex(
                name: "IX_FeeConfigurations_school_id",
                table: "FeeConfigurations",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_account_id",
                table: "Payments",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_assessment_id",
                table: "Payments",
                column: "assessment_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Permissions_PermissionCode",
                table: "Permissions",
                column: "permission_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignments_role_id",
                table: "RoleAssignments",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignments_school_id",
                table: "RoleAssignments",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAssignments_user_id",
                table: "RoleAssignments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_permission_id",
                table: "RolePermissions",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Roles_RoleCode",
                table: "Roles",
                column: "role_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Schools_SchoolCode",
                table: "Schools",
                column: "school_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_class_id",
                table: "Students",
                column: "class_id");

            migrationBuilder.CreateIndex(
                name: "IX_Students_school_id",
                table: "Students",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Students_BirthCertNumber",
                table: "Students",
                column: "birth_cert_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubsidyTiers_scheme_id",
                table: "SubsidyTiers",
                column: "scheme_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Users_SingpassUinFin",
                table: "Users",
                column: "singpass_uin_fin",
                unique: true,
                filter: "[singpass_uin_fin] IS NOT NULL");
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
                name: "Payments");

            migrationBuilder.DropTable(
                name: "RoleAssignments");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SubsidyTiers");

            migrationBuilder.DropTable(
                name: "TopUpRules");

            migrationBuilder.DropTable(
                name: "FeeAssessments");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "FASSchemes");

            migrationBuilder.DropTable(
                name: "EducationAccounts");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Cohorts");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
