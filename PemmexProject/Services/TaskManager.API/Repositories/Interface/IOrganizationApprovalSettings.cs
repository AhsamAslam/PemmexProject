using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IOrganizationApprovalSettings
    {
        Task<IEnumerable<OrganizationApprovalSettings>> GetOrganizationApprovalSettings();
        Task<IEnumerable<OrganizationApprovalSettings>> GetOrganizationApprovalSettingsById(int Id);
        Task<OrganizationApprovalSettings> AddOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings);
        Task<OrganizationApprovalSettings> UpdateOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings);
        Task<int> DeleteOrganizationApprovalSettings(int Id);
    }
}
