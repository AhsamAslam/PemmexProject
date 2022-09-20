using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Holidays.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyToEmployeeHolidays",
                columns: table => new
                {
                    CompanyToEmployeeHolidayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualHolidaysEntitled = table.Column<int>(type: "int", nullable: false),
                    AccruedHolidaysPreviousYear = table.Column<int>(type: "int", nullable: false),
                    UsedHolidaysCurrentYear = table.Column<int>(type: "int", nullable: false),
                    LeftHolidaysCurrentYear = table.Column<int>(type: "int", nullable: false),
                    ParentalHolidays = table.Column<int>(type: "int", nullable: false),
                    AnnualHolidays = table.Column<int>(type: "int", nullable: false),
                    AgreementAnnualHolidays = table.Column<int>(type: "int", nullable: false),
                    SickLeave = table.Column<int>(type: "int", nullable: false),
                    TimeOffWithoutSalary = table.Column<int>(type: "int", nullable: false),
                    EmployementStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidaySettingsIdentitfier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyToEmployeeHolidays", x => x.CompanyToEmployeeHolidayId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHolidays",
                columns: table => new
                {
                    EmployeeHolidayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    businessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    costcenterIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubsituteIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HolidayStatus = table.Column<int>(type: "int", nullable: false),
                    holidayType = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHolidays", x => x.EmployeeHolidayId);
                });

            migrationBuilder.CreateTable(
                name: "HolidayCalendars",
                columns: table => new
                {
                    HolidayCalendarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Global = table.Column<bool>(type: "bit", nullable: false),
                    Fixed = table.Column<bool>(type: "bit", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayCalendars", x => x.HolidayCalendarId);
                });

            migrationBuilder.CreateTable(
                name: "HolidaySettings",
                columns: table => new
                {
                    HolidaySettingsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidaySettingsIdentitfier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrganizationIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BusinessIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolidayCalendarYear = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaximumLimitHolidayToNextYear = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidaySettings", x => x.HolidaySettingsId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyToEmployeeHolidays");

            migrationBuilder.DropTable(
                name: "EmployeeHolidays");

            migrationBuilder.DropTable(
                name: "HolidayCalendars");

            migrationBuilder.DropTable(
                name: "HolidaySettings");
        }
    }
}
