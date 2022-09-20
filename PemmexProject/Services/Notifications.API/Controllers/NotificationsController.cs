using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notifications.API.Commands.AnnounceNotification;
using Notifications.API.Commands.MarkNotification;
using Notifications.API.Commands.SaveNotification;
using Notifications.API.NotificationHub;
using Notifications.API.Queries.GetAllNotifications;
using Notifications.API.Queries.GetUnReadNotificationsCount;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Threading.Tasks;

namespace Notifications.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Microsoft.AspNetCore.Authorization.Authorize]
    //[Microsoft.AspNetCore.Authorization.Authorize]
    //[Authorize]
    public class NotificationsController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public NotificationsController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpPost]
        public async Task<ActionResult<string>> PostAsync(SaveNotificationCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return "Notification send..";
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Notifications_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }

        [HttpPost]
        [Route("AnnounceNotification")]
        public async Task<ActionResult<string>> AnnounceNotification(AnnounceNotificationCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return "Notifications send..";
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Notifications_{CurrentUser.EmployeeIdentifier}");
                throw e;
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
                await _logService.WriteLogAsync(e, $"Notifications_{CurrentUser.EmployeeIdentifier}");
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
                await _logService.WriteLogAsync(e, $"Notifications_{CurrentUser.EmployeeIdentifier}");
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
                await _logService.WriteLogAsync(e, $"Notifications_{CurrentUser.EmployeeIdentifier}");
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
    }
}
