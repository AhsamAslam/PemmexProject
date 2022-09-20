using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Threading.Tasks;
using TaskManager.API.Commands.MarkNotification;
using TaskManager.API.Commands.SaveNotification;
using TaskManager.API.Queries.GetAllNotifications;
using TaskManager.API.Queries.GetUnReadNotificationsCount;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class TaskNotificationController : ApiControllerBase
    {
        [HttpGet]
        [Route("Notifications")]
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
