using Organization.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Repositories.Interface
{
    public interface IEmployee
    {
        Task<IEnumerable<Employee>> GetEmployeeByEmployeeIdentifier(string[] EmployeeIdentifier);
        Task<IEnumerable<Employee>> GetEmployeeCostCenterByEmployeeIdentifier(string[] EmployeeIdentifier);
        Task<Employee> GetEmployeeByEmployeeIdentifier(string EmployeeIdentifier);
        Task<IEnumerable<string>> GetEmployeeManagerIdentifierByParentBusinessId(string ParentBusinessId);
        Task<IEnumerable<Employee>> GetEmployeeByCostCenterIdentifier(string CostCenterIdentifier);
        Task<IEnumerable<Employee>> GetEmployeeAndBusinessAndCostCenterByParentBusinessId(string ParentBusinessId);
        Task<Employee> GetEmployeeByEmpGuid(Guid EmpGuid);
        Task<Employee> GetEmployeeByGuidId(Guid EmpGuid);
        Task<IEnumerable<Employee>> GetEmployeeByBusinessIdentifier(string BusinessIdentifier);
        Task<IEnumerable<Employee>> GetEmployeeById(int Id); 
        Task<IEnumerable<Employee>> GetEmployeeTreeForManager(string EmployeeIdentifier);
        Task<Employee> AddEmployee(Employee Employee);
        Task<Employee> UpdateEmployee(Employee Employee);
        Task<int> DeleteEmployee(int Id);
    }
}
