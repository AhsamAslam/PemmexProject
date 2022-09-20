using Holidays.API.Dtos;
using Holidays.API.Queries.GetEmployeeWithMaternityLeaves;
using Holidays.API.Queries.GetEmployeeWithSickLeaves;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HolidayReport : ApiControllerBase
    {
        private readonly ILogService _logService;

        public HolidayReport(ILogService logService)
        {
            _logService = logService;
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("EmployeeWithSickLeaves")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> EmployeeWithSickLeaves(EmployeeWithSickLeavesQuery query)
        {
            try
            {
                query.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                return await Mediator.Send(query);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("EmployeeWithMaternityLeaves")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> EmployeeWithMaternityLeaves(GetEmployeeWithMaternityLeavesQuery query)
        {
            try
            {
                query.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                return await Mediator.Send(query);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
    }
}
