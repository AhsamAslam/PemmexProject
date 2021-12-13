using Organization.API.Entities;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Repository
{
    public class EmployeeRepository : IEmployee
    {
        public Task<Employee> AddEmployee(Employee Employee)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEmployee(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployee()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetEmployeeById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> UpdateEmployee(Employee Employee)
        {
            throw new NotImplementedException();
        }
    }
}
