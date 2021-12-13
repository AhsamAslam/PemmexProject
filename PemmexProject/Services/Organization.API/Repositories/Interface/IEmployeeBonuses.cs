using Organization.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Interface
{
    public interface IEmployeeBonuses
    {
        Task<IEnumerable<EmployeeBonuses>> GetEmployeeBonuses();
        Task<IEnumerable<EmployeeBonuses>> GetEmployeeBonusesById(int Id);
        Task<EmployeeBonuses> AddEmployeeBonuses(EmployeeBonuses EmployeeBonuses);
        Task<EmployeeBonuses> UpdateEmployeeBonuses(EmployeeBonuses EmployeeBonuses);
        Task<int> DeleteEmployeeBonuses(int Id);
    }
}
