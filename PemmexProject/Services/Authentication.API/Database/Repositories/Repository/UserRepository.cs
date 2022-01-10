using Authentication.API.Database.Entities;
using Authentication.API.Database.Repositories.Interface;
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
    public class UserRepository : IUser
    {
        #region
        private IDbConnection db;
        #endregion
        public UserRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("AuthenticationConnection"));
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            try
            {
                var Sql = "Select * from Users u inner join Users us on u.ManagerIdentifier = us.EmployeeIdentifier";
                return await db.QueryAsync<User>(Sql).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> GetUserById(Guid Id)
        {
            try
            {
                var Sql = "Select * from Users where Id = @Id and isnull(isActive,1) = 1";
                return await db.QueryFirstOrDefaultAsync<User>(Sql, new { @Id = Id }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<User> UpdateUser(User User)
        {
            try
            {
                var Sql = "UPDATE Users SET Id = @Id,FirstName = @FirstName," +
                    "LastName = @LastName,Title = @Title,Email = @Email," +
                    "EmployeeIdentifier = @EmployeeIdentifier,ManagerIdentifier = @ManagerIdentifier," +
                    "CostCenterIdentifier = @CostCenterIdentifier,BusinessIdentifier = @BusinessIdentifier," +
                    "OrganizationIdentifier = @OrganizationIdentifier,UserName = @UserName," +
                    "Password = @Password ,Role = @Role,IsPasswordReset = @IsPasswordReset," +
                    "IsTwoStepAuthEnabled = @IsTwoStepAuthEnabled," +
                    "PasswordResetCode = @PasswordResetCode,PasswordResetCodeTime = @PasswordResetCodeTime," +
                    "isActive = @isActive,JobFunction = @JobFunction,Grade = @Grade," +
                    "OrganizationCountry = @OrganizationCountry,Created = @Created,CreatedBy = @CreatedBy," +
                    "LastModified = @LastModified,LastModifiedBy = @LastModifiedBy WHERE Id = @Id";
                await db.ExecuteAsync(Sql, User).ConfigureAwait(false);
                return User;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
