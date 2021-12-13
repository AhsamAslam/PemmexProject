using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class ChangeGradeRepository : IChangeGrade
    {
        public Task<ChangeGrade> AddChangeGrade(ChangeGrade ChangeGrade)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteChangeGrade(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeGrade>> GetChangeGrade()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeGrade>> GetChangeGradeById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ChangeGrade> UpdateChangeGrade(ChangeGrade ChangeGrade)
        {
            throw new NotImplementedException();
        }
    }
}
