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
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Families",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyReference = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InboxMessages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MessageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConsumerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MessageType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MessageVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttemptCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
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
                    MessageType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MessageVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Payload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Destination = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NextAttemptAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AttemptCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    LockedUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PublishedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastError = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExternalIdentitySubject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MailingAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RegistrationMethod = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentReadModels",
                schema: "dbo",
                columns: table => new
                {
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentNumber = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EnrollmentStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentReadModels", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "FasApplicationReadModels",
                schema: "dbo",
                columns: table => new
                {
                    FasApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationReference = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OutcomeSummary = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastStatusChangedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasApplicationReadModels", x => x.FasApplicationId);
                    table.ForeignKey(
                        name: "FK_FasApplicationReadModels_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalSchema: "dbo",
                        principalTable: "Families",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FasApplicationDrafts",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedByParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DraftPayload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DraftVersion = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    DraftStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SubmittedApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FasApplicationDrafts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FasApplicationDrafts_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalSchema: "dbo",
                        principalTable: "Families",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FasApplicationDrafts_Parents_CreatedByParentId",
                        column: x => x.CreatedByParentId,
                        principalSchema: "dbo",
                        principalTable: "Parents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationAccountReadModels",
                schema: "dbo",
                columns: table => new
                {
                    EducationAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumberMasked = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    CurrencyCode = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false, defaultValue: "SGD"),
                    AccountStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationAccountReadModels", x => x.EducationAccountId);
                    table.ForeignKey(
                        name: "FK_EducationAccountReadModels_StudentReadModels_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "StudentReadModels",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "FamilyMembers",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FamilyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    RelationshipType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IsPrimaryContact = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyMembers", x => x.Id);
                    table.CheckConstraint("CK_FamilyMembers_ExactlyOneMember", "([ParentId] IS NOT NULL AND [StudentId] IS NULL) OR ([ParentId] IS NULL AND [StudentId] IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Families_FamilyId",
                        column: x => x.FamilyId,
                        principalSchema: "dbo",
                        principalTable: "Families",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FamilyMembers_Parents_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "Parents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FamilyMembers_StudentReadModels_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "StudentReadModels",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "FeeAssessmentReadModels",
                schema: "dbo",
                columns: table => new
                {
                    FeeAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcademicYear = table.Column<int>(type: "int", nullable: true),
                    TermCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    SubsidyAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    PayableAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    AssessmentStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    SyncedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeAssessmentReadModels", x => x.FeeAssessmentId);
                    table.ForeignKey(
                        name: "FK_FeeAssessmentReadModels_StudentReadModels_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "StudentReadModels",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateTable(
                name: "PaymentRequests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EducationAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CurrencyCode = table.Column<string>(type: "char(3)", maxLength: 3, nullable: false, defaultValue: "SGD"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ExternalReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FailureReason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRequests_EducationAccountReadModels_EducationAccountId",
                        column: x => x.EducationAccountId,
                        principalSchema: "dbo",
                        principalTable: "EducationAccountReadModels",
                        principalColumn: "EducationAccountId");
                    table.ForeignKey(
                        name: "FK_PaymentRequests_FeeAssessmentReadModels_FeeAssessmentId",
                        column: x => x.FeeAssessmentId,
                        principalSchema: "dbo",
                        principalTable: "FeeAssessmentReadModels",
                        principalColumn: "FeeAssessmentId");
                    table.ForeignKey(
                        name: "FK_PaymentRequests_Parents_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "dbo",
                        principalTable: "Parents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationAccountReadModels_StudentId",
                schema: "dbo",
                table: "EducationAccountReadModels",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Families_FamilyReference",
                schema: "dbo",
                table: "Families",
                column: "FamilyReference",
                unique: true,
                filter: "[FamilyReference] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_FamilyId",
                schema: "dbo",
                table: "FamilyMembers",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_ParentId",
                schema: "dbo",
                table: "FamilyMembers",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FamilyMembers_StudentId",
                schema: "dbo",
                table: "FamilyMembers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_FasApplicationDrafts_FamilyId_DraftStatus",
                schema: "dbo",
                table: "FasApplicationDrafts",
                columns: new[] { "FamilyId", "DraftStatus" });

            migrationBuilder.CreateIndex(
                name: "IX_FasApplicationReadModels_FamilyId",
                schema: "dbo",
                table: "FasApplicationReadModels",
                column: "FamilyId");

            migrationBuilder.CreateIndex(
                name: "IX_FeeAssessmentReadModels_StudentId",
                schema: "dbo",
                table: "FeeAssessmentReadModels",
                column: "StudentId");

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
                name: "IX_OutboxMessages_Status_NextAttemptAt",
                schema: "dbo",
                table: "OutboxMessages",
                columns: new[] { "Status", "NextAttemptAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Parents_ExternalIdentitySubject",
                schema: "dbo",
                table: "Parents",
                column: "ExternalIdentitySubject",
                unique: true,
                filter: "[ExternalIdentitySubject] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRequests_ParentId_CreatedAt",
                schema: "dbo",
                table: "PaymentRequests",
                columns: new[] { "ParentId", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyMembers",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FasApplicationDrafts",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FasApplicationReadModels",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "InboxMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PaymentRequests",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Families",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "EducationAccountReadModels",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FeeAssessmentReadModels",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Parents",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentReadModels",
                schema: "dbo");
        }
    }
}
