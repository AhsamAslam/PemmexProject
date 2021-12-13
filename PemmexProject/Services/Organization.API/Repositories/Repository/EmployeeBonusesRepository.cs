using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class EmployeeBonusesRepository : IEmployeeBonuses
    {
        public Task<EmployeeBonuses> AddEmployeeBonuses(EmployeeBonuses EmployeeBonuses)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEmployeeBonuses(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeBonuses>> GetEmployeeBonuses()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeBonuses>> GetEmployeeBonusesById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeBonuses> UpdateEmployeeBonuses(EmployeeBonuses EmployeeBonuses)
        {
            throw new NotImplementedException();
        }
    }
}
