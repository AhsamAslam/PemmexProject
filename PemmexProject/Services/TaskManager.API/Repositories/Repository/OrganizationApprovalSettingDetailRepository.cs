using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class OrganizationApprovalSettingDetailRepository : IOrganizationApprovalSettingDetail
    {
        public Task<OrganizationApprovalSettingDetail> AddOrganizationApprovalSettingDetail(OrganizationApprovalSettingDetail OrganizationApprovalSettingDetail)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteOrganizationApprovalSettingDetail(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationApprovalSettingDetail>> GetOrganizationApprovalSettingDetail()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrganizationApprovalSettingDetail>> GetOrganizationApprovalSettingDetailById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationApprovalSettingDetail> UpdateOrganizationApprovalSettingDetail(OrganizationApprovalSettingDetail OrganizationApprovalSettingDetail)
        {
            throw new NotImplementedException();
        }
    }
}
