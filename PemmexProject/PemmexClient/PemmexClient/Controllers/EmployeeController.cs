using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PemmexClient.Interfaces;
using PemmexClient.Models;
using PemmexCommonLibs.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> GetAllEmployee(int organizationId)
        {
            return await _employeeService.GetAllEmployee(organizationId);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> GetEmployee(int id)
        {
            return await _employeeService.GetEmployee(id);

        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> CreateEmployee(Employee employee)
        {
            return await _employeeService.CreateEmployee(employee);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> UpdateEmployee(Employee employee)
        {
            return await _employeeService.UpdateEmployee(employee);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> GetManagers(int organizationId)
        {
            return await _employeeService.GetManagers(organizationId);

        }
    }
}
