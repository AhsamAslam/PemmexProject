using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization.API.Migrations
{
    public partial class sp_GetBusinessUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[sp_GetBusinessUnits]
                    @OrganizationIdentifier nvarchar(100)
                AS
                BEGIN
                     declare @employees table(identifier nvarchar(100),costcenterName nvarchar(100))
                        insert into @employees
                        select e.EmployeeIdentifier,c.CostCenterName from Employees e inner join CostCenters c
                        on c.CostCenterId = e.CostCenterId
                        and e.ManagerIdentifier =
                        (select top 1 EmployeeIdentifier from Employees
                        inner join Businesses b on ParentBusinessId = @OrganizationIdentifier
                        where Title ='CEO') ;with ctr(EmployeeIdentifier,Grade,ManagerIdentifier,FirstName,LastName,Title,JobFunction,businessIdentifier,CostCenterName,BUnitIdentifier, isBunit) as(
                        select e.EmployeeIdentifier,e.Grade,e.ManagerIdentifier,e.FirstName,e.LastName,e.Title,e.JobFunction,e.organizationIdentifier as businessIdentifier,emp.costcenterName as CostCenterName,e.EmployeeIdentifier as BUnitIdentifier,  Cast(1 as bit) as isBunit from Employees e inner join @employees emp on e.EmployeeIdentifier = emp.identifier
                        union all
                        select e.EmployeeIdentifier,e.Grade,e.ManagerIdentifier,e.FirstName,e.LastName,e.Title,e.JobFunction,e.organizationIdentifier as businessIdentifier,c.CostCenterName,c.BUnitIdentifier,  Cast(0 as bit) as isBunit from Employees e inner join ctr c on e.ManagerIdentifier = c.EmployeeIdentifier
                        )
                        select * from ctr

                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop procedure sp_GetBusinessUnits");
        }
    }
}
