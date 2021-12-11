using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Organization.API.Migrations
{
    public partial class sp_GetCostCentersTree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE [dbo].[sp_GetCostCentersTree]
                    @costCenterIdentifier nvarchar(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                   WITH CostCenterCTR AS
                    (
                        select c.* from CostCenters c where CostCenterIdentifier = @costCenterIdentifier
	                    union all 
	                    select cc.* from CostCenters cc inner join CostCenterCTR ct on ct.ParentCostCenterIdentifier = cc.CostCenterIdentifier
                    )


                    select * from CostCenterCTR;
                END";

            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("drop procedure sp_GetCostCentersTree");
        }
    }
}
