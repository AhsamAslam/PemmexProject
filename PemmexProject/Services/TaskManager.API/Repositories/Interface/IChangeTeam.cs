using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IChangeTeam
    {
        Task<IEnumerable<ChangeTeam>> GetChangeTeam();
        Task<IEnumerable<ChangeTeam>> GetChangeTeamById(int Id);
        Task<ChangeTeam> AddChangeTeam(ChangeTeam ChangeTeam);
        Task<ChangeTeam> UpdateChangeTeam(ChangeTeam ChangeTeam);
        Task<int> DeleteChangeTeam(int Id);
    }
}
