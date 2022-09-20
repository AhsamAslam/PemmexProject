using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexAPIAggregator.Filters;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompensationController : APIControllerBase
    {
        private readonly ICompensationService _compensationService;
        private readonly IOrganizationService _organizationService;
        public CompensationController(ICompensationService compensationService,
            IOrganizationService organizationService)
        {
            _compensationService = compensationService;
            _organizationService = organizationService;
        }
        [HttpGet]
        [Route("EmployeeBonus")]
        public async Task<ActionResult<List<EmployeeBonus>>> GetEmployeeBonus()
        {
            try
            {
                List<string> identifiers = new List<string>();
                identifiers.Add(CurrentUser.EmployeeIdentifier);
                var employeeBonuses = new List<EmployeeBonus>();
                var employee = await _organizationService.GetEmployee(CurrentUser.EmployeeIdentifier);
                var bonuses = await _compensationService.GetBonus(identifiers.ToArray());
                foreach (var b in bonuses)
                {
                    var employeeBonus = new EmployeeBonus();
                    employeeBonus.bonusAmount = b.bonusAmount;
                    employeeBonus.bonusDateTime = b.bonusDateTime;
                    employeeBonus.EmployeeIdentifier = b.EmployeeIdentifier;
                    employeeBonus.FirstName = employee.FirstName;
                    employeeBonus.LastName = employee.LastName;
                    employeeBonus.ManagerIdentifier = employee.ManagerIdentifier;
                    employeeBonus.ManagerName = employee.ManagerName;
                    employeeBonus.Grade = employee.Grade;
                    employeeBonus.Title = employee.Title;
                    employeeBonus.BusinessIdentifier = employee.BusinessIdentifier;
                    employeeBonuses.Add(employeeBonus);
                }
                return employeeBonuses;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("TeamBonuses")]
        public async Task<ActionResult<List<EmployeeBonus>>> GetTeamBonuses(string employeeIdentifier)
        {
            try
            {
                var employeeBonuses = new List<EmployeeBonus>();
                var employees = await _organizationService.GetTeamMembers(employeeIdentifier);
                List<string> emp = new List<string>();
                foreach(var e in employees)
                {
                    emp.Add(e.EmployeeIdentifier);
                }
                var bonuses = await _compensationService.GetBonus(emp.ToArray());
                foreach (var b in bonuses)
                {
                    var employee = employees
                        .FirstOrDefault
                        (e => e.EmployeeIdentifier == b.EmployeeIdentifier);
                    if(employee != null)
                    {
                        var employeeBonus = new EmployeeBonus();
                        employeeBonus.bonusAmount = b.bonusAmount;
                        employeeBonus.bonusDateTime = b.bonusDateTime;
                        employeeBonus.EmployeeIdentifier = b.EmployeeIdentifier;
                        employeeBonus.FirstName = employee.FirstName;
                        employeeBonus.LastName = employee.LastName;
                        employeeBonus.ManagerIdentifier = employee.ManagerIdentifier;
                        employeeBonus.ManagerName = employee.ManagerName;
                        employeeBonus.Grade = employee.Grade;
                        employeeBonus.Title = employee.Title;
                        employeeBonus.BusinessIdentifier = employee.BusinessIdentifier;
                        employeeBonuses.Add(employeeBonus);
                    }
                    
                }
                return employeeBonuses;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
