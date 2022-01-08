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
    public class EmployeeHolidaysRepository : IEmployeeHolidays
    {
        #region
        private IDbConnection db;
        #endregion
        public EmployeeHolidaysRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }
        public async Task<EmployeeHolidays> AddEmployeeHolidays(EmployeeHolidays EmployeeHolidays)
        {
            try
            {
                var sql = "INSERT INTO EmployeeHolidays" +
                    " (EmployeeId,EmployeeIdentifier,costcenterIdentifier," +
                    "SubsituteId,SubsituteIdentifier,HolidayStartDate," +
                    "HolidayEndDate,HolidayStatus,holidayType," +
                    "Description,Created,CreatedBy) " +
                    "VALUES(@EmployeeId,@EmployeeIdentifier,@costcenterIdentifier," +
                    "@SubsituteId,@SubsituteIdentifier,@HolidayStartDate," +
                    "@HolidayEndDate,@HolidayStatus,@holidayType,@Description," +
                    "GetDate(),'test')"
                    +"Select CAST(SCOPE_IDENTITY() as int);";
                await db.ExecuteAsync(sql, EmployeeHolidays).ConfigureAwait(false);
                return EmployeeHolidays;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> DeleteEmployeeHolidays(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidays()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdHolidayStatusStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId, int Planned, int Approved, int Availed)
        {
            try
            {
                var sql = "select * from EmployeeHolidays where (HolidayStartDate Between @start and @end) And (HolidayEndDate Between @start and @end)  And EmployeeId = @EmployeeId And (HolidayStatus = @Planned or HolidayStatus = @Approved or HolidayStatus = @Availed)";
                return await db.QueryAsync<EmployeeHolidays>(sql, new { @start = start, @end = end, @EmployeeId = EmployeeId, @Planned = Planned, @Approved = Approved, @Availed = Availed }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdHolidayTypeHolidayStatusStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId, int type, int status)
        {
            try
            {
                var sql = "select * from EmployeeHolidays where (HolidayStartDate Between @start and @end) And (HolidayEndDate Between @start and @end)  And EmployeeId = @EmployeeId And holidayType = @type And HolidayStatus = @status";
                return await db.QueryAsync<EmployeeHolidays>(sql, new { @start = start, @end = end, @EmployeeId = EmployeeId, @type = type, @status = status }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdHolidayTypeStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId, int type)
        {
            try
            {
                var sql = "select * from EmployeeHolidays where (HolidayStartDate Between @start and @end) And (HolidayEndDate Between @start and @end)  And EmployeeId = @EmployeeId And holidayType = @type";
                return await db.QueryAsync<EmployeeHolidays>(sql, new { @start = start, @end = end, @EmployeeId = EmployeeId, @type = type }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<EmployeeHolidays>> GetEmployeeHolidaysByEmployeeIdStartAndEndDate(DateTime start, DateTime end, Guid EmployeeId)
        {
            try
            {
                var sql = "select * from EmployeeHolidays where (HolidayStartDate Between @start and @end) And (HolidayEndDate Between @start and @end)  And EmployeeId = @EmployeeId";
                return await db.QueryAsync<EmployeeHolidays>(sql, new { @start = start, @end = end, @EmployeeId = EmployeeId }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
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
