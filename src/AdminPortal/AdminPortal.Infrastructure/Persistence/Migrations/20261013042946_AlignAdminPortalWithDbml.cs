using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPortal.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlignAdminPortalWithDbml : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FasAssessments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FeeAssessmentItems",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FeeConfigurations",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_FeeAssessments_StudentId_AcademicYear_TermCode",
                schema: "dbo",
                table: "FeeAssessments");

            migrationBuilder.DropColumn(
                name: "TermCode",
                schema: "dbo",
                table: "FeeAssessments");

            migrationBuilder.DropColumn(
                name: "EffectiveFrom",
                schema: "dbo",
                table: "FasSchemesDetails");

            migrationBuilder.DropColumn(
                name: "EffectiveTo",
                schema: "dbo",
                table: "FasSchemesDetails");

            migrationBuilder.DropColumn(
                name: "RuleDefinition",
                schema: "dbo",
                table: "FasSchemesDetails");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                schema: "dbo",
                table: "FeeAssessments",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "SubmissionChannel",
                schema: "dbo",
                table: "FasApplications",
                newName: "AssessmentOutcome");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "dbo",
                table: "Students",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FeeTypeId",
                schema: "dbo",
                table: "FeeAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Paid",
                schema: "dbo",
                table: "FeeAssessments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                schema: "dbo",
                table: "FeeAssessments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Quator",
                schema: "dbo",
                table: "FasSchemesDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                schema: "dbo",
                table: "FasSchemesDetails",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "name",
                schema: "dbo",
                table: "FasSchemesDetails",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DividedBy",
                schema: "dbo",
                table: "FasSchemes",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                schema: "dbo",
                table: "FasSchemes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AssessedAmount",
                schema: "dbo",
                table: "FasApplications",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AssessedAt",
                schema: "dbo",
                table: "FasApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FasSchemeId",
                schema: "dbo",
                table: "FasApplications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                schema: "dbo",
                table: "AccountTransactions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FeeType",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeeType_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalSchema: "dbo",
                        principalTable: "Schools",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StudentEnrollments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClassId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentEnrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_Classes_ClassId",
                        column: x => x.ClassId,
                        principalSchema: "dbo",
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StudentEnrollments_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "dbo",
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopUpRuleDetails",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TopRuleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Quator = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopUpRuleDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopUpRuleDetails_TopUpRules_TopRuleID",
                        column: x => x.TopRuleID,
                        principalSchema: "dbo",
                        principalTable: "TopUpRules",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FeeTypeDetail",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeeTypeDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeeTypeDetail_FeeType_FeeTypeId",
                        column: x => x.FeeTypeId,
                        principalSchema: "dbo",
                        principalTable: "FeeType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeeAssessments_StudentId_AcademicYear",
                schema: "dbo",
                table: "FeeAssessments",
                columns: new[] { "StudentId", "AcademicYear" });

            migrationBuilder.AddForeignKey(
                name: "FK_FasApplications_FasSchemes_FasSchemeId",
                schema: "dbo",
                table: "FasApplications",
                column: "FasSchemeId",
                principalSchema: "dbo",
                principalTable: "FasSchemes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeeAssessments_FeeType_FeeTypeId",
                schema: "dbo",
                table: "FeeAssessments",
                column: "FeeTypeId",
                principalSchema: "dbo",
                principalTable: "FeeType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FasApplications_FasSchemes_FasSchemeId",
                schema: "dbo",
                table: "FasApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_FeeAssessments_FeeType_FeeTypeId",
                schema: "dbo",
                table: "FeeAssessments");

            migrationBuilder.DropTable(
                name: "FeeTypeDetail",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "StudentEnrollments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "TopUpRuleDetails",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "FeeType",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_FeeAssessments_StudentId_AcademicYear",
                schema: "dbo",
                table: "FeeAssessments");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FeeTypeId",
                schema: "dbo",
                table: "FeeAssessments");

            migrationBuilder.DropColumn(
                name: "Paid",
                schema: "dbo",
                table: "FeeAssessments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                schema: "dbo",
                table: "FeeAssessments");

            migrationBuilder.DropColumn(
                name: "Quator",
                schema: "dbo",
                table: "FasSchemesDetails");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "dbo",
                table: "FasSchemesDetails");

            migrationBuilder.DropColumn(
                name: "name",
                schema: "dbo",
                table: "FasSchemesDetails");

            migrationBuilder.DropColumn(
                name: "DividedBy",
                schema: "dbo",
                table: "FasSchemes");

            migrationBuilder.DropColumn(
                name: "Value",
                schema: "dbo",
                table: "FasSchemes");

            migrationBuilder.DropColumn(
                name: "AssessedAmount",
                schema: "dbo",
                table: "FasApplications");

            migrationBuilder.DropColumn(
                name: "AssessedAt",
                schema: "dbo",
                table: "FasApplications");

            migrationBuilder.DropColumn(
                name: "FasSchemeId",
                schema: "dbo",
                table: "FasApplications");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "dbo",
                table: "AccountTransactions");

            migrationBuilder.RenameColumn(
                name: "Amount",
                schema: "dbo",
                table: "FeeAssessments",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "AssessmentOutcome",
                schema: "dbo",
                table: "FasApplications",
                newName: "SubmissionChannel");

            migrationBuilder.AddColumn<string>(
                name: "TermCode",
                schema: "dbo",
                table: "FeeAssessments",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EffectiveFrom",
                schema: "dbo",
                table: "FasSchemesDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EffectiveTo",
                schema: "dbo",
                table: "FasSchemesDetails",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RuleDefinition",
                schema: "dbo",
                table: "FasSchemesDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "FasAssessments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FasSchemeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    AssessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssessmentOutcome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
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
                name: "FeeConfigurations",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: false),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    FeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FeeName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
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
                name: "FeeAssessmentItems",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeAssessmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FeeConfigurationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssessedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
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
                name: "IX_FeeAssessments_StudentId_AcademicYear_TermCode",
                schema: "dbo",
                table: "FeeAssessments",
                columns: new[] { "StudentId", "AcademicYear", "TermCode" });
        }
    }
}
