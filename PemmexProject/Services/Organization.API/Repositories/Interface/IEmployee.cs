using Organization.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetEmployee();
        Task<IEnumerable<Employee>> GetEmployeeById(int Id);
        Task<Employee> AddEmployee(Employee Employee);
        Task<Employee> UpdateEmployee(Employee Employee);
        Task<int> DeleteEmployee(int Id);
    }
}
