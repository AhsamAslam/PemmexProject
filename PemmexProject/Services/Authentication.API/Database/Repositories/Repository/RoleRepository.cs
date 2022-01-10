using Authentication.API.Database.Entities;
using Authentication.API.Database.Repositories.Interface;
using Authentication.API.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Database.Repositories.Repository
{
    public class RoleRepository : IRole
    {
        #region
        private IDbConnection db;
        #endregion
        public RoleRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("AuthenticationConnection"));
        }
        public async Task<RoleDto> SaveRole(RoleDto Role)
        {
            try
            {
                var Sql = "INSERT INTO Roles (roleName,OrganizationIdentifier,Created,CreatedBy) " +
                    "VALUES (@roleName ,@OrganizationIdentifier ,GETDATE() ,'test' )";
                
                await db.ExecuteAsync(Sql, Role.role).ConfigureAwait(false);
                return Role;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
