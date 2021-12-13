using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IOrganizationApprovalSettingDetail
    {
        Task<IEnumerable<OrganizationApprovalSettingDetail>> GetOrganizationApprovalSettingDetail();
        Task<IEnumerable<OrganizationApprovalSettingDetail>> GetOrganizationApprovalSettingDetailById(int Id);
        Task<OrganizationApprovalSettingDetail> AddOrganizationApprovalSettingDetail(OrganizationApprovalSettingDetail OrganizationApprovalSettingDetail);
        Task<OrganizationApprovalSettingDetail> UpdateOrganizationApprovalSettingDetail(OrganizationApprovalSettingDetail OrganizationApprovalSettingDetail);
        Task<int> DeleteOrganizationApprovalSettingDetail(int Id);
    }
}
