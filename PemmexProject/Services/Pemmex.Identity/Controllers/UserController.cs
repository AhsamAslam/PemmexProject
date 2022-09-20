using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pemmex.Identity.Commands.AddUserToBusinessUnit;
using Pemmex.Identity.Commands.UpdateRole;
using Pemmex.Identity.Queries.GetAllUsers;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Pemmex.Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    //[ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    public class UserController:ApiControllerBase
    {

        [HttpGet]
        [Route("roles")]
        public async Task<ActionResult<ResponseMessage>> GetRolesAsync(string organizationIdentifier)
        {
            try
            {
                var data = DefaultDataService.GetDefaultRolesAndScreen(organizationIdentifier);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpGet]
        [Route("allusers")]
        public async Task<ActionResult<ResponseMessage>> GetAllUsersAsync(string organizationIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetAllUsersQuery { Identifier = organizationIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        [Route("updaterole")]
        public async Task<ActionResult<ResponseMessage>> UpdateRoleAsync([FromBody] UpdateRoleCommand updateRoleCommand)
        {
            try
            {
                var data = await Mediator.Send(updateRoleCommand);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        [Route("AddUserToBusinessUnit")]
        public async Task<ActionResult<ResponseMessage>> AddUserToBusinessUnit(AddUserToBusinessUnitCommand _command)
        {
            try
            {
                var data = await Mediator.Send(_command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
    }
}
