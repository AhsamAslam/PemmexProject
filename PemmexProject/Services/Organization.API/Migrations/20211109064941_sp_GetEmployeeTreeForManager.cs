using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization.API.Migrations
{
    public partial class sp_GetEmployeeTreeForManager : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[sp_GetEmployeeTreeForManager]
                    @EmployeeIdentifier nvarchar(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                   with emp as(
                    select e.*,b.BusinessIdentifier from Employees e inner join Businesses b on b.Id = e.BusinessId and ManagerIdentifier = @EmployeeIdentifier
                    union all 
                    select p.*,b.BusinessIdentifier from Employees p 
					inner join Businesses b on b.Id = p.BusinessId 
					inner join emp e on e.EmployeeIdentifier = p.ManagerIdentifier 
                    )
                    
                    select * from emp;
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop procedure sp_GetEmployeeTreeForManager");
        }
    }
}
