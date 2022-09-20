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
        Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId);
        Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdHolidayTypeStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId, int type);
        Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdHolidayStatusStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId, int Planned, int Approved, int Availed);

        Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdHolidayTypeHolidayStatusStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId, int type, int status);
        Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysById(int Id);
        Task<EmployeeHolidays> AddEmployeeHolidays(EmployeeHolidays EmployeeHolidays);
        Task<EmployeeHolidays> UpdateEmployeeHolidays(EmployeeHolidays EmployeeHolidays);
        Task<int> DeleteEmployeeHolidays(int Id);
    }
}
