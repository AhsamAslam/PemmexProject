using Holidays.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Interface
{
    public interface ICompanyToEmployeeHolidays
    {
        Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidays();
        Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidaysById(int Id);
        Task<CompanyToEmployeeHolidays> AddCompanyToEmployeeHolidays(CompanyToEmployeeHolidays CompanyToEmployeeHolidays);
        Task<CompanyToEmployeeHolidays> UpdateCompanyToEmployeeHolidays(CompanyToEmployeeHolidays CompanyToEmployeeHolidays);
        Task<int> DeleteCompanyToEmployeeHolidays(int Id);
    }
}
