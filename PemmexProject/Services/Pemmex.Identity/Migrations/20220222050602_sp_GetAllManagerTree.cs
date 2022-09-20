using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pemmex.Identity.Migrations
{
    public partial class sp_GetAllManagerTree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[sp_GetAllManagerTree]
                    @EmployeeIdentifier nvarchar(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    with emp as (
                        select e.* from AspNetUsers e where e.EmployeeIdentifier = @EmployeeIdentifier
                        union all 
                        select p.* from AspNetUsers p 
					    inner join emp e on p.EmployeeIdentifier = e.ManagerIdentifier 
                    )
                    
                    select * from emp;
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop procedure sp_GetAllManagerTree");
        }
    }
}
