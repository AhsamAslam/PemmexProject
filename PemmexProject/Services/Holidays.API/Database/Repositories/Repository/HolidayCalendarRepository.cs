using Dapper;
using Holidays.API.Database.Entities;
using Holidays.API.Repositories.Interface;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Repository
{
    public class HolidayCalendarRepository : IHolidayCalendar
    {
        #region
        private IDbConnection db;
        #endregion
        public HolidayCalendarRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }
        public async Task<IEnumerable<HolidayCalendar>> AddHolidayCalendar(List<HolidayCalendar> HolidayCalendar)
        {
            try
            {
                foreach (var item in HolidayCalendar)
                {
                    var Sql = "INSERT INTO HolidayCalendars (Date,CountryCode,LocalName," +
                        "Name,Global,Fixed,Type) VALUES " +
                        "(@Date,@CountryCode,@LocalName,@Name,@Global,@Fixed,@Type)" +
                         "Select CAST(SCOPE_IDENTITY() as int); ";
                    await db.ExecuteAsync(Sql, item).ConfigureAwait(false);
                }

                return HolidayCalendar;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> DeleteHolidayCalendar(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HolidayCalendar>> GetHolidayCalendar(string CountrCode, int year)
        {
            try
            {
                var sql = "Select * from HolidayCalendars where CountryCode = @CountryCode and YEAR(Date) = @Year";
                return await db.QueryAsync<HolidayCalendar>(sql, new { @CountryCode = CountrCode, @Year = year  }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<HolidayCalendar>> GetHolidayCalendarById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<HolidayCalendar> UpdateHolidayCalendar(HolidayCalendar HolidayCalendar)
        {
            throw new NotImplementedException();
        }
    }
}
