using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class BusinessRepository : IBusiness
    {
        #region
        private IDbConnection db;
        #endregion
        public BusinessRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("OrganizationConnection"));
        }
        public Task<Business> AddBusiness(Business Business)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBusiness(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Business>> GetAllEmployeeTree(string Id)
        {
            try
            {
                var Sql = "Select * from Businesses b " +
                    "inner join Employees e on b.Id = e.BusinessId " +
                    "where ISNULL(b.IsActive, 1) = 1 AND " +
                    "(b.ParentBusinessId = @Id OR b.BusinessIdentifier = @Id)";
                return await db.QueryAsync<Business>(Sql, new { @ParentBusinessId = Id, @EmployeeIdentifier = Id }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<Business>> GetBusinessById(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Business>> GetBusinessByParentBusinessId(string ParentBusinessId)
        {
            try
            {
                var Sql = "Select * from Businesses b " +
                    "where ISNULL(b.IsActive, 1) = 1 AND b.ParentBusinessId = @ParentBusinessId";
                return await db.QueryAsync<Business>(Sql, new { @ParentBusinessId = ParentBusinessId }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Business> GetBusinessByParentBusinessIdIsActive(string ParentBusinessId)
        {
            try
            {
                var Sql = "Select top(1)* from Businesses b " +
                    "where ISNULL(b.IsActive, 1) = 1 AND b.ParentBusinessId = @ParentBusinessId";
                return await db.QueryFirstAsync<Business>(Sql, new { @ParentBusinessId = ParentBusinessId }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Business> UpdateBusiness(Business Business)
        {
            throw new NotImplementedException();
        }
    }
}
