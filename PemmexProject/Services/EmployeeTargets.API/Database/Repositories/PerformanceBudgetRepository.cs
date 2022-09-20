using Dapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeTargets.API.Database.Repositories
{
    public class PerformanceBudgetRepository: IPerformanceBudgetRepository
    {
        #region
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;
        #endregion
        public PerformanceBudgetRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("EmployeeTargetsConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value);
        }

        public async Task<int> CreatePerfromanceBudgetPlanning(PerfromanceBudgetPlanning perfromanceBudgetPlanning)
        {
            try
            {
                //var CurrentUser = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "sub").Value;

                var sql = @"INSERT INTO PerfromanceBudgetPlanning 
                            (startDate,endDate,companyProfitabilityMultiplier,
                            bonusPayoutDate,Created,organizationIdentifier,CreatedBy) VALUES 
                            (@startDate,@endDate,@companyProfitabilityMultiplier,
                            @bonusPayoutDate,GETDATE(),@organizationIdentifier,'" + CurrentUser + "')";
                return await db.ExecuteAsync(sql, perfromanceBudgetPlanning).ConfigureAwait(false);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<PerfromanceBudgetPlanning> GetCreatePerfromanceBudgetPlanning(string organizationIdentifier)
        {
            try
            {
                var sql = @"Select top(1) * from PerfromanceBudgetPlanning with (nolock) where organizationIdentifier = @organizationIdentifier order by startDate desc";
                return await db.QueryFirstOrDefaultAsync<PerfromanceBudgetPlanning>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
                
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> CreatePerformanceEvaluationSummary(List<PerformanceEvaluationSummary> performanceEvaluationSummaries)
        {
            try
            {

                var sql = @"INSERT INTO PerformanceEvaluationSummary 
                            (name,title,country,grade,jobFunction,totalSalary,
                            bonusPercentage,bonusAmount,resultedBonusPercentage,
                            resultedBonusAmountBeforeMultiplier,performanceMultiplier,
                            resultedBonusAmountAfterMultiplier,companyProfitabilityMultiplier,
                            finalBonusAmount,bonusPayoutDate,employeeIdentifier,
                            organizationIdentifier,businessIdentifier,managerIdentifier,
                            Created,IsActive,CreatedBy) VALUES (@name,@title,@country,
                            @grade,@jobFunction,@totalSalary,@bonusPercentage,
                            @bonusAmount,@resultedBonusPercentage,@resultedBonusAmountBeforeMultiplier,
                            @performanceMultiplier,@resultedBonusAmountAfterMultiplier,
                            @companyProfitabilityMultiplier,@finalBonusAmount,
                            @bonusPayoutDate,@employeeIdentifier,@organizationIdentifier,
                            @businessIdentifier,@managerIdentifier,GETDATE(),1,'" + CurrentUser + "')";
                return await db.ExecuteAsync(sql, performanceEvaluationSummaries).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PerformanceEvaluationSummary>> GetPerformanceEvaluationSummary(string organizationIdentifier)
        {
            try
            {
                var sql = @"Select * from PerformanceEvaluationSummary with (nolock) where organizationIdentifier = @organizationIdentifier And isnull(IsActive,1) = 1";
                return await db.QueryAsync<PerformanceEvaluationSummary>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IEnumerable<PerformanceEvaluationSummary>> GetPerformanceEvaluationSummaryDetail(string[] employeeIdentifiers)
        {
            try
            {
                var sql = @"Select * from PerformanceEvaluationSummary with (nolock) where employeeIdentifier in @employeeIdentifiers And isnull(IsActive,1) = 1";
                return await db.QueryAsync<PerformanceEvaluationSummary>(sql, new { @employeeIdentifiers = employeeIdentifiers }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> CreatePerformanceEvaluationSetting(PerformanceEvaluationSettings performanceEvaluationSettings)
        {
            try
            {
                var sql = @"INSERT INTO PerformanceEvaluationSettings 
                            (organizationIdentifier,minimumPercentage,targetPercentage,maximumPercentage,
                            isActive,Created,CreatedBy) VALUES 
                            (@organizationIdentifier,@minimumPercentage,@targetPercentage,
                            @maximumPercentage,1,GETDATE(),'" + CurrentUser + "')";
                return await db.ExecuteAsync(sql, performanceEvaluationSettings).ConfigureAwait(false);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> DeletePerformanceEvaluationSetting(int Id)
        {
            try
            {
                var sql = @"Update PerformanceEvaluationSettings set isActive = 0 where performanceEvaluationSettingsId = @Id";
                return await db.ExecuteAsync(sql, new { @Id = Id }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PerformanceEvaluationSettings>> ShowPerformanceEvaluationSetting(string organizationIdentifier)
        {
            try
            {
                var sql =  @"Select * from PerformanceEvaluationSettings where organizationIdentifier = @organizationIdentifier and isnull(isActive,1) = 1";
                return await db.QueryAsync<PerformanceEvaluationSettings>(sql, new { @organizationIdentifier = organizationIdentifier }).ConfigureAwait(false);

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<double> GetBonusAmountByEmployeeIdentifier(string employeeIdentifier)
        {
            try
            {
                var sql = @"Select finalBonusAmount from PerformanceEvaluationSummary with (nolock) where employeeIdentifier = @employeeIdentifier and ISNULL(IsActive,1) =1";
                return await db.QueryFirstAsync<double>(sql, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdatePerformanceEvaluationSummaryIsActive(string[] employeeIdentifier)
        {
            try
            {
                var sqlPerformanceEvaluationSummary = @"Update PerformanceEvaluationSummary set 
                            IsActive = 0
                            where 
                            employeeIdentifier in @employeeIdentifier";
                var performanceEvaluationSummary =  await db.ExecuteAsync(sqlPerformanceEvaluationSummary, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);

                var sqlHardTargets = @"UPDATE H    
                                       SET isActive = 0
                                       from HardTargets H
                                       INNER JOIN HardTargetsDetail HD
                                       ON H.HardTargetsId = HD.HardTargetsId
                                       where EmployeeIdentifier in @employeeIdentifier";
                var hardTargets = await db.ExecuteAsync(sqlHardTargets, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);

                var sqlSoftTargets = @"UPDATE S   
                                     SET isActive = 0
                                     from SoftTargets S
                                     INNER JOIN SoftTargetsDetail SD
                                     ON S.SoftTargetsId = SD.SoftTargetsId
                                     where EmployeeIdentifier in @employeeIdentifier";
                var softTargets = await db.ExecuteAsync(sqlSoftTargets, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
                return 1;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
