using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organization.API.Commands.CreateEmployee;
using Organization.API.Commands.DeleteEmployee;
using Organization.API.Commands.UpdateEmployee;
using Organization.API.Dtos;
using Organization.API.Queries.GetAllBusinessEmployees;
using Organization.API.Queries.GetEmployee;
using Organization.API.Queries.GetEmployeeByIdentifier;
using Organization.API.Queries.GetEmployeeByIdentifiers;
using Organization.API.Queries.GetOrganization;
using Organization.API.Queries.GetOrganizationEmployees;
using Organization.API.Queries.GetTeamMembers;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<EmployeeResponse>>> GetAsync([FromQuery] string[] Identifiers)
        {
            try
            {
                return await Mediator.Send(new GetEmployeeByIdentifiersQuery { Identifiers = Identifiers });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("ManagerTree/{id}")]
        public async Task<ActionResult<List<EmployeeResponse>>> ManagerTree(string id)
        {
            try
            {
                return await Mediator.Send(new GetManagerTree { Id = id });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("TeamMembers/{costCenterIdentifier}")]
        public async Task<ActionResult<List<EmployeeResponse>>> TeamMembers(string costCenterIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetTeamMembersQuery { costCenterIdentifier = costCenterIdentifier });
            }
            catch (Exception e)
            {
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
            catch (Exception)
            {
                throw;
            }
        }
        //[HttpGet]
        //[Route("{identifier}")]
        //public async Task<ActionResult<EmployeeResponse>> GetAsync(string identifier)
        //{
        //    try
        //    {
        //        return await Mediator.Send(new GetEmployeeByIdentifierQuery { Id = id });
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(EmployeeRequest employee)
        {
            try
            {

                return await Mediator.Send(new CreateEmployeeCommand { employee = employee });
                
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
