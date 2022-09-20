using Compensation.API.Database.Entities;
using Compensation.API.Database.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories
{
    public class JobCatalogueRepository: IJobCatalogueRepository
    {
        #region
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;

        #endregion
        public JobCatalogueRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value);
        }

        public async Task<IEnumerable<JobCatalogue>> GetOrganizationJobCatalogue(string organizationIdentifier)
        {
            try
            {
                var sql = @"Select * from JobCatalogues with (nolock) where organizationIdentifier = @organizationIdentifier";
                return await db.QueryAsync<JobCatalogue>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
