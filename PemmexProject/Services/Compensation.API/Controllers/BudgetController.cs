using Compensation.API.Commands.CreateBudgetCommand;
using Compensation.API.Commands.SaveFunctionalBudget;
using Compensation.API.Commands.UpdateFunctionalBudget;
using Compensation.API.Database.Entities;
using Compensation.API.Dtos;
using Compensation.API.Queries.GetFunctionalBudget;
using Compensation.API.Queries.GetFunctionalBudgetByManagerId;
using Compensation.API.Queries.GetFunctionalBudgetOrganziation;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BudgetController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public BudgetController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> Post(CreateBudgetCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Budget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        public async Task<ActionResult<List<OrganizationBudgetDto>>> GetOrganizationalBudget(string organizationIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetOrganizationalBudgetQuery() { organizationIdentifier = organizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Budget_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("Team")]
        public async Task<ActionResult<List<FunctionalBudgetDto>>> GetFunctionalBudgetByTeams([FromBody]string[] employees)
        {
            try
            {
                return await Mediator.Send(new GetFunctionalBudgetTeamQuery() { employees = employees});
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Budget_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("Organziation")]
        public async Task<ActionResult<List<FunctionalBudgetDto>>> GetFunctionalBudgetByOrganziation(string organizationIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetFunctionalBudgetOrganziationQuery() { organizationIdentifier = organizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Budget_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }

        //Will be use by Group HR to update Single Functional budget of any employees
        [Authorize("GroupHR")]
        [HttpPost]
        [Route("SaveFunctionalBudget")]
        public async Task<ActionResult<ResponseMessage>> SaveFunctionalBudget(string organizationIdentifier,
            DateTime EffectiveDateTime)
        {
            try
            {
                var data = await Mediator.Send(new SaveFunctionalBudgetCommand() { EffectiveDateTime = EffectiveDateTime, organizationIdentifier = organizationIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Budget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        //Will be use by Group HR to assign whole budget to employees finally
        [Authorize("GroupHR")]
        [HttpPost]
        [Route("UpdateFunctionalBudget")]
        public async Task<ActionResult<ResponseMessage>> UpdateFunctionalBudgetCommand(UpdateFunctionalBudgetCommand _command)
        {
            try
            {
                var data = await Mediator.Send(_command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Budget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
    }
}
