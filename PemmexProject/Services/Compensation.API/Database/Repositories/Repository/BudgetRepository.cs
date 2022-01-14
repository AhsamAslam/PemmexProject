using Compensation.API.Database.Entities;
using Compensation.API.Database.Repositories.Interface;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories.Repository
{
    public class BudgetRepository : IBudget
    {
        #region
        private IDbConnection db;
        #endregion
        public BudgetRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
        }

        public async Task AddOrganizationBudget(OrganizationBudget OrganizationBudget)
        {
            try
            {
                var Sql = "INSERT INTO OrganizationBudgets (organizationIdentifier,startDate," +
                    "endDate,businessIdentifier,budgetPercentage,budgetValue," +
                    "TotalbudgetPercentage,TotalbudgetValue,jobFunction,Created,CreatedBy) " +
                    "VALUES (@organizationIdentifier,@startDate,@endDate,@businessIdentifier," +
                    "@budgetPercentage,@budgetValue,@TotalbudgetPercentage,@TotalbudgetValue," +
                    "@jobFunction,GetDate(),'Test')";
                await db.ExecuteAsync(Sql, OrganizationBudget).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteOrganizationBudget(List<OrganizationBudget> OrganizationBudget)
        {
            try
            {
                foreach (var item in OrganizationBudget)
                {
                    var Sql = "Delete from OrganizationBudgets where OrganizationBudgetId = @OrganizationBudgetId";
                    await db.ExecuteAsync(Sql, new { @OrganizationBudgetId = item.OrganizationBudgetId }).ConfigureAwait(false);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrganizationBudget>> GetOrganizationBudgetByOrganizationIdentifier(string OrganizationIdentifier)
        {
            try
            {
                var Sql = "Select * from OrganizationBudgets where " +
                    "organizationIdentifier = @OrganizationIdentifier and" +
                    " cast(startDate as date) = cast(@Date as date)";
                return await db.QueryAsync<OrganizationBudget>(Sql, new { @OrganizationIdentifier = OrganizationIdentifier }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
