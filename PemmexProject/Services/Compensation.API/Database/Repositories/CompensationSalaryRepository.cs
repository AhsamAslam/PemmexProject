using Compensation.API.Database.Entities;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using Dapper;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;

        #endregion
        public CompensationSalaryRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value);
        }

        public async Task<IEnumerable<Entities.Compensation>> GetCurrentCompensation(string[] employeeIdentifiers)
        {
            try
            {
                //string param = string.Join(", ", employeeIdentifiers.Select(f => "\'" + f + "\'"));
                string param = string.Join(",", employeeIdentifiers);
                var sql = @"Select t.*
                from Compensation t
                inner
                join
                (SELECT EmployeeIdentifier, BaseSalary, AdditionalAgreedPart, CarBenefit, InsuranceBenefit, PhoneBenefit,
                EmissionBenefit, HomeInternetBenefit, TotalMonthlyPay,currencyCode, MAX(EffectiveDate) as max_date
                FROM Compensation
                where EmployeeIdentifier in @employeeIdentifier
                GROUP BY EmployeeIdentifier,BaseSalary,AdditionalAgreedPart,CarBenefit,InsuranceBenefit,PhoneBenefit,
                EmissionBenefit,HomeInternetBenefit,TotalMonthlyPay,currencyCode)a
                on a.EmployeeIdentifier = t.EmployeeIdentifier and a.max_date = EffectiveDate";
                return await db.QueryAsync<Entities.Compensation>(sql, new { @employeeIdentifier = employeeIdentifiers }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CompensationTotalMonthlyPayDto>> GetCurrentCompensation(string[] employeeIdentifiers, int totalMonth)
        {
            try
            {
                var sql = @"SELECT EmployeeIdentifier,SUM(TotalMonthlyPay) TotalMonthlyPay
                            FROM
                                (SELECT
                                 TotalMonthlyPay, EmployeeIdentifier
                                 FROM CompensationSalaries where EmployeeIdentifier in @employeeIdentifier) AS foo
                               	 Group by EmployeeIdentifier";
                return await db.QueryAsync<CompensationTotalMonthlyPayDto>(sql, new { @employeeIdentifier = employeeIdentifiers, @totalMonth = totalMonth }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Entities.Compensation>> GetOrganizationCurrentCompensations(string organizationIdentifier)
        {
            try
            {
                var sql = @"select EmployeeIdentifier,BaseSalary,AdditionalAgreedPart,CarBenefit,InsuranceBenefit,PhoneBenefit,EmissionBenefit,HomeInternetBenefit
                            , TotalMonthlyPay, businessIdentifier, organizationIdentifier,currencyCode, MAX(EffectiveDate) from Compensation
                            where organizationIdentifier = @organizationIdentifier
                            Group by EmployeeIdentifier,BaseSalary,AdditionalAgreedPart,CarBenefit,InsuranceBenefit,PhoneBenefit,EmissionBenefit,HomeInternetBenefit,
                            TotalMonthlyPay,businessIdentifier,organizationIdentifier,currencyCode";
                return await db.QueryAsync<Entities.Compensation>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
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

        public async Task<int> SaveCompensation(Entities.Compensation compensation)
        {
            try
            {
                var sql = @"insert into Compensation([EmployeeIdentifier]
                          ,[BaseSalary],[AdditionalAgreedPart],[CarBenefit],[InsuranceBenefit],[PhoneBenefit]
                          ,[EmissionBenefit],[HomeInternetBenefit],[TotalMonthlyPay],[EffectiveDate],
                           [organizationIdentifier],[businessIdentifier],[currencyCode],[Created],[CreatedBy]) 
                          values(@EmployeeIdentifier
                          ,@BaseSalary
                          ,@AdditionalAgreedPart
                          ,@CarBenefit
                          ,@InsuranceBenefit
                          ,@PhoneBenefit
                          ,@EmissionBenefit
                          ,@HomeInternetBenefit
                          ,@TotalMonthlyPay,@EffectiveDate,@organizationIdentifier,
                          @businessIdentifier,@currencyCode, GETDATE(),'" + CurrentUser + "')";
                return await db.ExecuteAsync(sql, compensation).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> SaveSalary(Entities.CompensationSalaries compensationSalary)
        {
            try
            {
                var sql = @"insert into CompensationSalaries([EmployeeIdentifier]
                          ,[BaseSalary],[AdditionalAgreedPart],[CarBenefit],[InsuranceBenefit],[PhoneBenefit]
                          ,[EmissionBenefit],[HomeInternetBenefit],[TotalMonthlyPay]
                          ,[one_time_bonus]
                          ,[annual_bonus]
                          ,[IssuedDate]
                          ,[organizationIdentifier],[businessIdentifier],[Created],[CreatedBy]) 
                          values(@EmployeeIdentifier
                          ,@BaseSalary
                          ,@AdditionalAgreedPart
                          ,@CarBenefit
                          ,@InsuranceBenefit
                          ,@PhoneBenefit
                          ,@EmissionBenefit
                          ,@HomeInternetBenefit
                          ,@TotalMonthlyPay
                          ,@one_time_bonus
                          ,@annual_bonus
                          ,@IssuedDate                          
                          ,@organizationIdentifier,
                          @businessIdentifier, GETDATE(),'" + CurrentUser + "')";
                return await db.ExecuteAsync(sql, compensationSalary).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CompensationSalaries>> GetOrganizationCurrentSalary(string organizationIdentifier, DateTime startDate, DateTime endDate)
        {
            try
            {
                var sql = @"select EmployeeIdentifier,BaseSalary,AdditionalAgreedPart,CarBenefit,InsuranceBenefit,PhoneBenefit,EmissionBenefit,HomeInternetBenefit
                            ,TotalMonthlyPay, businessIdentifier, organizationIdentifier,currencyCode,IssuedDate from CompensationSalaries
                            where organizationIdentifier = @organizationIdentifier and (Month(IssuedDate) >= Month(@startDate) and YEAR(IssuedDate) >= YEAR(@startDate))
                            and (Month(IssuedDate) <= Month(@endDate) and YEAR(IssuedDate) <= YEAR(@endDate))";
                return await db.QueryAsync<CompensationSalaries>(sql, new { @organizationIdentifier = organizationIdentifier, @startDate = startDate, @endDate = endDate }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<CompensationSalaries>> GetTeamBonus(string[] employeeIdentifiers)
        {
            try
            {
                var sql = @"select EmployeeIdentifier,one_time_bonus from CompensationSalaries 
                            where EmployeeIdentifier in @employeeIdentifier";
                return await db.QueryAsync<CompensationSalaries>(sql, new { @employeeIdentifier = employeeIdentifiers }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<double> GetOrganizationTotalSalaryCountByDate(string organizationIdentifier, DateTime startDate, DateTime endDate)
        {
            try
            {
                var sql = @"Select SUM(TotalMonthlyPay) TotalMonthlyPay  from CompensationSalaries where 
                           organizationIdentifier = @organizationIdentifier and 
                           (Month(IssuedDate) >= Month(@startDate) and 
                           YEAR(IssuedDate) >= YEAR(@startDate)) and 
                           (Month(IssuedDate) <= Month(@endDate) and 
                           YEAR(IssuedDate) <= YEAR(@endDate))";
                var totalPay =  await db.QueryFirstAsync<double>(sql, new { @organizationIdentifier = organizationIdentifier, @startDate = startDate, @endDate = endDate }).ConfigureAwait(false);
                if (totalPay == null)
                {
                    throw new Exception("Record Not Found");
                }
                return totalPay;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
