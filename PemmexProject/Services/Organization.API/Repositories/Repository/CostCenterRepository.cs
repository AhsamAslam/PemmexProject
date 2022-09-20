using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class CostCenterRepository : ICostCenter
    {
        #region
        private IDbConnection db;
        #endregion
        public CostCenterRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("OrganizationConnection"));
        }
        public async Task<CostCenterRequest> AddCostCenterRequest(CostCenterRequest CostCenter)
        {
            try
            {
                var Sql = "INSERT INTO CostCenters(CostCenterIdentifier, CostCenterName, " +
                    "ParentCostCenterIdentifier, Created, CreatedBy) VALUES " +
                    "(@CostCenterIdentifier, @CostCenterName, @ParentCostCenterIdentifier, GETDATE(), 'test')";
                await db.ExecuteAsync(Sql, CostCenter).ConfigureAwait(false);
                return CostCenter;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> DeleteCostCenter(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CostCenter>> GetCostCenter()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CostCenter>> GetCostCenterByBusinessIdentifier(string BusinessIdentifier)
        {
            try
            {
                var Sql = "select * from CostCenters where businessIdentifier = @BusinessIdentifier";
                return await db.QueryAsync<CostCenter>(Sql, new { @BusinessIdentifier = BusinessIdentifier }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CostCenter> GetCostCenterByCostCenterIdentifier(string CostCenterIdentifier)
        {
            try
            {
                var Sql = "select * from CostCenters where CostCenterIdentifier = @CostCenterIdentifier";
                return await db.QueryFirstOrDefaultAsync<CostCenter>(Sql, new { @CostCenterIdentifier = CostCenterIdentifier }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CostCenter>> GetCostCentersTreeByCostCenterIdentifier(string CostCenterIdentifier)
        {
            try
            {
                return await db.QueryAsync<CostCenter>("sp_GetCostCentersTree",
                      this.SetParameter(CostCenterIdentifier),
                      commandType: CommandType.StoredProcedure);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<CostCenter> UpdateCostCenter(CostCenter CostCenter)
        {
            throw new NotImplementedException();
        }
        private DynamicParameters SetParameter(string CostCenterIdentifier)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@costCenterIdentifier", CostCenterIdentifier);
            return param;
        }
    }
}
