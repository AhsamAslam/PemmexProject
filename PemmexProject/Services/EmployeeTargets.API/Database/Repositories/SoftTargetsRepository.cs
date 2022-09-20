using Dapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeTargets.API.Database.Repositories
{
    public class SoftTargetsRepository : ISoftTargetsRepository
    {
        #region
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;
        #endregion
        public SoftTargetsRepository(IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("EmployeeTargetsConnection"));
            
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value);

        }
        public async Task<int> CreateSoftTargets(SoftTargetsDto target)
        {
            try
            {
                var sql = @"INSERT INTO SoftTargets 
                            (SoftTargetsName,SoftTargetsDescription,
                            PerformanceCriteria,OrganizationIdentifier
                            ,EvaluationDateTime,isActive,TargetType,Created,CreatedBy)
                            OUTPUT INSERTED.SoftTargetsId
                            VALUES (@SoftTargetsName,@SoftTargetsDescription,@PerformanceCriteria,
                            @OrganizationIdentifier,@EvaluationDateTime,
                            1,@TargetType,GETDATE(),'" + CurrentUser + "')";
                //var v = await _dapperService.ExecuteAsync(sql, target);
                //return v;
                var Id = db.QuerySingle<int>(sql, new { SoftTargetsName = target.SoftTargetsName, SoftTargetsDescription = target.SoftTargetsDescription,
                         PerformanceCriteria = target.PerformanceCriteria, OrganizationIdentifier = target.OrganizationIdentifier,EvaluationDateTime = target.EvaluationDateTime,TargetType = target.TargetType});
                var DetailSql = @"INSERT INTO SoftTargetsDetail 
                                  (EmployeeIdentifier,BusinessIdentifier,
                                  ManagerIdentifier,CostCenterIdentifier,SoftTargetsId) VALUES 
                                  (@EmployeeIdentifier,@BusinessIdentifier,
                                  @ManagerIdentifier,@CostCenterIdentifier,'" + Id + "')";

                var detailId = await db.ExecuteAsync(DetailSql, target.SoftTargetsDetail).ConfigureAwait(false);

                return Id;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ShowSoftTargetsDto>> ShowSoftTargets(string employeeIdentifier)
        {
            try
            {
                //string param = string.Join(", ", employeeIdentifiers.Select(f => "\'" + f + "\'"));
                var sql = @"Select * from SoftTargets with (nolock) where EmployeeIdentifier = @employeeIdentifier and isnull(isActive,1) = 1";
                return await db.QueryAsync<ShowSoftTargetsDto>(sql, new { @employeeIdentifier = employeeIdentifier}).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<ShowSoftTargetsDto>> ShowSoftTargetsByUserId(string userId, string employeeIdentifier)
        {
            try
            {
                //string param = string.Join(", ", employeeIdentifiers.Select(f => "\'" + f + "\'"));
                var sql = @"Select s.*
                           from SoftTargets s with (nolock)
                           where CreatedBy = @userId and isnull(isActive,1) = 1
                           
                            Union
                            Select s.*
                             from SoftTargets s with (nolock)
                            inner join SoftTargetsDetail sd
                            on s.SoftTargetsId = sd.SoftTargetsId
                            where isnull(isActive,1) = 1 and sd.EmployeeIdentifier = @employeeIdentifier";
                return await db.QueryAsync<ShowSoftTargetsDto>(sql, new { @userId = userId, @employeeIdentifier = employeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
