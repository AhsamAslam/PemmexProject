using Compensation.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories.Interface
{
    public interface IBudget
    {
        Task<IEnumerable<OrganizationBudget>> GetOrganizationBudgetByOrganizationIdentifier(string OrganizationIdentifier);
        Task DeleteOrganizationBudget(List<OrganizationBudget> OrganizationBudget);
        Task AddOrganizationBudget(OrganizationBudget OrganizationBudget);
    }
}
