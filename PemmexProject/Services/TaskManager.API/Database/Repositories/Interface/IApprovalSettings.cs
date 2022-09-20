using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Database.Repositories.Interface
{
    public interface IApprovalSettings
    {
        Task<IEnumerable<OrganizationApprovalSettings>> GetOrganizationApprovalSettingsByOrganizationIdentifier(string OrganizationIdentifier);
        Task<IEnumerable<OrganizationApprovalSettingDetail>> GetOrganizationApprovalSettingsDetailsByOrganizationApprovalSettingsId(int OrganizationApprovalSettingsId);
        Task DeleteOrganizationApprovalSettingsDetails(List<OrganizationApprovalSettingDetail> OrganizationApprovalSettingDetail);

        Task<OrganizationApprovalSettings> GetOrganizationApprovalSettingsByOrganizationIdentifierAndTaskType(string OrganizationIdentifier, TaskType TaskType);
        Task AddOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings);
        Task AddBaseTask(BaseTask BaseTask);
        Task<IEnumerable<BaseTask>> GetBaseTaskByRequestedByIdentifier(string RequestedByIdentifier, int currentTaskStatus);
        Task<List<BaseTask>> GetAllBaseTaskByTaskIdentifier(Guid[] TaskIdentifier, int currentTaskStatus);
        Task<IEnumerable<BaseTask>> GetBaseTaskAndByRequestedByIdentifierAndStatuses(string RequestedByIdentifier, int currentTaskStatus, int currentTaskStatus2);
        Task<List<BaseTask>> GetAllBaseTaskByTaskIdentifierAndStatuses(Guid[] TaskIdentifier, int currentTaskStatus, int currentTaskStatus2);

        Task UpdateOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings);

    }
}
