using Compensation.API.Database.Entities;
using Compensation.API.Database.Interfaces;
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
    public class FunctionalBudgetRepository : IFunctionalBudgetRepository
    {
        #region
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;
        #endregion
        public FunctionalBudgetRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value);

        }
        public async Task<int> Save(FunctionalBudget functionalBudget)
        {
            try
            {
                var Sql = @"INSERT INTO FunctionalBudgets(Emp_Guid,
                    EmployeeIdentifier,CurrentTitle,
                    NewTitle,ManagerIdentifier
                    ,ManagerName,CostCenterIdentifier,CostCenterName
                    ,OrganizationCountry,CurrentGrade,NewGrade,JobFunction,BaseSalary
                    ,AdditionalAgreedPart,TotalCurrentSalary,mandatoryPercentage
                    ,IncreaseInPercentage,NewBaseSalary,NewTotalSalary,EffectiveDate,IncreaseInCurrency
                    ,FirstName,LastName,organizationIdentifier,businessIdentifier
                    , Created,isActive, CreatedBy) VALUES
                    (@Emp_Guid,@EmployeeIdentifier,@CurrentTitle,@NewTitle,@ManagerIdentifier
                    ,@ManagerName,@CostCenterIdentifier,@CostCenterName
                    ,@OrganizationCountry,@CurrentGrade,@NewGrade,@JobFunction,@BaseSalary
                    ,@AdditionalAgreedPart,@TotalCurrentSalary,@mandatoryPercentage
                    ,@IncreaseInPercentage,@NewBaseSalary,@NewTotalSalary,@EffectiveDate,
                    @IncreaseInCurrency,@FirstName,@LastName,@organizationIdentifier,@businessIdentifier,GETDATE(),1,'" + CurrentUser + "')";
                return await db.ExecuteAsync(Sql, functionalBudget).ConfigureAwait(false);                
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<OrganizationBudget>> GetOrganizationBudget(string organizationIdentifier)
        {
            try
            {
                var sql = @"SELECT b.* from  OrganizationBudgets b 
				            inner join 
				            (select Max(startDate) as MaxDate,organizationIdentifier from OrganizationBudgets 
                            where organizationIdentifier = @organizationIdentifier group by organizationIdentifier)a
				            on a.organizationIdentifier = b.organizationIdentifier and a.MaxDate = b.startDate";
                return await db.QueryAsync<OrganizationBudget>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<FunctionalBudget>> Get(string[] managerIdentifier)
        {
            try
            {
                var sql = @"SELECT *
                        FROM [CompensationDB].[dbo].[FunctionalBudgets] where managerIdentifier in @managerIdentifier and isActive =1";
                return await db.QueryAsync<FunctionalBudget>(sql, new { @managerIdentifier = managerIdentifier }).ConfigureAwait(false);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<FunctionalBudget>> GetAll(string organizationIdentifier)
        {
            var sql = @"SELECT *
                        FROM [CompensationDB].[dbo].[FunctionalBudgets] where organizationIdentifier = @organizationIdentifier and isActive =1";
            return await db.QueryAsync<FunctionalBudget>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
        }
    }
}





