using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class ChangeCompensationRepository : IChangeCompensation
    {
        public Task<ChangeCompensation> AddChangeCompensation(ChangeCompensation ChangeCompensation)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteChangeCompensation(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeCompensation>> GetChangeCompensation()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeCompensation>> GetChangeCompensationById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ChangeCompensation> UpdateChangeCompensation(ChangeCompensation ChangeCompensation)
        {
            throw new NotImplementedException();
        }
    }
}
