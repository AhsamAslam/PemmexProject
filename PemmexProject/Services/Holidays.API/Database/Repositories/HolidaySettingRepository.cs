using Dapper;
using Holidays.API.Database.Entities;
using Holidays.API.Database.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Holidays.API.Database.Repositories
{
    public class HolidaySettingRepository : IHolidaySettingRepository
    {
        #region
        private IDbConnection db;
        #endregion
        public HolidaySettingRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }
        public async Task<IEnumerable<HolidaySettings>> GetOrganizationHolidaySettings(string organizationIdentifier)
        {
            try
            {
                var sql = @"SELECT hs.*
                            FROM HolidaySettings hs
                            INNER JOIN
                                (SELECT h.HolidaySettingsId, MAX(h.HolidayCalendarYear) AS MaxDateTime
                                FROM HolidaySettings h
                                GROUP BY h.HolidaySettingsId) groupedtt 
                            ON hs.HolidaySettingsId = groupedtt.HolidaySettingsId 
                            AND hs.HolidayCalendarYear = groupedtt.MaxDateTime
                            and hs.OrganizationIdentifier = @organizationIdentifier";
                return await db.QueryAsync<HolidaySettings>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
