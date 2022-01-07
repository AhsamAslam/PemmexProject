using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.API.Commands.MarkNotification;
using Notifications.API.Commands.SaveNotification;
using Notifications.API.NotificationHub;
using Notifications.API.Queries.GetAllNotifications;
using Notifications.API.Queries.GetUnReadNotificationsCount;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Notifications.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class Notifications : ApiControllerBase
    {
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly INotificationRepository _notificationRepository;

        public Notifications(IHubContext<NotificationUserHub> notificationUserHubContext,
            IUserConnectionManager userConnectionManager,
            INotificationRepository notificationRepository)
        {
            _notificationUserHubContext = notificationUserHubContext;
            _userConnectionManager = userConnectionManager;
            _notificationRepository = notificationRepository;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> PostAsync(SaveNotificationCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        [HttpGet]
        [Route("GetAllNotifications")]
        public async Task<ActionResult<ResponseMessage>> GetAllNotifications()
        {
            try
            {
                var data = await Mediator.Send(new GetAllNotificationsQuery { Id = CurrentUser.EmployeeIdentifier });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        [HttpGet]
        [Route("UnReadNotificationsCount")]
        public async Task<ActionResult<ResponseMessage>> UnReadNotificationsCount()
        {
            try
            {
                var data = await Mediator.Send(new GetUnReadNotificationsCountQuery { Id = CurrentUser.EmployeeIdentifier });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        [HttpPut]
        [Route("MarkAsRead")]
        public async Task<ActionResult<ResponseMessage>> MarkAsRead()
        {
            try
            {
                await Mediator.Send(new MarkNotificationCommand { userId = CurrentUser.EmployeeIdentifier });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, null));
            }
            catch (Exception e)
            {
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
    }
}
