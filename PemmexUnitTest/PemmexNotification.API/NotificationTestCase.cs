//using Microsoft.AspNet.SignalR;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.SignalR;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Notifications.API.Commands.SaveNotification;
//using Notifications.API.Database.Repositories.Interface;
//using Notifications.API.Database.Repositories.Repository;
//using PemmexCommonLibs.Application.Helpers;
//using PemmexCommonLibs.Application.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace PemmexUnitTest.PemmexNotification.API
//{
//    [TestClass]
//    public class NotificationTestCase
//    {
//        private readonly IHubContext<NotificationUserHubRepository> _notificationUserHubContext;
//        private readonly IUserConnectionManager _userConnectionManager;
//        private readonly INotification _notificationRepository;
//        private readonly ILogService _logService;
//        Notifications.API.Controllers.Notifications notifications;
//        public NotificationTestCase()
//        {
//            notifications = new Notifications.API.Controllers.Notifications(
//                _notificationUserHubContext, _userConnectionManager, _notificationRepository,
//                _logService);
//        }

//        #region PostAsync Test Case
//        [TestMethod]
//        public void PostAsync_Test1()
//        {
//            SaveNotificationCommand saveNotificationCommand = new SaveNotificationCommand();
//            saveNotificationCommand.userId = "1122";
//            saveNotificationCommand.isRead = true;
//            saveNotificationCommand.taskType = PemmexCommonLibs.Domain.Enums.TaskType.Holiday;
//            saveNotificationCommand.description = "abcd";
//            saveNotificationCommand.title = "abc";

//            Task<ActionResult<ResponseMessage>> result = notifications.PostAsync(saveNotificationCommand) as Task<ActionResult<ResponseMessage>>;

//            Assert.IsNotNull(result);
//        }
//        #endregion

//        #region GetAllNotifications Test Case
//        [TestMethod]
//        public void GetAllNotifications_Test1()
//        {

//            Task<ActionResult<ResponseMessage>> result = notifications.GetAllNotifications() as Task<ActionResult<ResponseMessage>>;

//            Assert.IsNotNull(result);
//        }
//        #endregion

//        #region UnReadNotificationsCount Test Case
//        [TestMethod]
//        public void UnReadNotificationsCount_Test1()
//        {

//            Task<ActionResult<ResponseMessage>> result = notifications.UnReadNotificationsCount() as Task<ActionResult<ResponseMessage>>;

//            Assert.IsNotNull(result);
//        }
//        #endregion

//        #region MarkAsRead Test Case
//        [TestMethod]
//        public void MarkAsRead_Test1()
//        {

//            Task<ActionResult<ResponseMessage>> result = notifications.MarkAsRead() as Task<ActionResult<ResponseMessage>>;

//            Assert.IsNotNull(result);
//        }
//        #endregion
//    }
//}
