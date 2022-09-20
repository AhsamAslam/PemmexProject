using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Survey.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeSurvey",
                columns: table => new
                {
                    employeeSurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    employeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    managerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    managerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isSurveySubmitted = table.Column<bool>(type: "bit", nullable: false),
                    organizationSurveyId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSurvey", x => x.employeeSurveyId);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSurvey",
                columns: table => new
                {
                    organizationSurveyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizationSurveyDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSurvey", x => x.organizationSurveyId);
                });

            migrationBuilder.CreateTable(
                name: "SurveyQuestion",
                columns: table => new
                {
                    surveyQuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    segmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    segmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    questionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    questionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surveyQuestionEngagement = table.Column<double>(type: "float", nullable: false),
                    surveyQuestionNPS = table.Column<double>(type: "float", nullable: false),
                    surveyQuestionAttrition = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurveyQuestion", x => x.surveyQuestionId);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationSurveyQuestion",
                columns: table => new
                {
                    surveyQuestionId = table.Column<int>(type: "int", nullable: false),
                    organizationSurveyId = table.Column<int>(type: "int", nullable: false),
                    surveyRate = table.Column<int>(type: "int", nullable: true),
                    surveyComments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationSurveyQuestion", x => new { x.surveyQuestionId, x.organizationSurveyId });
                    table.ForeignKey(
                        name: "FK_OrganizationSurveyQuestion_OrganizationSurvey_organizationSurveyId",
                        column: x => x.organizationSurveyId,
                        principalTable: "OrganizationSurvey",
                        principalColumn: "organizationSurveyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationSurveyQuestion_SurveyQuestion_surveyQuestionId",
                        column: x => x.surveyQuestionId,
                        principalTable: "SurveyQuestion",
                        principalColumn: "surveyQuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationSurveyQuestion_organizationSurveyId",
                table: "OrganizationSurveyQuestion",
                column: "organizationSurveyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeSurvey");

            migrationBuilder.DropTable(
                name: "OrganizationSurveyQuestion");

            migrationBuilder.DropTable(
                name: "OrganizationSurvey");

            migrationBuilder.DropTable(
                name: "SurveyQuestion");
        }
    }
}
