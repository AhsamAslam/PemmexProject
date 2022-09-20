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
    public class HolidayReportRepository : IHolidayReportRepository
    {
        #region
        private IDbConnection db;
        #endregion
        public HolidayReportRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }

        public async Task<IEnumerable<EmployeeHolidays>> GetEmployeeWithMaternityLeaves(string organizationIdentifier, int month,int type, string[] employeeIdentifiers)
        {
            try
            {
                var sql = @"select * from EmployeeHolidays where HolidayStartDate <= DATEADD(DAY, (30*@month), GETDATE()) and HolidayStartDate >= GETDATE() and holidayType = 0 and organizationIdentifier = @organizationIdentifier and @type = 1 and EmployeeIdentifier in @employeeIdentifier
                            union all 
                            select * from EmployeeHolidays where HolidayEndDate <= DATEADD(DAY, (30*@month), GETDATE()) and HolidayEndDate >= GETDATE() and HolidayStartDate < GETDATE() and holidayType = 0 and organizationIdentifier = @organizationIdentifier and @type = 0 and EmployeeIdentifier in @employeeIdentifier";
                return await db.QueryAsync<EmployeeHolidays>(sql, new { @organizationIdentifier = organizationIdentifier, @month = month, @type = type, @employeeIdentifier = employeeIdentifiers }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<dynamic> GetEmployeeWithSickLeaves(string organizationIdentifier,int month,int days, string[] employeeIdentifiers)
        {
            try
            {
                var sql = @"select Sum(e.holidays) TotalDays,h.businessIdentifier,h.costcenterIdentifier,h.EmployeeIdentifier,h.HolidayStatus from EmployeeHolidays h inner join 
                            (select DATEDIFF(DAY,HolidayStartDate,HolidayEndDate) holidays,EmployeeIdentifier,EmployeeHolidayId from EmployeeHolidays 
                            where HolidayStartDate >= DATEADD(DAY, -(30 * @month), GETDATE()) and HolidayEndDate <= GETDATE() and holidayType = 1 and organizationIdentifier = @organizationIdentifier and EmployeeIdentifier in @employeeIdentifiers) e
                            on h.EmployeeHolidayId = e.EmployeeHolidayId 
                            group by h.EmployeeIdentifier,h.businessIdentifier,h.costcenterIdentifier,h.HolidayStatus
                            having Sum(e.holidays) >= @numdays";
                return await db.QueryAsync<dynamic>(sql, new { @organizationIdentifier = organizationIdentifier,@month = month, @numdays = days, @employeeIdentifiers = employeeIdentifiers}).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
