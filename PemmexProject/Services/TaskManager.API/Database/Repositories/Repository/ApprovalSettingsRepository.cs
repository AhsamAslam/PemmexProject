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

        public async Task AddBaseTask(BaseTask BaseTask)
        {
            try
            {
                var Sql = "INSERT INTO BaseTasks (TaskIdentifier,RequestedByIdentifier," +
                    "ManagerIdentifier,appliedStatus,currentTaskStatus,taskType," +
                    "isActive,taskDescription,Created,CreatedBy) VALUES " +
                    "(@TaskIdentifier,@RequestedByIdentifier,@ManagerIdentifier," +
                    "@appliedStatus,@currentTaskStatus,@taskType,@isActive,@taskDescription," +
                    "@Created,@CreatedBy)";
                await db.ExecuteAsync(Sql, BaseTask).ConfigureAwait(false);

                var SqlHoliday = "INSERT INTO dbo.ChangeHolidays " +
                    "(HolidayTaskId,EmployeeIdentifier,SubsituteId,SubsituteIdentifier," +
                    "HolidayStartDate,HolidayEndDate,holidayType,FileName) VALUES " +
                    "(@HolidayTaskId,@EmployeeIdentifier,@SubsituteId,@SubsituteIdentifier," +
                    "@HolidayStartDate,@HolidayEndDate,@holidayType,@FileName)";
                await db.ExecuteAsync(SqlHoliday, BaseTask).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
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

        public async Task<List<BaseTask>> GetAllBaseTaskByTaskIdentifier(Guid[] TaskIdentifier, int currentTaskStatus)
        {
            try
            {
                List<BaseTask> baseTasks = new List<BaseTask>();
                foreach (var item in TaskIdentifier)
                {
                    var Sql = "Select * from BaseTasks bt with (nolock) " +
                        "inner join ChangeHolidays ch with (nolock) on " +
                        "bt.TaskId = ch.HolidayTaskId inner join ChangeTitles ct with (nolock) on " +
                        "bt.TaskId = ct.TitleTaskId inner join ChangeManagers cm with (nolock) " +
                        "on bt.TaskId = cm.ManagerTaskId inner join ChangeCompensations cc with (nolock) " +
                        "on bt.TaskId = cc.CompensationTaskId inner join ChangeGrades cg with (nolock) " +
                        "on bt.TaskId = cg.GradeTaskId inner join ChangeTeam cht with (nolock) " +
                        "on bt.TaskId = cht.TeamTaskId inner join BonusTask bot with (nolock) " +
                        "on bt.TaskId = bot.BonusTaskId  where bt.TaskIdentifier = @TaskIdentifier " +
                        "and bt.currentTaskStatus = @currentTaskStatus";
                    baseTasks.Add(await db.QueryFirstAsync<BaseTask>(Sql, new { @TaskIdentifier = item, @currentTaskStatus = currentTaskStatus }).ConfigureAwait(false));
                }
                return baseTasks;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<BaseTask>> GetAllBaseTaskByTaskIdentifierAndStatuses(Guid[] TaskIdentifier, int currentTaskStatus, int currentTaskStatus2)
        {
            try
            {
                List<BaseTask> baseTasks = new List<BaseTask>();
                foreach (var item in TaskIdentifier)
                {
                    var Sql = "Select * from BaseTasks bt with (nolock) " +
                        "inner join ChangeHolidays ch with (nolock) on " +
                        "bt.TaskId = ch.HolidayTaskId inner join ChangeTitles ct with (nolock) on " +
                        "bt.TaskId = ct.TitleTaskId inner join ChangeManagers cm with (nolock) " +
                        "on bt.TaskId = cm.ManagerTaskId inner join ChangeCompensations cc with (nolock) " +
                        "on bt.TaskId = cc.CompensationTaskId inner join ChangeGrades cg with (nolock) " +
                        "on bt.TaskId = cg.GradeTaskId inner join ChangeTeam cht with (nolock) " +
                        "on bt.TaskId = cht.TeamTaskId inner join BonusTask bot with (nolock) " +
                        "on bt.TaskId = bot.BonusTaskId  where bt.TaskIdentifier = @TaskIdentifier " +
                        "and (bt.currentTaskStatus = @currentTaskStatus or bt.currentTaskStatus = @currentTaskStatus2)";
                    baseTasks.Add(await db.QueryFirstAsync<BaseTask>(Sql, new { @TaskIdentifier = item, @currentTaskStatus = currentTaskStatus, @currentTaskStatus2 = currentTaskStatus2 }).ConfigureAwait(false));
                }
                return baseTasks;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BaseTask>> GetBaseTaskAndByRequestedByIdentifierAndStatuses(string RequestedByIdentifier, int currentTaskStatus, int currentTaskStatus2)
        {
            try
            {
                var Sql = "Select distinct TaskIdentifier from BaseTasks with (nolock) " +
                    "where RequestedByIdentifier = @RequestedByIdentifier and " +
                    "isnull(isActive,1)=1 and (currentTaskStatus = @currentTaskStatus or currentTaskStatus = @currentTaskStatus2)";
                return await db.QueryAsync<BaseTask>(Sql, new { @RequestedByIdentifier = RequestedByIdentifier, @currentTaskStatus = currentTaskStatus, @currentTaskStatus2 = currentTaskStatus2 }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BaseTask>> GetBaseTaskByRequestedByIdentifier(string RequestedByIdentifier, int currentTaskStatus)
        {
            try
            {
                var Sql = "Select distinct TaskIdentifier from BaseTasks with (nolock) " +
                    "where RequestedByIdentifier = @RequestedByIdentifier and " +
                    "isnull(isActive,1)=1 and (currentTaskStatus = @currentTaskStatus)";
                return await db.QueryAsync<BaseTask>(Sql, new { @RequestedByIdentifier = RequestedByIdentifier, @currentTaskStatus = currentTaskStatus }).ConfigureAwait(false);
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
