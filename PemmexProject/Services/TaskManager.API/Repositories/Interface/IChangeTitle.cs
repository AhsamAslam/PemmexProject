using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IChangeTitle
    {
        Task<IEnumerable<ChangeTitle>> GetChangeTitle();
        Task<IEnumerable<ChangeTitle>> GetChangeTitleById(int Id);
        Task<ChangeTitle> AddChangeTitle(ChangeTitle ChangeTitle);
        Task<ChangeTitle> UpdateChangeTitle(ChangeTitle ChangeTitle);
        Task<int> DeleteChangeTitle(int Id);
    }
}
