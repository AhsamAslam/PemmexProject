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
