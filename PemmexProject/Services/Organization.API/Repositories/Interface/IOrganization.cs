using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Interface
{
    public interface IOrganization
    {
        Task<IEnumerable<Organization.API.Entities.Organization>> GetOrganization();
        Task<IEnumerable<Organization.API.Entities.Organization>> GetOrganizationById(int Id);
        Task<Organization.API.Entities.Organization> AddOrganization(Organization.API.Entities.Organization Organization);
        Task<Organization.API.Entities.Organization> UpdateOrganization(Organization.API.Entities.Organization Organization);
        Task<int> DeleteOrganization(int Id);
    }
}
