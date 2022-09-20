using Organization.API.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organization.API.Database.Interfaces
{
    public interface IOrganizationRepository
    {
        Task<IEnumerable<Employee>> GetOrganizationEmployees(string organizationIdentifier);

    }
}
