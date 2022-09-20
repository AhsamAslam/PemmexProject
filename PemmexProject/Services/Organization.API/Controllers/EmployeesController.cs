using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organization.API.Commands.CreateEmployee;
using Organization.API.Commands.DeleteEmployee;
using Organization.API.Commands.UpdateEmployee;
using Organization.API.Dtos;
using Organization.API.Queries.GetEmployee;
using Organization.API.Queries.GetOrganization;
using Organization.API.Queries.GetSiblings;
using Organization.API.Queries.GetTeamMembers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Organization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public EmployeesController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpGet]
        [Route("TeamMembers/{id}")]
        public async Task<ActionResult<List<EmployeeResponse>>> TeamMembers(string id)
        {
            try
            {
                return await Mediator.Send(new TeamMemberHeirarchy { Id = id });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Employees_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("Siblings/{employeeIdentifier}")]
        public async Task<ActionResult<List<EmployeeResponse>>> Siblings(string employeeIdentifier)
        {
            try
            {
                var p = await Mediator.Send(new GetSiblingsQuery { employeeIdentifier = employeeIdentifier });
                return p;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Employees_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<EmployeeResponse>> GetAsync(string id)
        {
            try
            {
                return await Mediator.Send(new GetEmployeeQuery { Id = id });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Employees_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(EmployeeRequest employee)
        {
            try
            {

                return await Mediator.Send(new CreateEmployeeCommand { employee = employee });
                
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Employees_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<int>> PutAsync(EmployeeRequest employee, string id)
        {
            try
            {
                return await Mediator.Send(new UpdateEmployeeCommand { employee = employee, Id = id });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Employees_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<int>> DeleteAsync(Guid id)
        {
            try
            {
                return await Mediator.Send(new DeleteEmployeeCommand { Id = id });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Employees_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
    }
}
