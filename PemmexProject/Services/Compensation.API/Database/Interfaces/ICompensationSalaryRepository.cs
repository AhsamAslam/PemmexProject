
using Compensation.API.Database.Entities;
using Compensation.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Interfaces
{
    public interface ICompensationSalaryRepository
    {
        Task<CompensationSalaries> GetCurrentSalary(string employeeIdentifier);
        Task<IEnumerable<CompensationSalaries>> GetTeamBonus(string[] employeeIdentifier);
        Task<IEnumerable<Entities.Compensation>> GetCurrentCompensation(string[] employeeIdentifiers);
        Task<IEnumerable<CompensationTotalMonthlyPayDto>> GetCurrentCompensation(string[] employeeIdentifiers, int totalMonth);
        //Task<IEnumerable<Entities.Compensation>> GetCurrentBusinessCompensations(string businessIdentifier);
        Task<IEnumerable<Entities.Compensation>> GetOrganizationCurrentCompensations(string organizationIdentifier);
        Task<IEnumerable<Entities.CompensationSalaries>> GetOrganizationCurrentSalary(string organizationIdentifier,DateTime startDate,DateTime endDate);
        Task<int> SaveSalary(Entities.CompensationSalaries compensationSalary);
        Task<int> SaveCompensation(Entities.Compensation compensation);
        Task<double> GetOrganizationTotalSalaryCountByDate(string organizationIdentifier, DateTime startDate, DateTime endDate);

    }
}
