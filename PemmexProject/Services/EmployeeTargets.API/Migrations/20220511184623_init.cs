using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeTargets.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HardTargets",
                columns: table => new
                {
                    HardTargetsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HardTargetsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HardTargetsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MeasurementCriteria = table.Column<int>(type: "int", nullable: false),
                    MeasurementCriteriaResult = table.Column<double>(type: "float", nullable: false),
                    Weightage = table.Column<double>(type: "float", nullable: false),
                    OrganizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    TargetType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardTargets", x => x.HardTargetsId);
                });

            migrationBuilder.CreateTable(
                name: "HardTargetsDetail",
                columns: table => new
                {
                    HardTargetsDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HardTargetsId = table.Column<int>(type: "int", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HardTargetsDetail", x => x.HardTargetsDetailId);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEvaluationSettings",
                columns: table => new
                {
                    performanceEvaluationSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organizationIdentifier = table.Column<double>(type: "float", nullable: false),
                    minimumPercentage = table.Column<double>(type: "float", nullable: false),
                    targetPercentage = table.Column<double>(type: "float", nullable: false),
                    maximumPercentage = table.Column<double>(type: "float", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEvaluationSettings", x => x.performanceEvaluationSettingsId);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceEvaluationSummary",
                columns: table => new
                {
                    performanceEvaluationSummaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    jobFunction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    totalSalary = table.Column<double>(type: "float", nullable: false),
                    bonusPercentage = table.Column<double>(type: "float", nullable: false),
                    bonusAmount = table.Column<double>(type: "float", nullable: false),
                    resultedBonusPercentage = table.Column<double>(type: "float", nullable: false),
                    resultedBonusAmountBeforeMultiplier = table.Column<double>(type: "float", nullable: false),
                    performanceMultiplier = table.Column<double>(type: "float", nullable: false),
                    resultedBonusAmountAfterMultiplier = table.Column<double>(type: "float", nullable: false),
                    companyProfitabilityMultiplier = table.Column<double>(type: "float", nullable: false),
                    finalBonusAmount = table.Column<double>(type: "float", nullable: false),
                    bonusPayoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    employeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    managerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceEvaluationSummary", x => x.performanceEvaluationSummaryId);
                });

            migrationBuilder.CreateTable(
                name: "PerfromanceBudgetPlanning",
                columns: table => new
                {
                    perfromanceBudgetPlanningId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    companyProfitabilityMultiplier = table.Column<double>(type: "float", nullable: false),
                    bonusPayoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfromanceBudgetPlanning", x => x.perfromanceBudgetPlanningId);
                });

            migrationBuilder.CreateTable(
                name: "SoftTargets",
                columns: table => new
                {
                    SoftTargetsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftTargetsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoftTargetsDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PerformanceCriteria = table.Column<int>(type: "int", nullable: false),
                    OrganizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EvaluationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    TargetType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftTargets", x => x.SoftTargetsId);
                });

            migrationBuilder.CreateTable(
                name: "SoftTargetsDetail",
                columns: table => new
                {
                    SoftTargetsDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoftTargetsId = table.Column<int>(type: "int", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoftTargetsDetail", x => x.SoftTargetsDetailId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HardTargets");

            migrationBuilder.DropTable(
                name: "HardTargetsDetail");

            migrationBuilder.DropTable(
                name: "PerformanceEvaluationSettings");

            migrationBuilder.DropTable(
                name: "PerformanceEvaluationSummary");

            migrationBuilder.DropTable(
                name: "PerfromanceBudgetPlanning");

            migrationBuilder.DropTable(
                name: "SoftTargets");

            migrationBuilder.DropTable(
                name: "SoftTargetsDetail");
        }
    }
}
