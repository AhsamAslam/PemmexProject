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

        public async Task AddCompensationSalary(CompensationSalaries CompensationSalaries)
        {
            try
            {
                var Sql = "INSERT INTO CompensationSalaries " +
                    "(EmployeeIdentifier,BaseSalary,AdditionalAgreedPart," +
                    "CarBenefit,InsuranceBenefit,PhoneBenefit,EmissionBenefit," +
                    "HomeInternetBenefit,TotalMonthlyPay,IssuedDate,Created,CreatedBy) " +
                    "VALUES(@EmployeeIdentifier,@BaseSalary,@AdditionalAgreedPart," +
                    "@CarBenefit,@InsuranceBenefit,@PhoneBenefit,@EmissionBenefit," +
                    "@HomeInternetBenefit,@TotalMonthlyPay,@IssuedDate,GETDATE(),'Test')";
                await db.ExecuteAsync(Sql, CompensationSalaries).ConfigureAwait(false);
    
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task UpdateCompensationSalary(CompensationSalaries CompensationSalaries)
        {
            try
            {
                var Sql = "UPDATE CompensationSalaries SET BaseSalary = @BaseSalary," +
                    "AdditionalAgreedPart = @AdditionalAgreedPart,CarBenefit = @CarBenefit," +
                    "InsuranceBenefit = @InsuranceBenefit,PhoneBenefit = @PhoneBenefit," +
                    "EmissionBenefit = @EmissionBenefit,HomeInternetBenefit = @HomeInternetBenefit," +
                    "TotalMonthlyPay = @TotalMonthlyPay,IssuedDate = @IssuedDate,LastModified = GetDate()," +
                    "LastModifiedBy = 'Test1' WHERE CompensationSalaryId = @CompensationSalaryId";
                  
                await db.ExecuteAsync(Sql, CompensationSalaries).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CompensationSalaries> GetCompensationSalariesByEmployeeIdentifier(string employeeIdentifier)
        {
            try
            {
                var sql = "  select top 1 * from CompensationSalaries with (nolock) " +
                    "where EmployeeIdentifier = @employeeIdentifier and " +
                    "YEAR(IssuedDate) = Year(GETDATE()) and MONTH(IssuedDate) = MONTH(GETDATE())";
                return await db.QueryFirstOrDefaultAsync<CompensationSalaries>(sql, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.Compensation>> GetCompensationTotalAmountByOrganizationIdentifier(string organizationIdentifier)
        {
            try
            {
                var Sql = "Select TotalMonthlyPay from Compensation with (nolock) " +
                    "where organizationIdentifier = @organizationIdentifier";
                return await db.QueryAsync<Entities.Compensation>(Sql, new { @organizationIdentifier  = organizationIdentifier }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
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

        public async Task<IEnumerable<Entities.Compensation>> GetCurrentSalaries(string employeeIdentifier)
        {
            try
            {
                var sql = "select from Compensation (nolock) where EmployeeIdentifier = @employeeIdentifier ORDER BY IssuedDate DESC";
                return await db.QueryAsync<Entities.Compensation>(sql, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
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

        public async Task<List<Entities.Compensation>> GetFunctionalSalaryCount(string[] employeeIdentifier)
        {
            try
            {
                List<Entities.Compensation> Compensation = new List<Entities.Compensation>();
                foreach (var item in employeeIdentifier)
                {
                    var sql = "Select * from Compensation with (nolock) where " +
                        "EmployeeIdentifier = @employeeIdentifier";
                    Compensation.Add(await db.QueryFirstOrDefaultAsync<Entities.Compensation>(sql, new { @employeeIdentifier = item }).ConfigureAwait(false));
                }
                return Compensation;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
