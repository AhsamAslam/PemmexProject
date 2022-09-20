using Compensation.API.Commands.SaveSalary;
using Compensation.API.Dtos;
using Compensation.API.Queries.GetFunctionalBudgetCount;
using Compensation.API.Queries.GetFunctionalSalaryCount;
using Compensation.API.Queries.GetOrganizationSalaries;
using Compensation.API.Queries.GetOrganizationTotalSalaryCount;
using Compensation.API.Queries.GetSalary;
using Compensation.API.Queries.OrganizationTotalSalaryCountByDate;
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
    //[Authorize("ClientIdPolicy")]
    [Authorize]
    public class SalaryController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public SalaryController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> GetAsync(string EmployeeIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetSalaryQuery { employeeIdentifier = EmployeeIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> Post([FromBody]string EmployeeIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new SaveSalaryCommand() { EmployeeIdentifier = EmployeeIdentifier});
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpGet]
        [Route("OrganizationSalaries")]
        public async Task<ActionResult<List<CompensationSalariesDto>>> OrganizationSalaries(string organizationIdentifier,DateTime startDate,DateTime endDate)
        {
            try
            {
                return await Mediator.Send(new GetOrganizationSalariesQuery { organizationIdentifier = organizationIdentifier,startDate = startDate,endDate = endDate });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("OrganizationTotalSalaryCount")]
        public async Task<ActionResult<ResponseMessage>> OrganizationTotalSalaryCount()
        {
            try
            {
                var data = await Mediator.Send(new GetOrganizationTotalSalaryCountQuery { organizationIdentifier = CurrentUser.OrganizationIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [Authorize("GroupHR")]
        [HttpGet]
        [Route("OrganizationTotalSalaryCountByDate")]
        public async Task<ActionResult<ResponseMessage>> OrganizationTotalSalaryCountByDate(DateTime startDate, DateTime endDate)
        {
            try
            {
                var data = await Mediator.Send(new OrganizationTotalSalaryCountByDateQuery { organizationIdentifier = CurrentUser.OrganizationIdentifier,startDate = startDate, endDate = endDate });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [Authorize("Manager")]
        [HttpGet]
        [Route("FunctionalSalaryCount")]
        public async Task<ActionResult<double>> GetFunctionalSalaryCount([FromQuery] string[] Identifiers)
        {
            try
            {
                return await Mediator.Send(new GetFunctionalSalaryCountQuery() { employeeIdentifiers = Identifiers});
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("FunctionalBudgetCount")]
        public async Task<ActionResult<List<OrganizationBudgetDto>>> GetFunctionalBudgetCount(GetFunctionalBudgetCountQuery request)
        {
            try
            {
                return await Mediator.Send(request);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }

        //[HttpPost]
        //[Route("Bonus")]
        //public async Task<ActionResult<ResponseMessage>> SaveBonus(SaveSalaryBonusCommand bonus)
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(bonus);
        //        return new ResponseMessage(true, EResponse.OK, null, data);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
        //    }
        //}
    }
}
