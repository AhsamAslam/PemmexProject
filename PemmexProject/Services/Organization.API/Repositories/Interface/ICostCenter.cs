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
        Task<IEnumerable<CostCenter>> GetCostCenterById(int Id);
        Task<CostCenter> AddCostCenter(CostCenter CostCenter);
        Task<CostCenter> UpdateCostCenter(CostCenter CostCenter);
        Task<int> DeleteCostCenter(int Id);
    }
}
