using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IChangeManager
    {
        Task<IEnumerable<ChangeManager>> GetChangeManager();
        Task<IEnumerable<ChangeManager>> GetChangeManagerById(int Id);
        Task<ChangeManager> AddChangeManager(ChangeManager ChangeManager);
        Task<ChangeManager> UpdateChangeManager(ChangeManager ChangeManager);
        Task<int> DeleteChangeManager(int Id);
    }
}
