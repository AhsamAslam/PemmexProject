using Compensation.API.Database.Entities;
using Compensation.API.Database.Repositories.Interface;
using Compensation.API.Dtos;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Repositories.Repository
{
    public class BonusRepository: IBonus
    {
        #region
        private IDbConnection db;
        #endregion
        public BonusRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("CompensationConnection"));
        }

        public async Task<IEnumerable<CompensationSalaries>> GetCompensationSalariesByEmployeeIdentifier(string EmployeeIdentifier)
        {
            try
            {
                var sql = "Select * from CompensationSalaries where EmployeeIdentifier = @EmployeeIdentifier and YEAR(IssuedDate) = Year(GETDATE())";
                return await db.QueryAsync<CompensationSalaries>(sql, new { @EmployeeIdentifier = EmployeeIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UserBonus>> GetUserBonusByEmployeeIdentifier(List<string> EmployeeIdentifier)
        {
            try
            {
                List<UserBonus> Bonus = new List<UserBonus>();
                foreach (var item in EmployeeIdentifier)
                {
                    var Sql = "Select c.EmployeeIdentifier , SUM(c.one_time_bonus) " +
                        "as one_time_bonus from CompensationSalaries c " +
                        "Group by c.EmployeeIdentifier Having " +
                        "c.EmployeeIdentifier = @EmployeeIdentifier";
                    Bonus.Add(await db.QueryFirstAsync(Sql, new { @EmployeeIdentifier = EmployeeIdentifier }).ConfigureAwait(false));

                }
                return Bonus;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<UserBonus>> GetUserBonusByOrganizationIdentifiers(string OrganizationIdentifiers)
        {
            try
            {
                var Sql = "Select c.EmployeeIdentifier , SUM(c.one_time_bonus) " +
                    "as one_time_bonus from CompensationSalaries c " +
                    "Group by c.EmployeeIdentifier Having " +
                    "c.organizationIdentifier = @OrganizationIdentifier";
                return await db.QueryAsync<UserBonus>(Sql, new { @OrganizationIdentifiers = OrganizationIdentifiers }).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
