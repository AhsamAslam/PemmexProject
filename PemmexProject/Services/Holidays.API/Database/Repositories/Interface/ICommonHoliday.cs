using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Database.Repositories.Interface
{
    public interface ICommonHoliday
    {
        bool Isweekend(DayOfWeek day);
        Task<bool> IsPublicHoliday(DateTime date);
        Task<int> CountPublicHolidays(DateTime start, DateTime end, string CountrCode);
        int GetBusinessDays(DateTime startD, DateTime endD);
        string GetCountryCodeByName(string countryName);
    }
}
