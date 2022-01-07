using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexAPIAggregator.Helpers;
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
    public class AnnualSalaryPlanningController : APIControllerBase
    {
        private readonly IAnnualSalaryPlanning _annualSalaryPlanning;
        private readonly IOrganizationService _organizationService;
        public AnnualSalaryPlanningController(IAnnualSalaryPlanning annualSalaryPlanning, IOrganizationService organizationService)
        {
            _annualSalaryPlanning = annualSalaryPlanning;
            _organizationService = organizationService;
        }
        [HttpGet]
        [Route("OrganizationJobTypeBasedSalarySummary")]
        public async Task<ActionResult<List<FunctionalBudgetSummary>>> OrganizationJobTypeBasedSalarySummary(string organizationIdentifier)
        {
            try
            {
                var businesses = await _organizationService.GetBusinesses(organizationIdentifier);
                List<FunctionalBudgetSummary> functionalBudgetSummaries = new List<FunctionalBudgetSummary>();
                foreach(var b  in businesses)
                {
                    var salaries = await _annualSalaryPlanning.BusinessJobTypeBasedSalaries(b.BusinessIdentifier);
                    var employees = await _organizationService.GetBusinessEmployees(b.BusinessIdentifier);
                    var p = BAL.GetFunctionalBudgetSummaries(employees, salaries, b);
                    functionalBudgetSummaries.AddRange(p);
                }
                return functionalBudgetSummaries;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("OrganizationBUnitBasedSalarySummary")]
        public async Task<ActionResult<List<FunctionalBudgetSummary>>> OrganizationBUnitBasedSalarySummary(string organizationIdentifier)
        {
            try
            {
                var businesses = await _organizationService.GetBusinesses(organizationIdentifier);
                List<FunctionalBudgetSummary> functionalBudgetSummaries = new List<FunctionalBudgetSummary>();
                foreach (var b in businesses)
                {
                    var salaries = await _annualSalaryPlanning.BusinessJobTypeBasedSalaries(b.BusinessIdentifier);
                    var employees = await _organizationService.GetBusinessEmployees(b.BusinessIdentifier);
                    var costcenters = await _organizationService.GetBusinessCostCenters(b.BusinessIdentifier);
                    var p = BAL.GetFunctionalBudgetSummaries(employees, salaries, b);
                    functionalBudgetSummaries.AddRange(p);
                }
                return functionalBudgetSummaries;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
