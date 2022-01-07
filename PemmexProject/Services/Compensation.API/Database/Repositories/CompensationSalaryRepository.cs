using Compensation.API.Database.Entities;
using Compensation.API.Database.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories
{
    public class CompensationSalaryRepository : ICompensationSalaryRepository
    {
        #region
        private IDbConnection db;
        #endregion
        public CompensationSalaryRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
        }

        public async Task<Entities.Compensation> GetCurrentCompensation(string employeeIdentifier)
        {
            try
            {
                var sql = "select top 1 * from Compensation (nolock) where EmployeeIdentifier = @employeeIdentifier ORDER BY IssuedDate DESC";
                return await db.QueryFirstOrDefaultAsync<Entities.Compensation>(sql, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.Compensation>> GetCurrentJobFunctionCompensations(string businessIdentifer)
        {
            try
            {
                var sql = @"select EmployeeIdentifier,BaseSalary,AdditionalAgreedPart,CarBenefit,InsuranceBenefit,PhoneBenefit,EmissionBenefit,HomeInternetBenefit
                            , TotalMonthlyPay, businessIdentifier, organizationIdentifier, MAX(EffectiveDate) from Compensation
                            where businessIdentifier = @businessIdentifer
                            Group by EmployeeIdentifier,BaseSalary,AdditionalAgreedPart,CarBenefit,InsuranceBenefit,PhoneBenefit,EmissionBenefit,HomeInternetBenefit,
                            TotalMonthlyPay,businessIdentifier,organizationIdentifier";
                return await db.QueryAsync<Entities.Compensation>(sql, new { @businessIdentifer = businessIdentifer }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CompensationSalaries> GetCurrentSalary(string employeeIdentifier)
        {
            try
            {
                var sql = "select top 1 * from CompensationSalaries (nolock) where EmployeeIdentifier = @employeeIdentifier ORDER BY IssuedDate DESC";
                return await db.QueryFirstOrDefaultAsync<CompensationSalaries>(sql, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
