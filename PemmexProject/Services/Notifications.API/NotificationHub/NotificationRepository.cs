using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Notifications.API.Database.context;
using Notifications.API.Dtos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace Notifications.API.NotificationHub
{
    public class NotificationRepository :INotificationRepository
    {
        #region
        private readonly IApplicationDbContext _context;
        private IDbConnection db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        string CurrentUser;
        #endregion
        public NotificationRepository(IApplicationDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this.db = new SqlConnection(configuration.GetConnectionString("NotificationConnection"));
            _httpContextAccessor = httpContextAccessor;
            CurrentUser = Convert.ToString(_httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "sub").Value);

        }

        public async Task<int> AddNotifications(Database.Entities.Notifications notifications)
        {
            try
            {
                await _context.Notifications.AddAsync(notifications);
                await _context.SaveChangesAsync();
                return notifications.notificationId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> AddAnnounceNotification(List<AnnounceNotificationDto> notification)
        {
            try
            {
                var AnnNotifications = @"INSERT INTO Notifications (EmployeeId,title,isRead,tasks,description,Created,CreatedBy) VALUES (@EmployeeId, @title, @isRead, @tasks, @description, GETDATE(),'" + CurrentUser + "')";
                await db.ExecuteAsync(AnnNotifications, notification).ConfigureAwait(false);

                return 1;
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
                var n = await _context.Notifications
                    .Where(n => n.EmployeeId == EmployeeId && n.isRead == false)
                    .ToListAsync();
                return n.Count();
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
                return await _context.Notifications.Where(n => n.EmployeeId == EmployeeId)
                    .ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveNotification(Database.Entities.Notifications notifications, CancellationToken cancellationToken)
        {
            try
            {
                _context.Notifications.Add(notifications);
                await _context.SaveChangesAsync(cancellationToken);
                return notifications.notificationId;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateNotificationToRead(string Id)
        {
            try
            {
                var n = await _context.Notifications
                    .Where(n => n.EmployeeId == Id).ToListAsync();
                n.ForEach(n => n.isRead = true);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
