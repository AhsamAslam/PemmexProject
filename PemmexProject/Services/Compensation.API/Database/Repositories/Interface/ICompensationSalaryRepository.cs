
using Compensation.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Interfaces
{
    public interface ICompensationSalaryRepository
    {
        Task AddCompensationSalary(CompensationSalaries CompensationSalaries);
        Task UpdateCompensationSalary(CompensationSalaries CompensationSalaries);
        Task<CompensationSalaries> GetCurrentSalary(string employeeIdentifier);
        Task<CompensationSalaries> GetCompensationSalariesByEmployeeIdentifier(string employeeIdentifier);
        Task<IEnumerable<Entities.Compensation>> GetCurrentSalaries(string employeeIdentifier);
        Task<Entities.Compensation> GetCurrentCompensation(string employeeIdentifier);
        Task<List<Entities.Compensation>> GetFunctionalSalaryCount(string[] employeeIdentifier);
        Task<IEnumerable<Entities.Compensation>> GetCompensationTotalAmountByOrganizationIdentifier(string organizationIdentifier);
        //Task<IEnumerable<Entities.Compensation>> GetCurrentBusinessCompensations(string businessIdentifier);
        Task<IEnumerable<Entities.Compensation>> GetCurrentJobFunctionCompensations(string businessIdentifer);

    }
}
