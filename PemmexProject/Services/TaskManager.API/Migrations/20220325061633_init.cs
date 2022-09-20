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
                    RequesterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    appliedStatus = table.Column<int>(type: "int", nullable: false),
                    currentTaskStatus = table.Column<int>(type: "int", nullable: false),
                    taskType = table.Column<int>(type: "int", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    taskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reasons = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "BonusSettings",
                columns: table => new
                {
                    BonusSettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    limit_percentage = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusSettings", x => x.BonusSettingsId);
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
                name: "BonusTask",
                columns: table => new
                {
                    BonusTaskId = table.Column<int>(type: "int", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    one_time_bonus = table.Column<double>(type: "float", nullable: false),
                    salary = table.Column<double>(type: "float", nullable: false),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusTask", x => x.BonusTaskId);
                    table.ForeignKey(
                        name: "FK_BonusTask_BaseTasks_BonusTaskId",
                        column: x => x.BonusTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetPromotions",
                columns: table => new
                {
                    BudgetTaskId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPromotions", x => x.BudgetTaskId);
                    table.ForeignKey(
                        name: "FK_BudgetPromotions_BaseTasks_BudgetTaskId",
                        column: x => x.BudgetTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
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
                    HomeInternetBenefit = table.Column<double>(type: "float", nullable: false),
                    currencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "ChangeEmployeeHardTargets",
                columns: table => new
                {
                    ChangeEmployeeHardTargetsTaskId = table.Column<int>(type: "int", nullable: false),
                    Emp_Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeDob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractualOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobFunction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HardTargetsId = table.Column<int>(type: "int", nullable: false),
                    HardTargetsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HardTargetsDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeasurementCriteria = table.Column<int>(type: "int", nullable: false),
                    PerformanceCriteria = table.Column<int>(type: "int", nullable: false),
                    Weightage = table.Column<double>(type: "float", nullable: false),
                    EvaluationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeEmployeeHardTargets", x => x.ChangeEmployeeHardTargetsTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeEmployeeHardTargets_BaseTasks_ChangeEmployeeHardTargetsTaskId",
                        column: x => x.ChangeEmployeeHardTargetsTaskId,
                        principalTable: "BaseTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChangeEmployeeSoftTargets",
                columns: table => new
                {
                    ChangeEmployeeTargetsTaskId = table.Column<int>(type: "int", nullable: false),
                    Emp_Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeDob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractualOrganization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobFunction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftTargetsId = table.Column<int>(type: "int", nullable: false),
                    SoftTargetsName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftTargetsDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoftTargetsMultiplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformanceCriteria = table.Column<int>(type: "int", nullable: false),
                    EvaluationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeEmployeeSoftTargets", x => x.ChangeEmployeeTargetsTaskId);
                    table.ForeignKey(
                        name: "FK_ChangeEmployeeSoftTargets_BaseTasks_ChangeEmployeeTargetsTaskId",
                        column: x => x.ChangeEmployeeTargetsTaskId,
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
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubsituteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubsituteIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubsituteName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    holidayType = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    oldManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldCostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newCostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldbusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newbusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newbusinessName = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    managerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldCostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newCostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newCostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    oldbusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    newbusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "BudgetPromotionDetails",
                columns: table => new
                {
                    ChangeBudgetPromotionDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BudgetTaskId = table.Column<int>(type: "int", nullable: false),
                    Emp_Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostCenterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganizationCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobFunction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSalary = table.Column<double>(type: "float", nullable: false),
                    AdditionalAgreedPart = table.Column<double>(type: "float", nullable: false),
                    TotalCurrentSalary = table.Column<double>(type: "float", nullable: false),
                    mandatoryPercentage = table.Column<double>(type: "float", nullable: false),
                    IncreaseInPercentage = table.Column<double>(type: "float", nullable: false),
                    NewBaseSalary = table.Column<double>(type: "float", nullable: false),
                    NewTotalSalary = table.Column<double>(type: "float", nullable: false),
                    currencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncreaseInCurrency = table.Column<double>(type: "float", nullable: false),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPromotionDetails", x => x.ChangeBudgetPromotionDetailId);
                    table.ForeignKey(
                        name: "FK_BudgetPromotionDetails_BudgetPromotions_BudgetTaskId",
                        column: x => x.BudgetTaskId,
                        principalTable: "BudgetPromotions",
                        principalColumn: "BudgetTaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPromotionDetails_BudgetTaskId",
                table: "BudgetPromotionDetails",
                column: "BudgetTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_organizationApprovalSettingDetails_OrganizationApprovalSettingsId",
                table: "organizationApprovalSettingDetails",
                column: "OrganizationApprovalSettingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BonusSettings");

            migrationBuilder.DropTable(
                name: "BonusTask");

            migrationBuilder.DropTable(
                name: "BudgetPromotionDetails");

            migrationBuilder.DropTable(
                name: "ChangeCompensations");

            migrationBuilder.DropTable(
                name: "ChangeEmployeeHardTargets");

            migrationBuilder.DropTable(
                name: "ChangeEmployeeSoftTargets");

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
                name: "BudgetPromotions");

            migrationBuilder.DropTable(
                name: "organizationApprovalSettings");

            migrationBuilder.DropTable(
                name: "BaseTasks");
        }
    }
}
