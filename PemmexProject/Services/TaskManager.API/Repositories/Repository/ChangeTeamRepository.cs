using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class ChangeTeamRepository : IChangeTeam
    {
        public Task<ChangeTeam> AddChangeTeam(ChangeTeam ChangeTeam)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteChangeTeam(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeTeam>> GetChangeTeam()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeTeam>> GetChangeTeamById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ChangeTeam> UpdateChangeTeam(ChangeTeam ChangeTeam)
        {
            throw new NotImplementedException();
        }
    }
}
