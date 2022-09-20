using Compensation.API.Commands.UpdatePerformanceBonus;
using Compensation.API.Dtos;
using Compensation.API.Queries.GetEmployeePerformanceBonus;
using Compensation.API.Queries.GetOrgnaizationBonus;
using Compensation.API.Queries.GetTeamBonuses;
using Compensation.API.Queries.GetUserBonuses;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class BonusController : ApiControllerBase
    {
        private readonly ILogService _logService;
        public BonusController(ILogService logService)
        {
            _logService = logService;
        }
        //[HttpGet]
        //[Route("user")]
        //public async Task<ActionResult<ResponseMessage>> Get(string EmployeeIdentifier)
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(new GetUserBonusesQuery { employeeIdentifier = EmployeeIdentifier });
        //        return new ResponseMessage(true, EResponse.OK, null, data);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
        //    }
        //}
        [HttpGet]
        public async Task<ActionResult<List<UserBonus>>> Get([FromQuery] string[] employeeIdentifiers)
        {
            try
            {
                return await Mediator.Send(new GetTeamBonusesQuery { employeeIdentifiers = employeeIdentifiers });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Bonus_{CurrentUser.EmployeeIdentifier}");
                throw;
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

        //[HttpGet]
        //[Route("GetEmployeePerformanceBonus")]
        //public async Task<double> GetEmployeePerformanceBonus()
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(new GetEmployeePerformanceBonusQuery { businessIdentifier = CurrentUser.BusinessIdentifier, grade = CurrentUser.Grade, jobFunction = Enum.GetName(CurrentUser.JobFunction), employeeIdentifier = CurrentUser.EmployeeIdentifier});
        //        return data;
        //    }
        //    catch (Exception e)
        //    {
        //        await _logService.WriteLogAsync(e, $"Bonus_{CurrentUser.EmployeeIdentifier}");
        //        throw;
        //    }
        //}

        [HttpPost]
        [Route("UpdateBonusFromPerformanceBonus")]
        public async Task<bool> UpdateBonusFromPerformanceBonus(List<UpdateBonusFromPerformanceBonusRequest> command)
        {
            try
            {
                var data = await Mediator.Send(new UpdatePerformanceBonusCommand { bonusFromPerformanceBonusRequests = command });
                return true;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Bonus_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
    }
}
