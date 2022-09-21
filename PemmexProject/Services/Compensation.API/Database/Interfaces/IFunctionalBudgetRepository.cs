using Compensation.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Interfaces
{
    public interface IFunctionalBudgetRepository
    {
        public Task<int> Save(FunctionalBudget functionalBudget);
        public Task<IEnumerable<FunctionalBudget>> Get(string[] managerIdentifier);
        public Task<IEnumerable<FunctionalBudget>> GetAll(string organizationIdentifier);
        Task<IEnumerable<OrganizationBudget>> GetOrganizationBudget(string organizationIdentifier);
    }
}
