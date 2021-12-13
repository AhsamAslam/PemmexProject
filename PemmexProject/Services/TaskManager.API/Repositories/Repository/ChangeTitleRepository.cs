using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class ChangeTitleRepository : IChangeTitle
    {
        public Task<ChangeTitle> AddChangeTitle(ChangeTitle ChangeTitle)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteChangeTitle(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeTitle>> GetChangeTitle()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeTitle>> GetChangeTitleById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ChangeTitle> UpdateChangeTitle(ChangeTitle ChangeTitle)
        {
            throw new NotImplementedException();
        }
    }
}
