
using Compensation.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Interfaces
{
    public interface ICompensationSalaryRepository
    {
        Task<CompensationSalaries> GetCurrentSalary(string employeeIdentifier);
        Task<Entities.Compensation> GetCurrentCompensation(string employeeIdentifier);
        //Task<IEnumerable<Entities.Compensation>> GetCurrentBusinessCompensations(string businessIdentifier);
        Task<IEnumerable<Entities.Compensation>> GetCurrentJobFunctionCompensations(string businessIdentifer);

    }
}
