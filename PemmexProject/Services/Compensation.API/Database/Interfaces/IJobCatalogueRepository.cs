using Compensation.API.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compensation.API.Database.Interfaces
{
    public interface IJobCatalogueRepository
    {
        Task<IEnumerable<JobCatalogue>> GetOrganizationJobCatalogue(string organizationIdentifier);
    }
}
