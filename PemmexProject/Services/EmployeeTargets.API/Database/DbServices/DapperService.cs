using Dapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PemmexCommonLibs.Domain.Common;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeTargets.API.Database.DbServices
{
    public class DapperService: IDapperService
    {
        #region
        private readonly IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion

        public DapperService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("EmployeeTargetsConnection"));
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            
            //foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            //{
               
            //    entry.Entity.CreatedBy = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "sub").Value;
            //    entry.Entity.Created = DateTime.Now;
                   
            //}
            return await db.ExecuteAsync(sql, param, transaction).ConfigureAwait(false);
        }
        public async Task<IReadOnlyList<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return (await db.QueryAsync<T>(sql, param, transaction)).AsList();
        }
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await db.QueryFirstOrDefaultAsync<T>(sql, param, transaction);
        }
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, CancellationToken cancellationToken = default)
        {
            return await db.QuerySingleAsync<T>(sql, param, transaction);
        }
    }
}
