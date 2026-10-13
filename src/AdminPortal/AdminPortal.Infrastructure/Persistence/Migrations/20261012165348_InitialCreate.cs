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
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "FasSchemes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchemeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SchemeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SchemeType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasSchemes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MessageVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastError = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MessageVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RetryCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LastError = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Adress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FasSchemesDetails",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleDefinition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasSchemesDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FasSchemesDetails_FasSchemes_FasSchemeId",
                        column: x => x.FasSchemeId,
                        principalSchema: "dbo",
                        principalTable: "FasSchemes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdminUsers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalIdentityId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminUsers_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalSchema: "dbo",
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FiscalYear = table.Column<int>(type: "int", nullable: false),
                    AllocatedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_FasSchemes_FasSchemeId",
                        column: x => x.FasSchemeId,
                        principalSchema: "dbo",
                        principalTable: "FasSchemes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Budgets_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalSchema: "dbo",
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Cohorts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartYear = table.Column<int>(type: "int", nullable: false),
                    EndYear = table.Column<int>(type: "int", nullable: false),
                    LevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cohorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cohorts_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalSchema: "dbo",
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeeConfigurations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeConfigurations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeeConfigurations_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalSchema: "dbo",
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopUpRules",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RuleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EligibilityDefinition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpRules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopUpRules_AdminUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "AdminUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BudgetTransactions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BudgetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BudgetTransactions_Budgets_BudgetId",
                        column: x => x.BudgetId,
                        principalSchema: "dbo",
                        principalTable: "Budgets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CohortId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AcademicYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Cohorts_CohortId",
                        column: x => x.CohortId,
                        principalSchema: "dbo",
                        principalTable: "Cohorts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    EnrollmentStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Classes_CurrentClassId",
                        column: x => x.CurrentClassId,
                        principalSchema: "dbo",
                        principalTable: "Classes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationAccounts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OpenedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationAccounts_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FasApplications",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ExternalFamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SubmissionChannel = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FasApplications_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeeAssessments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    TermCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SubsidyAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PayableAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeeAssessments_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AccountTransactions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopUpRuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BusinessReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountTransactions_EducationAccounts_EducationAccountId",
                        column: x => x.EducationAccountId,
                        principalSchema: "dbo",
                        principalTable: "EducationAccounts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccountTransactions_TopUpRules_TopUpRuleId",
                        column: x => x.TopUpRuleId,
                        principalSchema: "dbo",
                        principalTable: "TopUpRules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FasAssessments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessmentOutcome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AssessedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    AssessedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasAssessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FasAssessments_FasApplications_FasApplicationId",
                        column: x => x.FasApplicationId,
                        principalSchema: "dbo",
                        principalTable: "FasApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FasAssessments_FasSchemes_FasSchemeId",
                        column: x => x.FasSchemeId,
                        principalSchema: "dbo",
                        principalTable: "FasSchemes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FasDecisions",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Decision = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecidedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DecidedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasDecisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FasDecisions_AdminUsers_DecidedByUserId",
                        column: x => x.DecidedByUserId,
                        principalSchema: "dbo",
                        principalTable: "AdminUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FasDecisions_FasApplications_FasApplicationId",
                        column: x => x.FasApplicationId,
                        principalSchema: "dbo",
                        principalTable: "FasApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FasDecisions_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalSchema: "dbo",
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HardcopyFasRecords",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DocumentReference = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CapturedByUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CapturedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerificationStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardcopyFasRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HardcopyFasRecords_AdminUsers_CapturedByUserId",
                        column: x => x.CapturedByUserId,
                        principalSchema: "dbo",
                        principalTable: "AdminUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HardcopyFasRecords_FasApplications_FasApplicationId",
                        column: x => x.FasApplicationId,
                        principalSchema: "dbo",
                        principalTable: "FasApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HardcopyFasRecords_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalSchema: "dbo",
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeeAssessmentItems",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FeeAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SubsidyAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeAssessmentItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeeAssessmentItems_FeeAssessments_FeeAssessmentId",
                        column: x => x.FeeAssessmentId,
                        principalSchema: "dbo",
                        principalTable: "FeeAssessments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FeeAssessmentItems_FeeConfigurations_FeeConfigurationId",
                        column: x => x.FeeConfigurationId,
                        principalSchema: "dbo",
                        principalTable: "FeeConfigurations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_BusinessReference",
                schema: "dbo",
                table: "AccountTransactions",
                column: "BusinessReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountTransactions_EducationAccountId_OccurredAt",
                schema: "dbo",
                table: "AccountTransactions",
                columns: new[] { "EducationAccountId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_AdminUsers_ExternalIdentityId",
                schema: "dbo",
                table: "AdminUsers",
                column: "ExternalIdentityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BudgetTransactions_BusinessReference",
                schema: "dbo",
                table: "BudgetTransactions",
                column: "BusinessReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_CohortId_ClassCode",
                schema: "dbo",
                table: "Classes",
                columns: new[] { "CohortId", "ClassCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cohorts_SchoolId_LevelName_Name",
                schema: "dbo",
                table: "Cohorts",
                columns: new[] { "SchoolId", "LevelName", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationAccounts_AccountNumber",
                schema: "dbo",
                table: "EducationAccounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EducationAccounts_StudentId",
                schema: "dbo",
                table: "EducationAccounts",
                column: "StudentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FasApplications_ApplicationReference",
                schema: "dbo",
                table: "FasApplications",
                column: "ApplicationReference",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FasSchemes_SchemeCode",
                schema: "dbo",
                table: "FasSchemes",
                column: "SchemeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeeAssessments_StudentId_AcademicYear_TermCode",
                schema: "dbo",
                table: "FeeAssessments",
                columns: new[] { "StudentId", "AcademicYear", "TermCode" });

            migrationBuilder.CreateIndex(
                name: "IX_InboxMessages_MessageId_ConsumerName",
                schema: "dbo",
                table: "InboxMessages",
                columns: new[] { "MessageId", "ConsumerName" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InboxMessages_Status_ReceivedAt",
                schema: "dbo",
                table: "InboxMessages",
                columns: new[] { "Status", "ReceivedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_MessageId",
                schema: "dbo",
                table: "OutboxMessages",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OutboxMessages_Status_NextAttemptAt",
                schema: "dbo",
                table: "OutboxMessages",
                columns: new[] { "Status", "NextAttemptAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Schools_SchoolCode",
                schema: "dbo",
                table: "Schools",
                column: "SchoolCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentNumber",
                schema: "dbo",
                table: "Students",
                column: "StudentNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTransactions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "BudgetTransactions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FasAssessments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FasDecisions",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FasSchemesDetails",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FeeAssessmentItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HardcopyFasRecords",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationAccounts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TopUpRules",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Budgets",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FeeAssessments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FeeConfigurations",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FasApplications",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "AdminUsers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FasSchemes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Students",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Classes",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Cohorts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Schools",
                schema: "dbo");
        }
    }
}
