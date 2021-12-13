using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class ChangeManagerRepository : IChangeManager
    {
        public Task<ChangeManager> AddChangeManager(ChangeManager ChangeManager)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteChangeManager(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeManager>> GetChangeManager()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeManager>> GetChangeManagerById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ChangeManager> UpdateChangeManager(ChangeManager ChangeManager)
        {
            throw new NotImplementedException();
        }
    }
}
