using Holidays.API.Database.Entities;
using Holidays.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Repository
{
    public class EmployeeHolidaysRepository : IEmployeeHolidays
    {
        public Task<EmployeeHolidays> AddEmployeeHolidays(EmployeeHolidays EmployeeHolidays)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteEmployeeHolidays(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidays()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<EmployeeHolidays> UpdateEmployeeHolidays(EmployeeHolidays EmployeeHolidays)
        {
            throw new NotImplementedException();
        }
    }
}
