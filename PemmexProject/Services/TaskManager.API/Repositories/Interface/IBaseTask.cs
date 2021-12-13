using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IBaseTask
    {
        Task<IEnumerable<BaseTask>> GetBaseTask();
        Task<IEnumerable<BaseTask>> GetBaseTaskById(int Id);
        Task<BaseTask> AddBaseTask(BaseTask BaseTask);
        Task<BaseTask> UpdateBaseTask(BaseTask BaseTask);
        Task<int> DeleteBaseTask(int Id);
    }
}
