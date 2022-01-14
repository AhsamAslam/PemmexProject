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
        Task UpdateOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings);

    }
}
