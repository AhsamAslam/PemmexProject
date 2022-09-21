using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Common
{
    public class CommonHolidayDAL : ICommonHolidayDAL
    {
        private readonly IApplicationDbContext _context;

        public CommonHolidayDAL(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountPublicHolidays(DateTime start, DateTime end,string CountryCode)
        {
            var holidays = await _context.HolidayCalendars
                .Where(h => h.CountryCode == CountryCode)
                .Where(h => h.Date.Date >= start.Date && h.Date.Date <= end.Date).ToListAsync();

            int count = 0;
            foreach(var h in holidays)
            {
                if(!Isweekend(h.Date.DayOfWeek))
                {
                    count++;
                }
            }
            return count;
        }

        public async Task<bool> IsPublicHoliday(DateTime date)
        {
            var h = await _context.HolidayCalendars.FirstOrDefaultAsync(d => d.Date == date);
            return (h != null);
        }
        public int GetBusinessDays(DateTime startD, DateTime endD)
        {
            double calcBusinessDays =
                1 + ((endD - startD).TotalDays * 5 -
                (startD.DayOfWeek - endD.DayOfWeek) * 2) / 7;

            if (endD.DayOfWeek == DayOfWeek.Saturday) calcBusinessDays--;
            if (startD.DayOfWeek == DayOfWeek.Sunday) calcBusinessDays--;

            return Convert.ToInt32(calcBusinessDays);
        }
        public bool Isweekend(DayOfWeek day)
        {
            return ((day == DayOfWeek.Saturday) || (day == DayOfWeek.Sunday));
        }

        public string GetCountryCodeByName(string countryName)
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(countryName));
            return englishRegion.TwoLetterISORegionName;
        }

        public async Task<List<HolidayCalendar>> GetHolidayCalendar(string CountryCode,int year)
        {
            return  await _context.HolidayCalendars
                .Where(h => h.CountryCode == CountryCode)
                .Where(h => h.Date.Year == year).ToListAsync();
        }
    }
}
