using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class OrganizationRepository : IOrganization
    {
        public Task<Entities.Organization> AddOrganization(Entities.Organization Organization)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteOrganization(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entities.Organization>> GetOrganization()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entities.Organization>> GetOrganizationById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.Organization> UpdateOrganization(Entities.Organization Organization)
        {
            throw new NotImplementedException();
        }
    }
}
