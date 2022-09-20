using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Compensation.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compensation",
                columns: table => new
                {
                    CompensationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSalary = table.Column<double>(type: "float", nullable: false),
                    AdditionalAgreedPart = table.Column<double>(type: "float", nullable: false),
                    CarBenefit = table.Column<double>(type: "float", nullable: false),
                    InsuranceBenefit = table.Column<double>(type: "float", nullable: false),
                    PhoneBenefit = table.Column<double>(type: "float", nullable: false),
                    EmissionBenefit = table.Column<double>(type: "float", nullable: false),
                    HomeInternetBenefit = table.Column<double>(type: "float", nullable: false),
                    TotalMonthlyPay = table.Column<double>(type: "float", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compensation", x => x.CompensationId);
                });

            migrationBuilder.CreateTable(
                name: "CompensationSalaries",
                columns: table => new
                {
                    CompensationSalaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaseSalary = table.Column<double>(type: "float", nullable: false),
                    AdditionalAgreedPart = table.Column<double>(type: "float", nullable: false),
                    CarBenefit = table.Column<double>(type: "float", nullable: false),
                    InsuranceBenefit = table.Column<double>(type: "float", nullable: false),
                    PhoneBenefit = table.Column<double>(type: "float", nullable: false),
                    EmissionBenefit = table.Column<double>(type: "float", nullable: false),
                    HomeInternetBenefit = table.Column<double>(type: "float", nullable: false),
                    TotalMonthlyPay = table.Column<double>(type: "float", nullable: false),
                    one_time_bonus = table.Column<double>(type: "float", nullable: false),
                    annual_bonus = table.Column<double>(type: "float", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompensationSalaries", x => x.CompensationSalaryId);
                });

            migrationBuilder.CreateTable(
                name: "FunctionalBudgets",
                columns: table => new
                {
                    FunctionalBudgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emp_Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    currencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncreaseInCurrency = table.Column<double>(type: "float", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalBudgets", x => x.FunctionalBudgetId);
                });

            migrationBuilder.CreateTable(
                name: "JobCatalogues",
                columns: table => new
                {
                    jobCatalogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    minimum_salary = table.Column<double>(type: "float", nullable: false),
                    median_salary = table.Column<double>(type: "float", nullable: false),
                    maximum_salary = table.Column<double>(type: "float", nullable: false),
                    annual_bonus = table.Column<double>(type: "float", nullable: false),
                    country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    jobFunction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    acv_bonus_percentage = table.Column<double>(type: "float", nullable: false),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCatalogues", x => x.jobCatalogId);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationBudgets",
                columns: table => new
                {
                    OrganizationBudgetId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    budgetPercentage = table.Column<double>(type: "float", nullable: false),
                    budgetValue = table.Column<double>(type: "float", nullable: false),
                    compulsoryPercentage = table.Column<double>(type: "float", nullable: false),
                    TotalbudgetPercentage = table.Column<double>(type: "float", nullable: false),
                    TotalbudgetValue = table.Column<double>(type: "float", nullable: false),
                    jobFunction = table.Column<int>(type: "int", nullable: false),
                    euro_rate = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationBudgets", x => x.OrganizationBudgetId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compensation");

            migrationBuilder.DropTable(
                name: "CompensationSalaries");

            migrationBuilder.DropTable(
                name: "FunctionalBudgets");

            migrationBuilder.DropTable(
                name: "JobCatalogues");

            migrationBuilder.DropTable(
                name: "OrganizationBudgets");
        }
    }
}
