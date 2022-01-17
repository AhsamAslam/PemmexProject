using Compensation.API.Commands.CreateBudgetCommand;
using Compensation.API.Queries.GetFunctionalBudget;
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
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> Get(string organizationIdentifier,DateTime budgetDate)
        {
            try
            {
                var data = await Mediator.Send(new GetFunctionalBudgetQuery() { organizationIdentifier = organizationIdentifier,budgetDate = budgetDate});
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
