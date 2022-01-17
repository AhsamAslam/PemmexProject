using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Commands.TaskRequest;
using TaskManager.API.Commands.UpdateTask;
using TaskManager.API.Dtos;
using TaskManager.API.Queries.GetCurrentTasksByManagerId;
using TaskManager.API.Queries.GetHistoryTasksByManagerId;
using TaskManager.API.Queries.GetPendingTasksByManagerId;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TaskManager : ApiControllerBase
    {
        private readonly ILogService _logService;
        public TaskManager(ILogService logService)
        {
            _logService = logService;
        }
        [HttpPost]
        [Route("InitiateTask")]
        public async Task<ActionResult<ResponseMessage>> InitiateTask(TaskRequest task)
        {
            try
            {
                task.UserId = CurrentUser.Id;
                task.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                task.RequestedByIdentifier = CurrentUser.EmployeeIdentifier;
                task.ManagerIdentifier = CurrentUser.ManagerIdentifier;
                task.businessIdentifier = CurrentUser.BusinessIdentifier;
                var data = await Mediator.Send(task);
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"TaskManager_{CurrentUser.EmployeeIdentifier}");
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        [HttpPost]
        [Route("UpdateTasks")]
        public async Task<ActionResult<ResponseMessage>> UpdateTasks(UpdateTask task)
        {
            try
            {
                task.userId = CurrentUser.Id;
                task.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                task.managerIdentifier = CurrentUser.ManagerIdentifier;
                task.requestIdentifier = CurrentUser.EmployeeIdentifier;
                task.costcenterIdentifier = CurrentUser.CostCenterIdentifier;
                task.businessIdentifier = CurrentUser.BusinessIdentifier;
                var token = Request.Headers[HeaderNames.Authorization].ToString2().Split(new string[] { "Bearer" }, StringSplitOptions.None);
                if (token.Length > 1)
                {
                    task.token = token[1].Trim();
                }
                else
                {
                    throw new Exception("User is un Authenticated");
                }

                var data = await Mediator.Send(task);
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, null));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"TaskManager_{CurrentUser.EmployeeIdentifier}");
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        [HttpGet]
        [Route("PendingTasks")]
        public async Task<ActionResult<ResponseMessage>> PendingTasks()
        {
            try
            {
                var data = await Mediator.Send(new GetPendingTasksQuery { Id = CurrentUser.EmployeeIdentifier });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"TaskManager_{CurrentUser.EmployeeIdentifier}");

                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        [HttpGet]
        [Route("ApprovedTasks")]
        public async Task<ActionResult<ResponseMessage>> ApprovedTasks()
        {
            try
            {
                var data = await Mediator.Send(new GetApprovedTasksQuery { Id = CurrentUser.EmployeeIdentifier });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"TaskManager_{CurrentUser.EmployeeIdentifier}");

                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        [HttpGet]
        [Route("NotApprovedTasks")]
        public async Task<ActionResult<ResponseMessage>> NotApprovedTasks()
        {
            try
            {
                var data = await Mediator.Send(new GetNotApprovedTasksQuery { Id = CurrentUser.EmployeeIdentifier });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"TaskManager_{CurrentUser.EmployeeIdentifier}");

                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
    }
}
