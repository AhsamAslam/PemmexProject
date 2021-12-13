using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IChangeGrade
    {
        Task<IEnumerable<ChangeGrade>> GetChangeGrade();
        Task<IEnumerable<ChangeGrade>> GetChangeGradeById(int Id);
        Task<ChangeGrade> AddChangeGrade(ChangeGrade ChangeGrade);
        Task<ChangeGrade> UpdateChangeGrade(ChangeGrade ChangeGrade);
        Task<int> DeleteChangeGrade(int Id);
    }
}
