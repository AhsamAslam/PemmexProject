
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Notifications.API.Database.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.API.Database.Repositories.Repository
{
    public class NotificationRepository:INotification
    {
        #region
        private IDbConnection db;
        #endregion
        public NotificationRepository(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("HolidaysConnection"));
        }
        public async Task<int> AddNotifications(Database.Entities.Notifications notifications)
        {
            try
            {
                var Sql = "INSERT INTO Notifications " +
                    "(EmployeeId,title,isRead,tasks,description,Created,CreatedBy) " +
                    "VALUES (@EmployeeId,@title,@isRead,@tasks,@description,GETDATE(), 'test')";
                await db.ExecuteAsync(Sql, notifications).ConfigureAwait(false);
                return notifications.notificationId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> CountUnReadNotifications(string EmployeeId)
        {
            try
            {
                var sql = "Select * from Notifications where EmployeeId = @EmployeeId and isnull(isRead,0)=0";
                var count =  await db.QueryAsync<Database.Entities.Notifications>(sql, new { @EmployeeId = EmployeeId }).ConfigureAwait(false);
                return count.Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Database.Entities.Notifications>> GetAllNotification(string EmployeeId)
        {
            try
            {
                var sql = "Select * from Notifications where EmployeeId = @EmployeeId";
                var notificationList =  await db.QueryAsync<Database.Entities.Notifications>(sql, new { @EmployeeId = EmployeeId }).ConfigureAwait(false);
                return notificationList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveNotification(Database.Entities.Notifications notifications)
        {
            //try
            //{
            //    _context.Notifications.Add(notifications);
            //    await _context.SaveChangesAsync(cancellationToken);
            //    return notifications.notificationId;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return 0;
        }

        public async Task UpdateNotificationToRead(string Id)
        {
            try
            {
                var sql = "Select * from Notifications where EmployeeId = @EmployeeId";
                var notificationList = await db.QueryAsync<Database.Entities.Notifications>(sql, new { @EmployeeId = Id }).ConfigureAwait(false);
                foreach (var item in notificationList)
                {
                    item.isRead = true;
                    var UpdateSql = "UPDATE dbo.Notifications SET " +
                        "title = @title,isRead = " +
                        "@isRead,tasks = @tasks,description = @description," +
                        "LastModified = GETDATE() ,LastModifiedBy = 'Test' " +
                        "WHERE EmployeeId = @Id";
                    await db.ExecuteAsync(UpdateSql, item).ConfigureAwait(false);

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
