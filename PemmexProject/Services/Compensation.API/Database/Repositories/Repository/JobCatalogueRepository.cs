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
    public class JobCatalogueRepository : IJobCatalogue
    {
        #region
        private IDbConnection db;
        #endregion
        public JobCatalogueRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
        }
        public async Task AddJobCatalogue(JobCatalogue JobCatalogue)
        {
            try
            {
                var Sql = "INSERT INTO JobCatalogues (grade,minimum_salary,median_salary," +
                    "maximum_salary,annual_bonus,country,currency,jobFunction,acv_bonus_percentage," +
                    "organizationIdentifier,Created,CreatedBy) " +
                    "VALUES (@grade,@minimum_salary,@median_salary,@maximum_salary," +
                    "@annual_bonus,@country,@currency,@jobFunction,@acv_bonus_percentage," +
                    "@organizationIdentifier,GETDATE(),'Test')";
                
                await db.ExecuteAsync(Sql, JobCatalogue).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<JobCatalogue> GetJobCatalogueByOrganizationIdentifierAndJobFunctionAndGrade(string OrganizationIdentifier, string JobFunction, string Grade)
        {
            try
            {
                var Sql = "Select * from JobCatalogues where jobFunction = @JobFunction and " +
                    "organizationIdentifier = @OrganizationIdentifier and grade = @Grade";
                return await db.QueryFirstAsync<JobCatalogue>(Sql).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
