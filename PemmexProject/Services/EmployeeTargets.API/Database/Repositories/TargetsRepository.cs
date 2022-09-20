using Dapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeTargets.API.Database.Repositories
{
    public class TargetsRepository : ITargetsRepository
    {
        #region
        private IDbConnection db;
        #endregion
        public TargetsRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("EmployeeTargetsConnection"));
        }
        public async Task<int> CreateSoftTargets(Targets target)
        {
            try
            {
                var sql = @"INSERT INTO Targets
                           (TargetsTitle,TargetsDescription,MeasurementCriteria,
                            PerformanceCriteria,TargetsType,Weightage,
                            TargetsDeadLineDate,EmployeeIdentifier,OrganizationIdentifier,
                            BusinessIdentifier,ManagerIdentifier,isOrganizationBaseTarget,
                            isBusinessBaseTarget,isBusinessUnitBaseTarget,isTeamBaseTarget,
                            isActive,Created,CreatedBy) VALUES 
                            (@TargetsTitle,@TargetsDescription,@MeasurementCriteria,
                            @PerformanceCriteria,@TargetsType,@Weightage,
                            @TargetsDeadLineDate,@EmployeeIdentifier,
                            @OrganizationIdentifier,@BusinessIdentifier,
                            @ManagerIdentifier,@isOrganizationBaseTarget,
                            @isBusinessBaseTarget,@isBusinessUnitBaseTarget,
                            @isTeamBaseTarget,1,GETDATE(),'test')";
                return await db.ExecuteAsync(sql, target).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Targets>> ShowSoftTargets()
        {
            try
            {
                //string param = string.Join(", ", employeeIdentifiers.Select(f => "\'" + f + "\'"));
                var sql = @" Select * from Targets with (nolock) where ISNULL(isActive, 0) =1 ";
                return await db.QueryAsync<Targets>(sql).ConfigureAwait(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
