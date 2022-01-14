using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Database.Repositories.Interface;

namespace TaskManager.API.Database.Repositories.Repository
{
    public class ApprovalSettingsRepository : IApprovalSettings
    {
        #region
        private IDbConnection db;
        #endregion
        public ApprovalSettingsRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("TaskManagerConnection"));
        }

        public async Task AddOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings)
        {
            try
            {
                var Sql = "INSERT INTO organizationApprovalSettings " +
                    "(OrganizationIdentifier,taskType,Created,CreatedBy) " +
                    "VALUES (@OrganizationIdentifier,@taskType,GETDATE(),'Test')";
                await db.ExecuteAsync(Sql, OrganizationApprovalSettings).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteOrganizationApprovalSettingsDetails(List<OrganizationApprovalSettingDetail> OrganizationApprovalSettingDetail)
        {
            try
            {
                foreach (var item in OrganizationApprovalSettingDetail)
                {
                    var Sql = "Delete from organizationApprovalSettingDetails where Id = @Id";
                    await db.ExecuteAsync(Sql, new { @Id = item.Id });
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrganizationApprovalSettings>> GetOrganizationApprovalSettingsByOrganizationIdentifier(string OrganizationIdentifier)
        {
            try
            {
                var Sql = "Select * from organizationApprovalSettings o " +
                    "inner join organizationApprovalSettingDetails od " +
                    "on o.Id = od.OrganizationApprovalSettingsId " +
                    "where OrganizationIdentifier = @OrganizationIdentifier";
                return await db.QueryAsync<OrganizationApprovalSettings>(Sql, new { @OrganizationIdentifier = OrganizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<OrganizationApprovalSettings> GetOrganizationApprovalSettingsByOrganizationIdentifierAndTaskType(string OrganizationIdentifier, TaskType TaskType)
        {
            try
            {
                var Sql = "Select * from organizationApprovalSettings o " +
                    "inner join organizationApprovalSettingDetails od " +
                    "on o.Id = od.OrganizationApprovalSettingsId " +
                    "where OrganizationIdentifier = @OrganizationIdentifier and taskType = @TaskType";
                return await db.QueryFirstAsync<OrganizationApprovalSettings>(Sql, new { @OrganizationIdentifier = OrganizationIdentifier, @TaskType = TaskType }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrganizationApprovalSettingDetail>> GetOrganizationApprovalSettingsDetailsByOrganizationApprovalSettingsId(int OrganizationApprovalSettingsId)
        {
            try
            {
                var Sql = "select * from organizationApprovalSettingDetails " +
                    "where OrganizationApprovalSettingsId = @OrganizationApprovalSettingsId";
                return await db.QueryAsync<OrganizationApprovalSettingDetail>(Sql, new { @OrganizationApprovalSettingsId = OrganizationApprovalSettingsId }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings)
        {
            try
            {
                var Sql = "UPDATE organizationApprovalSettings SET OrganizationIdentifier = @OrganizationIdentifier,taskType = @taskType,LastModified = GETDATE(),LastModifiedBy = 'test' WHERE Id = @Id";

                await db.ExecuteAsync(Sql, OrganizationApprovalSettings).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
