using Compensation.API.Commands.SaveCompensation;
using Compensation.API.Dtos;
using Compensation.API.Filters;
using Compensation.API.Queries.GetCompensation;
using Compensation.API.Queries.GetEmployeesSalaryDetails;
using Compensation.API.Queries.GetOrganizationCompensations;
using Compensation.API.Queries.GetOrganizationTotalSalaryCount;
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
    public class CompensationController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public CompensationController(ILogService logService)
        {
            _logService = logService;
        }
        [ServiceFilter(typeof(AuthorizationIdentifierFilter))]
        [HttpGet]
        public async Task<ActionResult<List<CompensationDto>>> Get([FromQuery] string[] Identifiers)
        {
            try
            {
                return await Mediator.Send(new GetCompensationQuery() { Identifiers = Identifiers });
                //return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Compensation_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("GetEmployeeTotalMonthlySalary")]
        public async Task<ActionResult<List<CompensationTotalMonthlyPayDto>>> GetEmployeeTotalMonthlySalary([FromQuery] string[] Identifiers, DateTime startDate, DateTime endDate)
        {
            try
            {
                return await Mediator.Send(new GetEmployeesSalaryDetailsQuery() { Identifiers = Identifiers, startDate = startDate, endDate = endDate });
                //return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Compensation_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("OrganizationCompensations")]
        public async Task<ActionResult<List<CompensationDto>>> OrganizationCompensations(string organizationIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetOrganizationCompensationsQuery { organizationIdentifier = organizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Compensation_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
    }
}
