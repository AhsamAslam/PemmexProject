using Compensation.API.Queries.GetOrgnaizationBonus;
using Compensation.API.Queries.GetTeamBonuses;
using Compensation.API.Queries.GetUserBonuses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class BonusController : ApiControllerBase
    {
        private readonly ILogService _logService;
        public BonusController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<ResponseMessage>> Get(string EmployeeIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetUserBonusesQuery { employeeIdentifier = EmployeeIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Bonus_{CurrentUser.EmployeeIdentifier}");

                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpGet]
        [Route("team")]
        public async Task<ActionResult<ResponseMessage>> TeamBonuses([System.Web.Http.FromUri] List<string> EmployeeIdentifiers)
        {
            try
            {
                var data = await Mediator.Send(new GetTeamBonusesQuery { employeeIdentifiers = EmployeeIdentifiers });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Bonus_{CurrentUser.EmployeeIdentifier}");

                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpGet]
        [Route("orgnaization")]
        public async Task<ActionResult<ResponseMessage>> OrgnaizationBonuses(string orgnaizationIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetOrgnaizationBonusQuery { organizationIdentifiers = orgnaizationIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Bonus_{CurrentUser.EmployeeIdentifier}");

                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
    }
}
