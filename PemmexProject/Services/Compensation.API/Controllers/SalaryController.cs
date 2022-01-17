using Compensation.API.Commands.SaveSalary;
using Compensation.API.Dtos;
using Compensation.API.Queries.GetBusinessSalaries;
using Compensation.API.Queries.GetFunctionalSalaryCount;
using Compensation.API.Queries.GetOrganizationTotalSalaryCount;
using Compensation.API.Queries.GetSalary;
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
        [Route("BusinessSalaries")]
        public async Task<ActionResult<List<CompensationDto>>> BusinessSalaries(string BusinessIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetBusinessSalariesQuery { businessIdentifier = BusinessIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");

                throw;
            }
        }
        [HttpGet]
        [Route("TotalSalary")]
        public async Task<ActionResult<ResponseMessage>> OrganizationTotalSalaryCount(string organizationIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetOrganizationTotalSalaryCountQuery { organizationIdentifier = organizationIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");

                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpGet]
        [Route("FunctionalSalary")]
        public async Task<ActionResult<ResponseMessage>> GetFunctionalSalaryCount([FromQuery] string[] EmployeeIdentifiers)
        {
            try
            {
                var data = await Mediator.Send(new GetFunctionalSalaryCountQuery { employeeIdentifiers = EmployeeIdentifiers });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Salary_{CurrentUser.EmployeeIdentifier}");

                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
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
