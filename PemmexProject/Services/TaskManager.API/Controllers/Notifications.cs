using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Commands.MarkNotification;
using TaskManager.API.Queries.GetAllNotifications;
using TaskManager.API.Queries.GetUnReadNotificationsCount;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Notifications : ApiControllerBase
    {
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
