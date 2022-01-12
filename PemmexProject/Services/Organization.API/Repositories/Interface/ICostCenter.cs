using Organization.API.Dtos;
using Organization.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Interface
{
    public interface ICostCenter
    {
        Task<IEnumerable<CostCenter>> GetCostCenter(); 
         Task<CostCenter> GetCostCenterByCostCenterIdentifier(string CostCenterIdentifier);
        Task<IEnumerable<CostCenter>> GetCostCenterByBusinessIdentifier(string BusinessIdentifier);
        Task<IEnumerable<CostCenter>> GetCostCentersTreeByCostCenterIdentifier(string CostCenterIdentifier);
        Task<CostCenterRequest> AddCostCenterRequest(CostCenterRequest CostCenter);
        Task<CostCenter> UpdateCostCenter(CostCenter CostCenter);
        Task<int> DeleteCostCenter(int Id);
    }
}
