using Holidays.API.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holidays.API.Database.Interfaces
{
    public interface IHolidayReportRepository
    {
        public Task<dynamic> GetEmployeeWithSickLeaves(string organizationIdentifier,int month,int days,string[] employeeIdentifiers);
        public Task<IEnumerable<EmployeeHolidays>> GetEmployeeWithMaternityLeaves(string organizationIdentifier, int month,int type,string[] employeeIdentifiers);
    }
}
