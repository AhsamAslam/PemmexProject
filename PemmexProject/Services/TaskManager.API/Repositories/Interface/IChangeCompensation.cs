using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IChangeCompensation
    {
        Task<IEnumerable<ChangeCompensation>> GetChangeCompensation();
        Task<IEnumerable<ChangeCompensation>> GetChangeCompensationById(int Id);
        Task<ChangeCompensation> AddChangeCompensation(ChangeCompensation ChangeCompensation);
        Task<ChangeCompensation> UpdateChangeCompensation(ChangeCompensation ChangeCompensation);
        Task<int> DeleteChangeCompensation(int Id);
    }
}
