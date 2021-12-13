using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class OrganizationApprovalSettingsRepository : IOrganizationApprovalSettings
    {
        public Task<OrganizationApprovalSettings> AddOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteOrganizationApprovalSettings(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationApprovalSettings>> GetOrganizationApprovalSettings()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationApprovalSettings>> GetOrganizationApprovalSettingsById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationApprovalSettings> UpdateOrganizationApprovalSettings(OrganizationApprovalSettings OrganizationApprovalSettings)
        {
            throw new NotImplementedException();
        }
    }
}
