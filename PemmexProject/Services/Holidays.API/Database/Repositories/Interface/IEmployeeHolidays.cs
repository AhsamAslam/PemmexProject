using Holidays.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Interface
{
    public interface IEmployeeHolidays
    {
        Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidays();
        Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysById(int Id);
        Task<EmployeeHolidays> AddEmployeeHolidays(EmployeeHolidays EmployeeHolidays);
        Task<EmployeeHolidays> UpdateEmployeeHolidays(EmployeeHolidays EmployeeHolidays);
        Task<int> DeleteEmployeeHolidays(int Id);
    }
}
