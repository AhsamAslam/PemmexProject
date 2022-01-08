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
    public class CompanyToEmployeeHolidaysRepository : ICompanyToEmployeeHolidays
    {
        #region
        private IDbConnection db;
        #endregion
        public CompanyToEmployeeHolidaysRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }
        public Task<CompanyToEmployeeHolidays> AddCompanyToEmployeeHolidays(CompanyToEmployeeHolidays CompanyToEmployeeHolidays)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteCompanyToEmployeeHolidays(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidays()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidaysByCostcenterIdentifierAndHolidaySettingsIdentitfier(string CostcenterIdentifier, Guid HolidaySettingsIdentitfier)
        {
            try
            {
                var sql = "Select * from CompanyToEmployeeHolidays where costcenterIdentifier = @CostcenterIdentifier and HolidaySettingsIdentitfier = @HolidaySettingsIdentitfier";
                return await db.QueryAsync<CompanyToEmployeeHolidays>(sql, new { @CostcenterIdentifier = CostcenterIdentifier, @HolidaySettingsIdentitfier = HolidaySettingsIdentitfier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CompanyToEmployeeHolidays> GetCompanyToEmployeeHolidaysByEmployeeIdAndHolidaySettingsIdentitfier(Guid EmployeeId, Guid HolidaySettingsIdentitfier)
        {
            try
            {
                var sql = "Select top(1) * from CompanyToEmployeeHolidays where EmployeeId = @EmployeeId and HolidaySettingsIdentitfier = @HolidaySettingsIdentitfier";
                return await db.QueryFirstOrDefaultAsync<CompanyToEmployeeHolidays>(sql, new { @EmployeeId = EmployeeId, @HolidaySettingsIdentitfier = HolidaySettingsIdentitfier}).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidaysByHolidaySettingsIdentitfier(Guid HolidaySettingsIdentitfier)
        {
            try
            {
                var sql = "Select * from CompanyToEmployeeHolidays where HolidaySettingsIdentitfier = @HolidaySettingsIdentitfier";
                return await db.QueryAsync<CompanyToEmployeeHolidays>(sql, new { @HolidaySettingsIdentitfier = HolidaySettingsIdentitfier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<CompanyToEmployeeHolidays>> GetCompanyToEmployeeHolidaysById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyToEmployeeHolidays> UpdateCompanyToEmployeeHolidays(CompanyToEmployeeHolidays CompanyToEmployeeHolidays)
        {
            throw new NotImplementedException();
        }
    }
}
