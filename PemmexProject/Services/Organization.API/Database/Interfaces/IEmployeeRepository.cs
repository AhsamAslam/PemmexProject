using Organization.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Database.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees(string[] employeeIdentifiers);
        Task<IEnumerable<Employee>> GetSiblings(string employeeIdentifier);
        Task<IEnumerable<Employee>> GetTeamMembers(string employeeIdentifier);
        Task<IEnumerable<Employee>> GetBusinessUnitsHeads(string organizationIdentifier);
        Task<IEnumerable<Employee>> GetTeamMembersHierarchy(string employeeIdentifier);
    }
}
