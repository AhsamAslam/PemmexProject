using Dapper;
using Holidays.API.Database.Entities;
using Holidays.API.Database.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Database.Repositories.Repository
{
    public class CommonHolidayRepository: ICommonHoliday
    {
        #region
        private IDbConnection db;
        #endregion

        public CommonHolidayRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }

        public async Task<int> CountPublicHolidays(DateTime start, DateTime end, string CountryCode)
        {

            var sql = "Select * from HolidayCalendars where CountryCode = @CountryCode and cast([Date] as date) >= @start and cast([Date] as date) <= @end";
            var holidays = await db.QueryAsync<HolidayCalendar>(sql, new { @CountryCode = CountryCode, @start = start.Date, @end = end.Date }).ConfigureAwait(false);
            //var holidays = await _context.HolidayCalendars
            //    .Where(h => h.CountryCode == CountryCode)
            //    .Where(h => h.Date.Date >= start.Date && h.Date.Date <= end.Date).ToListAsync();

            int count = 0;
            foreach (var h in holidays)
            {
                if (!Isweekend(h.Date.DayOfWeek))
                {
                    count++;
                }
            }
            return count;
        }

        public async Task<bool> IsPublicHoliday(DateTime date)
        {
            var sql = "Select top(1) * from HolidayCalendars where cast([Date] as date) <= @date";
            var h = await db.QueryAsync<HolidayCalendar>(sql, new { @date = date.Date }).ConfigureAwait(false);
            //var h = await _context.HolidayCalendars.FirstOrDefaultAsync(d => d.Date == date);
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
    }
}
