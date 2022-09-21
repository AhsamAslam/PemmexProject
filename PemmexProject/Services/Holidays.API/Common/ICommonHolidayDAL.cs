using Holidays.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Common
{
    public interface ICommonHolidayDAL
    {
        bool Isweekend(DayOfWeek day);
        Task<bool> IsPublicHoliday(DateTime date);
        Task<int> CountPublicHolidays(DateTime start, DateTime end,string CountrCode);
        int GetBusinessDays(DateTime startD, DateTime endD);
        string GetCountryCodeByName(string countryName);
        Task<List<HolidayCalendar>> GetHolidayCalendar(string CountrCode,int year);

    }
}
