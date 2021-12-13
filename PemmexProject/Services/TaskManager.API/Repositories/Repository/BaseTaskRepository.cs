using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class BaseTaskRepository : IBaseTask
    {
        public Task<BaseTask> AddBaseTask(BaseTask BaseTask)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteBaseTask(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseTask>> GetBaseTask()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BaseTask>> GetBaseTaskById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseTask> UpdateBaseTask(BaseTask BaseTask)
        {
            throw new NotImplementedException();
        }
    }
}
