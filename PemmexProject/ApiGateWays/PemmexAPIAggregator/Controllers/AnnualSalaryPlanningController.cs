using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PemmexAPIAggregator.Helpers;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Queries;
using PemmexAPIAggregator.Queries.BUnitSummaryByOrganizationIdentifier;
using PemmexAPIAggregator.Queries.FunctionalBudgetCount;
using PemmexAPIAggregator.Services;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AnnualSalaryPlanningController : APIControllerBase
    {
        private readonly IAnnualSalaryPlanning _annualSalaryPlanning;
        private readonly IOrganizationService _organizationService;
        private readonly ITaskManagerService _taskManagerService;
        private readonly ICompensationService _compensationService;

        private ConnectionMultiplexer _Connection = null;

        public AnnualSalaryPlanningController(IAnnualSalaryPlanning annualSalaryPlanning,
            IOrganizationService organizationService, IConfiguration configuration,
            ITaskManagerService taskManagerService, ICompensationService compensationService)
        {
            _annualSalaryPlanning = annualSalaryPlanning;
            _organizationService = organizationService;
            _Connection = ConnectionMultiplexer.Connect(configuration["RedisConnectionStrings"]);
            _taskManagerService = taskManagerService;
            _compensationService = compensationService;
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("OrganizationJobTypeBasedSalarySummary")]
        public async Task<ActionResult<List<OrganizationalBudgetSummary>>> OrganizationJobTypeBasedSalarySummary()
        {
            try
            {
                var businesses = await _organizationService.GetBusinesses(CurrentUser.OrganizationIdentifier);
                var employees = await _organizationService.GetOrganizationEmployees(CurrentUser.OrganizationIdentifier);
                List<OrganizationalBudgetSummary> functionalBudgetSummaries = new List<OrganizationalBudgetSummary>();
                var salaries = await _annualSalaryPlanning.OrganizationCompensation(CurrentUser.OrganizationIdentifier);
                var budget = await _annualSalaryPlanning.GetOrganizationalBudget(CurrentUser.OrganizationIdentifier);
                foreach (var b  in businesses)
                {
                    if (budget != null && budget.ToList().Count > 0)
                    {
                        var p = BAL.GetFunctionalBudgetSummaries(employees, salaries, b,
                            budget.Where(p => p.businessIdentifier == b.BusinessIdentifier).ToList());
                        functionalBudgetSummaries.AddRange(p);
                    }
                    else
                    {
                        var p = BAL.GetFunctionalBudgetSummaries(employees, salaries, b);
                        functionalBudgetSummaries.AddRange(p);
                    }
                    
                }
                return functionalBudgetSummaries;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("OrganizationBUnitBasedSalarySummaryByQuery")]
        public async Task<ActionResult<List<BusinessUnitBudgetSummary>>> OrganizationBUnitBasedSalarySummaryByQuery([FromQuery]BUnitSummaryQuery request)
        {
            try
            {
                return await Mediator.Send(request);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("OrganizationBUnitBasedSalarySummary")]
        public async Task<ActionResult<List<BusinessUnitBudgetSummary>>> OrganizationBUnitBasedSalarySummary()
        {
            try
            {
                return await Mediator.Send(new BUnitSummaryByOrganizationIdentifierQuery() {organizationIdentifier = CurrentUser.OrganizationIdentifier });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("FunctionalSalaryCount")]
        public async Task<ActionResult<double>> FunctionalSalaryCount()
        {
            try
            {
                List<string> emp = new List<string>();
                var employees = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
                var identifiers = employees.Where(e => e.ManagerIdentifier == CurrentUser.EmployeeIdentifier).Select(e => e.EmployeeIdentifier).ToArray();
                return await _annualSalaryPlanning.GetFunctionalSalaryCount(identifiers);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("FunctionalBudgetCount")]
        public async Task<ActionResult<List<OrganizationalBudgetSummary>>> FunctionalBudgetCount()
        {
            try
            {
                var businesses = await _organizationService.GetBusinesses(CurrentUser.OrganizationIdentifier);
                var employees = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
                var team_members = employees.Where(e => e.ManagerIdentifier == CurrentUser.EmployeeIdentifier);
                var summary = await Mediator.Send(new FunctionalBudgetCountQuery() {
                organizationIdentifier = CurrentUser.OrganizationIdentifier,
                employees = team_members                
                });
                foreach(var s in summary)
                {
                    s.businessName = businesses.FirstOrDefault(b => b.BusinessIdentifier == s.businessIdentifier).BusinessName ?? "";
                }
                return summary;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("TeamSalaryDetails")]
        public async Task<ActionResult<List<EmployeeCompensation>>> TeamSalaryDetails()
        {
            try 
            {
                var employees = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
                string[] emp  = employees.Where(e => e.ManagerIdentifier == CurrentUser.EmployeeIdentifier).Select(e => e.EmployeeIdentifier).ToArray();
                var salaries = await _compensationService.GetEmployeesSalaryDetails(emp);
                var salarydictioray = salaries.ToDictionary(s => s.EmployeeIdentifier, s=>s);
                var budget = await _annualSalaryPlanning.GetOrganizationalBudget(CurrentUser.OrganizationIdentifier);
                List<EmployeeCompensation> employeeCompensations = new List<EmployeeCompensation>();
                foreach(var e in employees)
                {
                    Compensation salary;
                    salarydictioray.TryGetValue(e.EmployeeIdentifier, out salary);
                    if(salary != null)
                    {
                        employeeCompensations.Add(new EmployeeCompensation()
                        {
                             EmployeeIdentifier = e.EmployeeIdentifier,
                             CostCenterIdentifier = e.CostCenterIdentifier,
                             CostCenterName = e.CostCenterName,
                             AdditionalAgreedPart = salary.AdditionalAgreedPart,
                             BaseSalary = salary.BaseSalary,
                             BusinessIdentifier = e.BusinessIdentifier,
                             CarBenefit = salary.CarBenefit,
                             EffectiveDate = salary.EffectiveDate,
                             EmissionBenefit = salary.EmissionBenefit,
                             Emp_Guid = e.Emp_Guid,
                             FirstName = e.FirstName,
                             Grade = e.Grade,
                             HomeInternetBenefit = salary.HomeInternetBenefit,
                             InsuranceBenefit = salary.InsuranceBenefit,
                             JobFunction = e.JobFunction,
                             LastName = e.LastName,
                             ManagerIdentifier = e.ManagerIdentifier,
                             ManagerName = e.ManagerName,
                             MiddleName = e.MiddleName,
                             OrganizationCountry = e.OrganizationCountry,
                             PhoneBenefit = salary.PhoneBenefit,
                             Title = e.Title,
                             TotalMonthlyPay = salary.TotalMonthlyPay,
                             currencyCode = salary.currencyCode,
                             mandatoryPercentage = (budget.FirstOrDefault(b => b.businessIdentifier == e.BusinessIdentifier
                             && b.jobFunction == e.JobFunction)?.mandatoryBudgetValue) ?? 0.0
                        });
                    }
                }
                return employeeCompensations;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("BUnitFunctionalBudgetDetails")]
        public async Task<ActionResult<List<FunctionalBudgetDto>>> BUnitFunctionalBudgetDetails(string managerIdentifier)
        {
            try
            {
                string[] emp;
                var _redisCache = _Connection.GetDatabase();
                var employees = await _redisCache.StringGetAsync("Team_" + managerIdentifier);
                var value = string.IsNullOrWhiteSpace(employees)
                ? default(List<RedisTeamDto>)
                : JsonConvert.DeserializeObject<List<RedisTeamDto>>(employees);

                if (employees.IsNull)
                {
                    var employee = await _organizationService.GetTeamMembers(managerIdentifier);
                    emp = employee.Select(e => e.EmployeeIdentifier).ToArray();
                }
                else
                {
                    emp = value.Select(e => e.EmployeeIdentifier).ToArray();
                }
                var budgets = await _annualSalaryPlanning.GetFunctionalBudgetDetails(JsonConvert.SerializeObject(emp));
                return budgets.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("TeamBudgetSummary")]
        public async Task<ActionResult<List<WholeTeamSummaryDto>>> TeamBudgetSummary()
        {
            try
            {
                List<WholeTeamSummaryDto> wholeTeamSummaryDtos = new List<WholeTeamSummaryDto>();
                var budgets = await _annualSalaryPlanning.GetOrganizationFunctionalBudgetDetails(CurrentUser.OrganizationIdentifier);
                var employees = await _organizationService.GetOrganizationEmployees(CurrentUser.OrganizationIdentifier);
                var managers = await _organizationService.GetManagers();
                foreach(var m in managers)
                {
                    var m_b = budgets.Where(b => b.ManagerIdentifier == m.EmployeeIdentifier).ToList();
                    var team_members = employees.Where(e => e.ManagerIdentifier == m.EmployeeIdentifier);
                    var functional_budget = await Mediator.Send(new FunctionalBudgetCountQuery()
                    {
                        organizationIdentifier = CurrentUser.OrganizationIdentifier,
                        employees = team_members
                    });
                    if (m_b.Count() > 0)
                    {
                        WholeTeamSummaryDto wholeTeamSummaryDto = new WholeTeamSummaryDto();
                        wholeTeamSummaryDto.teamSalary = m_b.Sum(b => b.TotalCurrentSalary);
                        wholeTeamSummaryDto.managerName = m.FirstName + " " + m.LastName;
                        wholeTeamSummaryDto.managerIdentifier = m.EmployeeIdentifier;
                        wholeTeamSummaryDto.budgetAllocated = functional_budget.Sum(f => f.TotalbudgetValue);
                        wholeTeamSummaryDto.budgetAllocatedPercentage = functional_budget.Sum(f => f.TotalbudgetPercentageValue);
                        wholeTeamSummaryDto.budgetUsed = (m_b.Sum(q => q.NewTotalSalary) - m_b.Sum(q => q.TotalCurrentSalary));
                        wholeTeamSummaryDto.budgetUsedPercentage =  ((wholeTeamSummaryDto.budgetUsed / wholeTeamSummaryDto.budgetAllocated) * 100);
                        wholeTeamSummaryDto.status = true;
                        wholeTeamSummaryDtos.Add(wholeTeamSummaryDto);
                    }
                    else
                    {
                        WholeTeamSummaryDto wholeTeamSummaryDto = new WholeTeamSummaryDto();
                        wholeTeamSummaryDto.teamSalary = 0;
                        wholeTeamSummaryDto.managerName = m.FirstName + " " + m.LastName;
                        wholeTeamSummaryDto.managerIdentifier = m.EmployeeIdentifier;
                        wholeTeamSummaryDto.budgetAllocated = functional_budget.Sum(f => f.TotalbudgetValue);
                        wholeTeamSummaryDto.budgetAllocatedPercentage = functional_budget.Sum(f => f.TotalbudgetPercentageValue);
                        wholeTeamSummaryDto.budgetUsed = 0;
                        wholeTeamSummaryDto.budgetUsedPercentage = 0;
                        wholeTeamSummaryDto.status = false;
                        wholeTeamSummaryDtos.Add(wholeTeamSummaryDto);
                    }
                }

                return wholeTeamSummaryDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
