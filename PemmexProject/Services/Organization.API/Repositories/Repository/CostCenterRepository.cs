using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class CostCenterRepository : ICostCenter
    {
        public Task<CostCenter> AddCostCenter(CostCenter CostCenter)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteCostCenter(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CostCenter>> GetCostCenter()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CostCenter>> GetCostCenterById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CostCenter> UpdateCostCenter(CostCenter CostCenter)
        {
            throw new NotImplementedException();
        }
    }
}
