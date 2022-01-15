using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Database.Repositories.Interface;

namespace TaskManager.API.Database.Repositories.Repository
{
    public class BonusSettingsRepository: IBonusSettings
    {
        #region
        private IDbConnection db;
        #endregion
        public BonusSettingsRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("TaskManagerConnection"));
        }

        public async Task AddBonusSettings(BonusSettings BonusSettings)
        {
            try
            {
                var Sql = "INSERT INTO BonusSettings(businessIdentifier, organizationIdentifier, " +
                    "limit_percentage, Created, CreatedBy) VALUES " +
                    "(@businessIdentifier, @organizationIdentifier, " +
                    "@limit_percentage, GETDATE(), 'Test')";
                await db.ExecuteAsync(Sql, BonusSettings).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteBonusSettings(BonusSettings BonusSettings)
        {
            try
            {
                var Sql = "Delete from BonusSettings where BonusSettingsId = @BonusSettingsId";
                await db.ExecuteAsync(Sql, BonusSettings);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<BonusSettings> GetBonusSettingsByBusinessIdentifier(string businessIdentifier)
        {
            try
            {
                var Sql = "Select top(1) * from BonusSettings with (nolock) where businessIdentifier = @businessIdentifier";
                return await db.QueryFirstAsync<BonusSettings>(Sql, new { @businessIdentifier = businessIdentifier }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateBonusSettings(BonusSettings BonusSettings)
        {
            try
            {
                var Sql = "UPDATE BonusSettings SET businessIdentifier = @businessIdentifier," +
                    "organizationIdentifier = @organizationIdentifier," +
                    "limit_percentage = @limit_percentage,LastModified = GETDATE()," +
                    "LastModifiedBy = 'Test' WHERE BonusSettingsId = @BonusSettingsId";
                await db.ExecuteAsync(Sql, BonusSettings).ConfigureAwait(false);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
