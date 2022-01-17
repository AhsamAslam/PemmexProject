using Authentication.API.Commands.ChangePassword;
using Authentication.API.Commands.DeactivateUser;
using Authentication.API.Commands.SaveRole;
using Authentication.API.Dtos;
using Authentication.API.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public UserController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpGet]
        [Route("roles")]
        public ActionResult<ResponseMessage> GetAsync()
        {
            try
            {
                var data = DefaultDataService.GetDefaultRolesAndScreen(CurrentUser.OrganizationIdentifier);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                _logService.WriteLogAsync(e, $"User_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpGet]
        [Route("allusers")]
        public async Task<ActionResult<ResponseMessage>> GetAllUsersAsync()
        {
            try
            {
                var data = await Mediator.Send(new GetAllUsersQuery { Identifier = "hjhj" });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"User_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        [Route("role")]
        public async Task<ActionResult<ResponseMessage>> PostAsync(RoleDto role)
        {
            try
            {
                var data = await Mediator.Send(new SaveRoleCommand { role = role });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"User_{CurrentUser.EmployeeIdentifier}");

                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpDelete]
        [Route("deactivate/{id}")]
        public async Task<ActionResult<ResponseMessage>> DeactivateAsync(Guid id)
        {
            try
            {
                var data = await Mediator.Send(new DeactivateUserCommand { Id = id });
                return new ResponseMessage(true, EResponse.OK, AppConstants._employeeDeleted, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"User_{CurrentUser.EmployeeIdentifier}");

                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        
    }
}
