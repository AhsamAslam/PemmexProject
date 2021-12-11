using PemmexAPIAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface IOrganizationService
    {
        Task<IEnumerable<Employee>> GetSuboridnates(string EmployeeIdentifier);
        Task<IEnumerable<Employee>> GetTeamMembers(string costcenterIdentifier);
        Task<IEnumerable<Employee>> GetBusinessEmployees(string businessIdentifier);
        Task<IEnumerable<Employee>> GetOrganizationEmployees(string organizationId);
        Task<IEnumerable<Business>> GetBusinesses(string organizationId);
    }
}
