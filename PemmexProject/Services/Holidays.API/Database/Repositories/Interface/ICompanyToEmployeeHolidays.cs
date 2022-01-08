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
        Task<CompanyToEmployeeHolidays> GetCompanyToEmployeeHolidaysByEmployeeIdAndHolidaySettingsIdentitfier(Guid EmployeeId, Guid HolidaySettingsIdentitfier);
        Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidaysByCostcenterIdentifierAndHolidaySettingsIdentitfier(string CostcenterIdentifier, Guid HolidaySettingsIdentitfier);
        Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidaysByHolidaySettingsIdentitfier(Guid HolidaySettingsIdentitfier);

        Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidaysById(int Id);
        Task<CompanyToEmployeeHolidays> AddCompanyToEmployeeHolidays(CompanyToEmployeeHolidays CompanyToEmployeeHolidays);
        Task<CompanyToEmployeeHolidays> UpdateCompanyToEmployeeHolidays(CompanyToEmployeeHolidays CompanyToEmployeeHolidays);
        Task<int> DeleteCompanyToEmployeeHolidays(int Id);
    }
}
