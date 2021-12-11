using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskIdentifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedByIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    appliedStatus = table.Column<int>(type: "int", nullable: false),
                    currentTaskStatus = table.Column<int>(type: "int", nullable: false),
                    taskType = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    taskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reasons = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTasks", x => x.TaskId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    notificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isRead = table.Column<bool>(type: "bit", nullable: false),
                    tasks = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.notificationId);
                });

            migrationBuilder.CreateTable(
                name: "organizationApprovalSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    taskType = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationApprovalSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChangeCompensations",
                columns: table => new
                {
                    CompensationTaskId = table.Column<int>(type: "int", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppliedSalary = table.Column<double>(type: "float", nullable: false),
                    BaseSalary = table.Column<double>(type: "float", nullable: false),
                    AdditionalAgreedPart = table.Column<double>(type: "float", nullable: false),
                    NewAdditionalAgreedPart = table.Column<double>(type: "float", nullable: false),
                    TotalMonthlyPay = table.Column<double>(type: "float", nullable: false),
                    NewTotalMonthlyPay = table.Column<double>(type: "float", nullable: false),
                    CarBenefit = table.Column<double>(type: "float", nullable: false),
                    InsuranceBenefit = table.Column<double>(type: "float", nullable: false),
                    PhoneBenefit = table.Column<double>(type: "float", nullable: false),
                    EmissionBenefit = table.Column<double>(type: "float", nullable: false),
                    HomeInternetBenefit = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeCompensations", x => x.CompensationTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeCompensations_BaseTasks_CompensationTaskId",
                        column: x => x.CompensationTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeGrades",
                columns: table => new
                {
                    GradeTaskId = table.Column<int>(type: "int", nullable: false),
                    oldGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeGrades", x => x.GradeTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeGrades_BaseTasks_GradeTaskId",
                        column: x => x.GradeTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeHolidays",
                columns: table => new
                {
                    HolidayTaskId = table.Column<int>(type: "int", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubsituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubsituteIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    holidayType = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeHolidays", x => x.HolidayTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeHolidays_BaseTasks_HolidayTaskId",
                        column: x => x.HolidayTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeManagers",
                columns: table => new
                {
                    ManagerTaskId = table.Column<int>(type: "int", nullable: false),
                    oldManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeManagers", x => x.ManagerTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeManagers_BaseTasks_ManagerTaskId",
                        column: x => x.ManagerTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeTeam",
                columns: table => new
                {
                    TeamTaskId = table.Column<int>(type: "int", nullable: false),
                    managerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeTeam", x => x.TeamTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeTeam_BaseTasks_TeamTaskId",
                        column: x => x.TeamTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeTitles",
                columns: table => new
                {
                    TitleTaskId = table.Column<int>(type: "int", nullable: false),
                    oldTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeTitles", x => x.TitleTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeTitles_BaseTasks_TitleTaskId",
                        column: x => x.TitleTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationApprovalSettingDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rankNo = table.Column<int>(type: "int", nullable: false),
                    ManagerType = table.Column<int>(type: "int", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationApprovalSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationApprovalSettingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_organizationApprovalSettingDetails_organizationApprovalSettings_OrganizationApprovalSettingsId",
                        column: x => x.OrganizationApprovalSettingsId,
                        principalTable: "organizationApprovalSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_organizationApprovalSettingDetails_OrganizationApprovalSettingsId",
                table: "organizationApprovalSettingDetails",
                column: "OrganizationApprovalSettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeCompensations");

            migrationBuilder.DropTable(
                name: "ChangeGrades");

            migrationBuilder.DropTable(
                name: "ChangeHolidays");

            migrationBuilder.DropTable(
                name: "ChangeManagers");

            migrationBuilder.DropTable(
                name: "ChangeTeam");

            migrationBuilder.DropTable(
                name: "ChangeTitles");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "organizationApprovalSettingDetails");

            migrationBuilder.DropTable(
                name: "BaseTasks");

            migrationBuilder.DropTable(
                name: "organizationApprovalSettings");
        }
    }
}
