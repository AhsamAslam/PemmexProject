using Dapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeTargets.API.Database.Repositories
{
    public class HardTargetsRepository : IHardTargetsRepository
    {
        #region
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;
        #endregion
        public HardTargetsRepository(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("EmployeeTargetsConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value);

        }

        public async Task<int> CreateHardTargets(HardTargetsDto target)
        {
            try
            {
                var sql = @"INSERT INTO dbo.HardTargets(HardTargetsName,HardTargetsDescription,
                          MeasurementCriteria,Weightage,MeasurementCriteriaResult,
                          OrganizationIdentifier,EvaluationDateTime,isActive,TargetType,Created,CreatedBy)
                          OUTPUT INSERTED.HardTargetsId
                          VALUES (@HardTargetsName,@HardTargetsDescription,@MeasurementCriteria,
                          @Weightage,@MeasurementCriteriaResult,
                          @OrganizationIdentifier,@EvaluationDateTime,1,@TargetType,GETDATE(),'" + CurrentUser + "')";
                var Id = db.QuerySingle<int>(sql, new
                {
                    HardTargetsName = target.HardTargetsName,
                    HardTargetsDescription = target.HardTargetsDescription,
                    MeasurementCriteria = target.MeasurementCriteria,
                    Weightage = target.Weightage,
                    OrganizationIdentifier = target.OrganizationIdentifier,
                    EvaluationDateTime = target.EvaluationDateTime,
                    TargetType = target.TargetType,
                    MeasurementCriteriaResult = target.MeasurementCriteriaResult
                });

                var detailSql = @"INSERT INTO HardTargetsDetail 
                                  (EmployeeIdentifier,BusinessIdentifier,
                                  ManagerIdentifier,CostCenterIdentifier,HardTargetsId) VALUES 
                                  (@EmployeeIdentifier,@BusinessIdentifier,
                                  @ManagerIdentifier,@CostCenterIdentifier,'" + Id + "')";

                var detailId = await db.ExecuteAsync(detailSql, target.HardTargetsDetail).ConfigureAwait(false);

                return Id;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ShowHardTargetsDto>> ShowHardTargets(string employeeIdentifier)
        {
            try
            {
                //string param = string.Join(", ", employeeIdentifiers.Select(f => "\'" + f + "\'"));
                var sql = @"Select * from HardTargets h with (nolock)
                            inner join HardTargetsDetail hd 
                            on h.HardTargetsId = hd.HardTargetsId
                            where EmployeeIdentifier = @employeeIdentifier and isnull(isActive,1) = 1";
                return await db.QueryAsync<ShowHardTargetsDto>(sql, new { @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ShowHardTargetsDto>> ShowHardTargetsByUserId(string userId, string employeeIdentifier)
        {
            try
            {
                //string param = string.Join(", ", employeeIdentifiers.Select(f => "\'" + f + "\'"));
                var sql = @"Select h.*
                            from HardTargets h with (nolock)
                            where CreatedBy = @userId and isnull(isActive,1) = 1
                            
                             Union
                             Select h.*
                             from HardTargets h inner join HardTargetsDetail hd
                             on h.HardTargetsId = hd.HardTargetsId
                             where isnull(isActive,1) = 1 and hd.EmployeeIdentifier = @employeeIdentifier";
                return await db.QueryAsync<ShowHardTargetsDto>(sql, new { @userId = userId, @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateHardTargetsMeasurementCriteriaResult(int Id, double MeasurementCriteriaResult)
        {
            try
            {
                var sql = @"update HardTargets set 
                            MeasurementCriteriaResult = @MeasurementCriteriaResult
                            where HardTargetsId = @Id";
                return await db.ExecuteAsync(sql, new { @Id = Id, @MeasurementCriteriaResult = MeasurementCriteriaResult }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> UpdateHardTargetsWeightage(int Id, double Weightage)
        {
            try
            {
                var sql = @"update HardTargets set 
                            Weightage = @Weightage
                            where HardTargetsId = @Id";
                return await db.ExecuteAsync(sql, new { @Id = Id, @Weightage = Weightage }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
