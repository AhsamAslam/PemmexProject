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
    public class HolidaySettingsRepository : IHolidaySettings
    {
        #region
        private IDbConnection db;
        #endregion
        public HolidaySettingsRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }
        public async Task<HolidaySettings> AddHolidaySetting(HolidaySettings HolidaySettings)
        {
            try
            {
                var Sql = "INSERT INTO HolidaySettings " +
                    "(HolidaySettingsIdentitfier,OrganizationIdentifier," +
                    "BusinessIdentifier,HolidayCalendarYear," +
                    "MaximumLimitHolidayToNextYear,Created,CreatedBy) " +
                    "VALUES (@HolidaySettingsIdentitfier,@OrganizationIdentifier," +
                    "@BusinessIdentifier,@HolidayCalendarYear," +
                    "@MaximumLimitHolidayToNextYear,GetDate(),'test')"
                    + "Select CAST(SCOPE_IDENTITY() as int);";
                await db.ExecuteAsync(Sql, HolidaySettings).ConfigureAwait(false);
                return HolidaySettings;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<HolidaySettings>> AddHolidaySettings(List<HolidaySettings> HolidaySettings)
        {
            try
            {
                foreach (var item in HolidaySettings)
                {
                    var Sql = "INSERT INTO HolidaySettings " +
                   "(HolidaySettingsIdentitfier,OrganizationIdentifier," +
                   "BusinessIdentifier,HolidayCalendarYear," +
                   "MaximumLimitHolidayToNextYear,Created,CreatedBy) " +
                   "VALUES (@HolidaySettingsIdentitfier,@OrganizationIdentifier," +
                   "@BusinessIdentifier,@HolidayCalendarYear," +
                   "@MaximumLimitHolidayToNextYear,GetDate(),'test')"
                   + "Select CAST(SCOPE_IDENTITY() as int);";
                    await db.ExecuteAsync(Sql, item).ConfigureAwait(false);
                }
               
                return HolidaySettings;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> DeleteHolidaySettings(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HolidaySettings>> GetHolidaySettings()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<HolidaySettings>> GetHolidaySettingsById(string Id)
        {
            try
            {
                var sql = "Select * from HolidaySettings where OrganizationIdentifier = @Id";
                return await db.QueryAsync<HolidaySettings>(sql, new { @Id = Id }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<HolidaySettings> UpdateHolidaySetting(HolidaySettings HolidaySettings)
        {
            try
            {
                var Sql = "UPDATE HolidaySettings SET " +
                    "HolidaySettingsIdentitfier = @HolidaySettingsIdentitfier," +
                    "OrganizationIdentifier = @OrganizationIdentifier," +
                    "BusinessIdentifier = @BusinessIdentifier," +
                    "HolidayCalendarYear = @HolidayCalendarYear," +
                    "MaximumLimitHolidayToNextYear = @MaximumLimitHolidayToNextYear," +
                    "LastModified = GetDate(),LastModifiedBy = 'test1' " +
                    "WHERE HolidaySettingsId = @HolidaySettingsId ";
                await db.ExecuteAsync(Sql, HolidaySettings).ConfigureAwait(false);
                return HolidaySettings;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<HolidaySettings>> UpdateHolidaySettings(List<HolidaySettings> HolidaySettings)
        {
            try
            {
                foreach (var item in HolidaySettings)
                {
                    var Sql = "UPDATE HolidaySettings SET " +
                     "HolidaySettingsIdentitfier = @HolidaySettingsIdentitfier," +
                     "OrganizationIdentifier = @OrganizationIdentifier," +
                     "BusinessIdentifier = @BusinessIdentifier," +
                     "HolidayCalendarYear = @HolidayCalendarYear," +
                     "MaximumLimitHolidayToNextYear = @MaximumLimitHolidayToNextYear," +
                     "LastModified = GetDate(),LastModifiedBy = 'test1' " +
                     "WHERE HolidaySettingsId = @HolidaySettingsId ";
                    await db.ExecuteAsync(Sql, HolidaySettings).ConfigureAwait(false);
                }

                return HolidaySettings;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
