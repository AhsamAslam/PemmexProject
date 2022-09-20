using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Organization.API.Database.Entities;
using Organization.API.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Organization.API.Database.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        #region
        private IDbConnection db;
        #endregion
        public OrganizationRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("OrganizationConnection"));
        }
        public async Task<IEnumerable<Employee>> GetOrganizationEmployees(string organizationIdentifier)
        {
            try
            {
                var sql = @"Select e.*,(m.FirstName + m.LastName) ManagerName,c.CostCenterIdentifier,c.CostCenterName,
                            b.BusinessIdentifier,b.BusinessName,b.OrganizationCountry from Employees e 
                            inner join CostCenters c on e.CostCenterId = c.CostCenterId
                            inner join Businesses b on b.Id = e.BusinessId
                            inner join Employees m on m.EmployeeIdentifier = e.ManagerIdentifier
                            and b.ParentBusinessId = @organizationIdentifier";
                return await db.QueryAsync<Employee>(sql,
                     new { @organizationIdentifier = organizationIdentifier })
                    .ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
