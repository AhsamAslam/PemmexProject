using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PemmexAPIAggregator.Helpers;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Models.PerformanceBonus;
using PemmexAPIAggregator.Queries.BUnitPerformanceBonusSummary;
using PemmexAPIAggregator.Services;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
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
    public class PerformanceController : APIControllerBase
    {
        private readonly IAnnualSalaryPlanning _annualSalaryPlanning;
        private readonly IOrganizationService _organizationService;
        private readonly IPerformanceBonus _performanceBonus;
        private readonly ICompensationService _compensationService;
        private readonly INotificationService _notificationService;
        private ConnectionMultiplexer _Connection = null;
        public PerformanceController(IAnnualSalaryPlanning annualSalaryPlanning,
            IOrganizationService organizationService,
            IPerformanceBonus performanceBonus,
            ICompensationService compensationService
            ,IConfiguration configuration,
            INotificationService notificationService)
        {
            _annualSalaryPlanning = annualSalaryPlanning;
            _organizationService = organizationService;
            _performanceBonus = performanceBonus;
            _compensationService = compensationService;
            _Connection = ConnectionMultiplexer.Connect(configuration["RedisConnectionStrings"]);
            _notificationService = notificationService;
        }
        [HttpGet]
        [Route("JobTypeBasedPerformanceBonusSummary")]
        public async Task<ActionResult<List<JobBasedBonusSummary>>>
            OrganizationJobTypeBasedBonusSummary(DateTime startDate, DateTime endDate)
        {
            try
            {
                var businesses = await _organizationService.GetBusinesses(CurrentUser.OrganizationIdentifier);
                var employees = await _organizationService.GetOrganizationEmployees(CurrentUser.OrganizationIdentifier);
                List<JobBasedBonusSummary> functionalBonusSummaries = new List<JobBasedBonusSummary>();
                var salaries = await _annualSalaryPlanning.OrganizationSalaries(CurrentUser.OrganizationIdentifier, startDate, endDate);
                var jobcatalogues = await _compensationService.GetOrganizationJobCatalogues(CurrentUser.OrganizationIdentifier);
                foreach (var b in businesses)
                {
                    var p = BAL.GetFunctionalBonusSummaries(employees, salaries, b, jobcatalogues.ToList());
                    functionalBonusSummaries.AddRange(p);
                }
                return functionalBonusSummaries;
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("OrganizationBUnitBasedBonusSummary")]
        public async Task<ActionResult<List<BusinessUnitBonusSummary>>>
            OrganizationBUnitBasedSalarySummary(DateTime startDate, DateTime endDate)
        {
            try
            {
                return await Mediator.Send(new BUnitPerformanceBonusSummaryQuery()
                {
                    organizationIdentifier = CurrentUser.OrganizationIdentifier,
                    startDate = startDate,
                    endDate = endDate
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("TotalPerformanceBonusCount")]
        public async Task<ActionResult<double>> TotalPerformanceBonusCount(DateTime startDate, DateTime endDate)
        {
            try
            {
                double Count = 0.0;
                var employees = await _organizationService.GetOrganizationEmployees(CurrentUser.OrganizationIdentifier);
                var salaries = await _annualSalaryPlanning.OrganizationSalaries(CurrentUser.OrganizationIdentifier, startDate, endDate);
                var jobcatalogues = await _compensationService.GetOrganizationJobCatalogues(CurrentUser.OrganizationIdentifier);
                foreach (var employee in employees)
                {
                    var Salary = salaries.Where(x => x.EmployeeIdentifier == employee.EmployeeIdentifier).Sum(s=>s.TotalMonthlyPay);
                    //var AdditionalAgreedPart = salaries.Where(x => x.EmployeeIdentifier == employee.EmployeeIdentifier).Sum(s=>s.AdditionalAgreedPart);
                    var bonus = jobcatalogues.FirstOrDefault(j => j.businessIdentifier == employee.BusinessIdentifier
                        && j.grade == employee.Grade && j.jobFunction == employee.JobFunction);
                    if(bonus != null)
                    {
                        Count += ((Salary / 100) * bonus.annual_bonus);
                    }
                }
                return Count;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region softtarget
        [HttpPost]
        [Route("GroupSoftTargets")]
        [Authorize("BuHR")]
        public async Task<bool> CreateGroupSoftTargets(SoftTargetsDto targets)
        {
            try
            {
                bool target = false;
                List<SoftTargetsDto> TargetList = new List<SoftTargetsDto>();
                IEnumerable<Employee> employees = new List<Employee>();
                employees = await _organizationService.GetOrganizationEmployees(CurrentUser.OrganizationIdentifier);
                var targetType = TargetAssignType.OrganizationBasedTarget;
                if (targets.businessUnitIdentifier != null)
                {
                   employees = await _organizationService.GetTeamMembers(targets.businessUnitIdentifier);
                   targetType = TargetAssignType.BusinessUnitBasedTarget;
                }

                if(employees.Count() <= 0)
                {
                    throw new Exception("No Employee Found");
                }

                SoftTargetsDto obj = new SoftTargetsDto();
                List<SoftTargetsDetailDto> detailList = new List<SoftTargetsDetailDto>();
                obj.softTargetsName = targets.softTargetsName;
                obj.softTargetsDescription = targets.softTargetsDescription;
                obj.performanceCriteria = targets.performanceCriteria;
                obj.evaluationDateTime = DateTime.Now;
                obj.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                obj.TargetType = targetType;
                foreach (var item in employees)
                {
                    SoftTargetsDetailDto detailObj = new SoftTargetsDetailDto();
                    detailObj.businessIdentifier = item.BusinessIdentifier;
                    detailObj.managerIdentifier = item.ManagerIdentifier;
                    detailObj.costCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;
                    detailObj.employeeIdentifier = item.EmployeeIdentifier;
                    detailList.Add(detailObj);
                }
                obj.SoftTargetsDetail = detailList;
                target = await _performanceBonus.CreateSoftTargets(JsonConvert.SerializeObject(obj));

                if (target == true)
                {
                    Parallel.ForEach(employees, async employee =>
                    {
                        dynamic json = new ExpandoObject();
                        json.userId = employee.EmployeeIdentifier;
                        json.isRead = false;
                        json.taskType = TaskType.SoftTargets;
                        json.description = "Soft target is assign to employee...";
                        json.title = "SoftTargets";


                        var notification = await _notificationService.PostNotification(JsonConvert.SerializeObject(json));
                    });
                }
                return target;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("TeamSoftTargets")]
        [Authorize("Manager")]
        public async Task<bool> CreateTeamSoftTargets(SoftTargetsDto targets)
        {
            try
            {
                bool target = false;
                
                IEnumerable<Employee> employees = new List<Employee>();
                var employeeList = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
                employees = employeeList.Where(x => x.ManagerIdentifier == CurrentUser.EmployeeIdentifier).ToList();

                if (employees.Count() <= 0)
                {
                    throw new Exception("No user found");
                }
                SoftTargetsDto obj = new SoftTargetsDto();
                List<SoftTargetsDetailDto> detailList = new List<SoftTargetsDetailDto>();
                obj.softTargetsName = targets.softTargetsName;
                obj.softTargetsDescription = targets.softTargetsDescription;
                obj.performanceCriteria = targets.performanceCriteria;
                obj.evaluationDateTime = DateTime.Now;
                obj.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                obj.TargetType = TargetAssignType.TeamBasedTarget;
                foreach (var item in employees)
                {
                    SoftTargetsDetailDto detailObj = new SoftTargetsDetailDto();
                    detailObj.businessIdentifier = item.BusinessIdentifier;
                    detailObj.managerIdentifier = item.ManagerIdentifier;
                    detailObj.costCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;
                    detailObj.employeeIdentifier = item.EmployeeIdentifier;
                    detailList.Add(detailObj);

                   
                }
                obj.SoftTargetsDetail = detailList;

                target = await _performanceBonus.CreateSoftTargets(JsonConvert.SerializeObject(obj));
                if (target == true)
                {
                    Parallel.ForEach(employees, async employee =>
                    {
                        dynamic json = new ExpandoObject();
                        json.userId = employee.EmployeeIdentifier;
                        json.isRead = false;
                        json.taskType = TaskType.SoftTargets;
                        json.description = "Soft target is assign to employee...";
                        json.title = "SoftTargets";


                        var notification = await _notificationService.PostNotification(JsonConvert.SerializeObject(json));
                    });
                }
                
                    return target;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        [HttpPost]
        [Route("UserSoftTargets")]
        [Authorize("User")]
        public async Task<bool> CreateUserSoftTargets(SoftTargetsDto targets)
        {
            try
            {
                bool target = false;
                List<SoftTargetsDto> TargetList = new List<SoftTargetsDto>();
                List<Employee> employees = new List<Employee>();
                var s = await _organizationService.GetEmployee(CurrentUser.EmployeeIdentifier);
                if (s != null)
                {
                    employees.Add(s);
                }
                else
                {
                    throw new Exception("No user found");
                }

                //foreach (var item in employees)
                //{
                //    SoftTargetsDto obj = new SoftTargetsDto();
                //    obj.softTargetsName = targets.softTargetsName;
                //    obj.softTargetsDescription = targets.softTargetsDescription;
                //    obj.performanceCriteria = targets.performanceCriteria;
                //    obj.organizationIdentifier = item.OrganizationIdentifier;
                //    obj.businessIdentifier = item.BusinessIdentifier;
                //    obj.managerIdentifier = item.ManagerIdentifier;
                //    obj.costCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;
                //    obj.evaluationDateTime = DateTime.Now;
                //    obj.employeeIdentifier = item.EmployeeIdentifier;
                //    TargetList.Add(obj);
                //}

                //target = await _performanceBonus.CreateSoftTargets(JsonConvert.SerializeObject(TargetList));
                SoftTargetsDto obj = new SoftTargetsDto();
                List<SoftTargetsDetailDto> detailList = new List<SoftTargetsDetailDto>();
                obj.softTargetsName = targets.softTargetsName;
                obj.softTargetsDescription = targets.softTargetsDescription;
                obj.performanceCriteria = targets.performanceCriteria;
                obj.evaluationDateTime = DateTime.Now;
                obj.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                obj.TargetType = TargetAssignType.EmployeeBasedTarget;
                foreach (var item in employees)
                {
                    SoftTargetsDetailDto detailObj = new SoftTargetsDetailDto();
                    detailObj.businessIdentifier = item.BusinessIdentifier;
                    detailObj.managerIdentifier = item.ManagerIdentifier;
                    detailObj.costCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;
                    detailObj.employeeIdentifier = item.EmployeeIdentifier;
                    detailList.Add(detailObj);
                }
                obj.SoftTargetsDetail = detailList;

                target = await _performanceBonus.CreateSoftTargets(JsonConvert.SerializeObject(obj));
                if (target == true)
                {
                    Parallel.ForEach(employees, async employee =>
                    {
                        dynamic json = new ExpandoObject();
                        json.userId = employee.EmployeeIdentifier;
                        json.isRead = false;
                        json.taskType = TaskType.SoftTargets;
                        json.description = "Soft target is assign to employee...";
                        json.title = "SoftTargets";


                        var notification = await _notificationService.PostNotification(JsonConvert.SerializeObject(json));
                    });
                }

                return target;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        #endregion

        #region hardtargets
        [HttpPost]
        [Route("GroupHardTargets")]
        [Authorize("BuHR")]
        public async Task<bool> CreateGroupHardTargets(HardTargetsDto targets)
        {
            try
            {
                bool target = false;
                IEnumerable<Employee> employees = new List<Employee>();
                employees = await _organizationService.GetOrganizationEmployees(CurrentUser.OrganizationIdentifier);
                var targetType = TargetAssignType.OrganizationBasedTarget;
                if (targets.businessUnitIdentifier != null)
                {
                   employees = await _organizationService.GetTeamMembers(targets.businessUnitIdentifier);
                    targetType = TargetAssignType.BusinessUnitBasedTarget;
                }
                HardTargetsDto obj = new HardTargetsDto();
                List<HardTargetsDetailDto> detailList = new List<HardTargetsDetailDto>();
                obj.hardTargetsName = targets.hardTargetsName;
                obj.hardTargetsDescription = targets.hardTargetsDescription;
                obj.measurementCriteria = targets.measurementCriteria;
                obj.weightage = targets.weightage;
                obj.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                obj.TargetType = targetType;
                obj.evaluationDateTime = DateTime.Now;
                obj.MeasurementCriteriaResult = 0;
                foreach (var item in employees)
                {
                    HardTargetsDetailDto detailObj = new HardTargetsDetailDto();
                    detailObj.EmployeeIdentifier = item.EmployeeIdentifier;
                    detailObj.BusinessIdentifier = item.BusinessIdentifier;
                    detailObj.ManagerIdentifier = item.ManagerIdentifier;
                    detailObj.CostCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;
                    
                    detailList.Add(detailObj);
                }
                obj.HardTargetsDetail = detailList;
                target = await _performanceBonus.CreateHardTargets(JsonConvert.SerializeObject(obj));
                if (target == true)
                {
                    Parallel.ForEach(employees, async employee =>
                    {
                        dynamic json = new ExpandoObject();
                        json.userId = employee.EmployeeIdentifier;
                        json.isRead = false;
                        json.taskType = TaskType.HardTargets;
                        json.description = "Hard target is assign to employee...";
                        json.title = "HardTargets";


                        var notification = await _notificationService.PostNotification(JsonConvert.SerializeObject(json));
                        
                    });
                }

                return target;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("TeamHardTargets")]
        [Authorize("Manager")]
        public async Task<bool> CreateTeamHardTargets(HardTargetsDto targets)
        {
            try
            {
                bool target = false;
                List<HardTargetsDto> TargetList = new List<HardTargetsDto>();
                IEnumerable<Employee> employees = new List<Employee>();
                var employeeList = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
                employees = employeeList.Where(x => x.ManagerIdentifier == CurrentUser.EmployeeIdentifier).ToList();
                
                if(employees.Count() <= 0)
                {
                   throw new Exception("No user found"); 
                }

                HardTargetsDto obj = new HardTargetsDto();
                List<HardTargetsDetailDto> detailList = new List<HardTargetsDetailDto>();
                obj.hardTargetsName = targets.hardTargetsName;
                obj.hardTargetsDescription = targets.hardTargetsDescription;
                obj.measurementCriteria = targets.measurementCriteria;
                obj.weightage = targets.weightage;
                obj.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                obj.evaluationDateTime = DateTime.Now;
                obj.TargetType = TargetAssignType.TeamBasedTarget;
                obj.MeasurementCriteriaResult = 0;
                foreach (var item in employees)
                {
                    HardTargetsDetailDto detailObj = new HardTargetsDetailDto();
                    detailObj.EmployeeIdentifier = item.EmployeeIdentifier;
                    detailObj.BusinessIdentifier = item.BusinessIdentifier;
                    detailObj.ManagerIdentifier = item.ManagerIdentifier;
                    detailObj.CostCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;

                    detailList.Add(detailObj);
                }
                obj.HardTargetsDetail = detailList;

                target = await _performanceBonus.CreateHardTargets(JsonConvert.SerializeObject(obj));
                if (target == true)
                {
                    Parallel.ForEach(employees, async employee =>
                    {
                        dynamic json = new ExpandoObject();
                        json.userId = employee.EmployeeIdentifier;
                        json.isRead = false;
                        json.taskType = TaskType.HardTargets;
                        json.description = "Hard target is assign to employee...";
                        json.title = "HardTargets";


                        var notification = await _notificationService.PostNotification(JsonConvert.SerializeObject(json));
                    });
                }
                return target;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        [Route("EmployeeHardTargets")]
        [Authorize("User")]
        public async Task<bool> CreateEmployeeHardTargets(HardTargetsDto targets)
        {
            try
            {
                bool target = false;
                List<HardTargetsDto> TargetList = new List<HardTargetsDto>();
                List<Employee> employees = new List<Employee>();
                var s = await _organizationService.GetEmployee(CurrentUser.EmployeeIdentifier);
                if(s!= null)
                {
                    employees.Add(s);
                }
                else
                {
                    throw new Exception("No user found");
                }

                //foreach (var item in employees)
                //{
                //    HardTargetsDto obj = new HardTargetsDto();
                //    obj.hardTargetsName = targets.hardTargetsName;
                //    obj.hardTargetsDescription = targets.hardTargetsDescription;
                //    obj.measurementCriteria = targets.measurementCriteria;
                //    obj.weightage = targets.weightage;
                //    obj.employeeIdentifier = item.EmployeeIdentifier;
                //    obj.organizationIdentifier = item.OrganizationIdentifier;
                //    obj.businessIdentifier = item.BusinessIdentifier;
                //    obj.managerIdentifier = item.ManagerIdentifier;
                //    obj.costCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;
                //    obj.evaluationDateTime = DateTime.Now;
                //    TargetList.Add(obj);
                //}

                //target = await _performanceBonus.CreateHardTargets(JsonConvert.SerializeObject(TargetList));
                HardTargetsDto obj = new HardTargetsDto();
                List<HardTargetsDetailDto> detailList = new List<HardTargetsDetailDto>();
                obj.hardTargetsName = targets.hardTargetsName;
                obj.hardTargetsDescription = targets.hardTargetsDescription;
                obj.measurementCriteria = targets.measurementCriteria;
                obj.weightage = targets.weightage;
                obj.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                obj.evaluationDateTime = DateTime.Now;
                obj.TargetType = TargetAssignType.EmployeeBasedTarget;
                obj.MeasurementCriteriaResult = 0;
                foreach (var item in employees)
                {
                    HardTargetsDetailDto detailObj = new HardTargetsDetailDto();
                    detailObj.EmployeeIdentifier = item.EmployeeIdentifier;
                    detailObj.BusinessIdentifier = item.BusinessIdentifier;
                    detailObj.ManagerIdentifier = item.ManagerIdentifier;
                    detailObj.CostCenterIdentifier = item.CostCenterIdentifier;//item.CostCenterIdentifier;

                    detailList.Add(detailObj);
                }
                obj.HardTargetsDetail = detailList;
                target = await _performanceBonus.CreateHardTargets(JsonConvert.SerializeObject(obj));
                if (target == true)
                {
                    Parallel.ForEach(employees, async employee =>
                    {
                        dynamic json = new ExpandoObject();
                        json.userId = employee.EmployeeIdentifier;
                        json.isRead = false;
                        json.taskType = TaskType.HardTargets;
                        json.description = "Hard target is assign to employee...";
                        json.title = "HardTargets";


                        var notification = await _notificationService.PostNotification(JsonConvert.SerializeObject(json));
                    });
                }

                return target;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        [HttpGet]
        [Route("GeneratePerformanceSummary")]
        public async Task<ActionResult<List<PerformanceEvaluationSummaryDto>>> GeneratePerformanceSummary()
        {
            try
            {
                List<PerformanceEvaluationSummaryDto> objList = new List<PerformanceEvaluationSummaryDto>();
                var employees = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
                string[] employeeIdentifiers = employees.Select(e => e.EmployeeIdentifier).ToArray();
                var performanceSummary = await _performanceBonus.GetPerformanceEvaluationSummaryDetail(employeeIdentifiers);

                if (performanceSummary.Count() > 0)
                {
                    return performanceSummary;
                }
                // StartDate and EndDate from GetPerfromanceBudgetPlanning Api
                var startAndEndDate = await _performanceBonus.GetPerfromanceBudgetPlanning(CurrentUser.OrganizationIdentifier);
                if (startAndEndDate == null)
                {
                    throw new Exception("GetPerfromanceBudgetPlanning return null...");
                }
                var totalMonthlySalary = await _compensationService.GetEmployeesSalaryDetails(employeeIdentifiers, startAndEndDate.startDate, startAndEndDate.endDate);

                //Loop on employees
                //Calculate har employee ka bonus percentage
                var jobCatalog = await _compensationService.GetOrganizationJobCatalogues(CurrentUser.OrganizationIdentifier);
                var employeeSalaries = await _compensationService.GetEmployeesSalaryDetails(employeeIdentifiers);
                foreach (var employee in employees)
                {

                    var bonus = jobCatalog.FirstOrDefault(x => x.businessIdentifier == employee.BusinessIdentifier
                                && x.grade == employee.Grade && x.jobFunction == employee.JobFunction);
                    if (bonus == null)
                    {
                        throw new Exception(bonus.ToString());
                    }

                    var baseSalary = employeeSalaries.Where(x => x.EmployeeIdentifier == employee.EmployeeIdentifier)
                        .OrderByDescending(y => y.EffectiveDate)
                        .FirstOrDefault();
                    //var additionalAgreedPart = employeeSalaries.Where(x => x.EmployeeIdentifier == employee.EmployeeIdentifier)
                    //    .OrderByDescending(y => y.EffectiveDate)
                    //    .Select(a => a.AdditionalAgreedPart)
                    //    .FirstOrDefault();


                    var bonusPercentage = (((baseSalary.BaseSalary + baseSalary.AdditionalAgreedPart) / 100) * bonus.annual_bonus);


                    #region Model Fill
                    PerformanceEvaluationSummaryDto obj = new PerformanceEvaluationSummaryDto();
                    obj.name = employee.FirstName + " " + employee.LastName;
                    obj.title = employee.Title;
                    obj.country = bonus.country;
                    obj.grade = bonus.grade;
                    obj.jobFunction = bonus.jobFunction;
                    obj.totalSalary = totalMonthlySalary.Where(x => x.employeeIdentifier == employee.EmployeeIdentifier).Select(y => y.totalMonthlyPay).Sum();
                    obj.bonusPercentage = bonusPercentage;
                    obj.bonusAmount = (obj.totalSalary * obj.bonusPercentage) / 100.00;
                    obj.resultedBonusPercentage = 0.00;
                    obj.resultedBonusAmountBeforeMultiplier = 0.00;
                    obj.performanceMultiplier = 0.00;
                    obj.resultedBonusAmountAfterMultiplier = 0.00;
                    obj.companyProfitabilityMultiplier = startAndEndDate.companyProfitabilityMultiplier;
                    obj.finalBonusAmount = 0.00;
                    obj.bonusPayoutDate = startAndEndDate.bonusPayoutDate;
                    obj.employeeIdentifier = employee.EmployeeIdentifier;
                    obj.businessIdentifier = employee.BusinessIdentifier;
                    obj.organizationIdentifier = employee.OrganizationIdentifier;
                    obj.managerIdentifier = employee.ManagerIdentifier;



                    objList.Add(obj);
                    #endregion
                }


                return objList;
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        [Route("PerformancePlanningResultBusinessUnit")]
        public async Task<ActionResult<List<BusinessUnitBonusSummary>>> PerformanceBudgetResult()
        {
            try
            {
                List<BusinessUnitBonusSummary> businessUnitBonusSummaries = new List<BusinessUnitBonusSummary>();
                var salaries = await _annualSalaryPlanning.OrganizationCompensation(CurrentUser.OrganizationIdentifier);
                var summaries = await _performanceBonus.GetPerformanceEvaluationSummary(CurrentUser.OrganizationIdentifier); // linq 
                var businesses = await _organizationService.GetBusinesses(CurrentUser.OrganizationIdentifier);
                var businessUnits = await _organizationService.GetBusinessUnits(CurrentUser.OrganizationIdentifier);
                var filteredBusinessUnits = businessUnits.Where(x => x.isBunit == true);
                foreach (var business in businesses)
                {
                    foreach (var businessUnit in filteredBusinessUnits)
                    {
                        var emp = businessUnits.Where(x => x.BUnitIdentifier == businessUnit.BUnitIdentifier && x.BusinessIdentifier == business.BusinessIdentifier);
                        BusinessUnitBonusSummary businessUnitBonusSummary = new BusinessUnitBonusSummary();
                        businessUnitBonusSummary.title = businessUnit.Title;
                        businessUnitBonusSummary.businessIdentifier = businessUnit.BUnitIdentifier;
                        foreach (var e in emp)
                        {
                            var summaryObj = summaries.Find(x => x.employeeIdentifier == e.EmployeeIdentifier);
                            if(summaryObj != null)
                            {
                                var salaryObj = salaries.ToList().Find(x => x.EmployeeIdentifier == e.EmployeeIdentifier);
                                businessUnitBonusSummary.bonusValue += summaryObj.finalBonusAmount;
                                businessUnitBonusSummary.monthlySalaryValue += salaryObj.TotalMonthlyPay;
                            }
                        }
                        businessUnitBonusSummaries.Add(businessUnitBonusSummary);
                    }
                }
                return businessUnitBonusSummaries;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("PerformanceBudgetResultDetail")]
        public async Task<ActionResult<List<PerformanceEvaluationSummaryDto>>> PerformanceBudgetResultDetail(string employeeIdentifier)
        {
            try
            {
                string[] emp;
                var _redisCache = _Connection.GetDatabase();
                var employees = await _redisCache.StringGetAsync("Team_" + employeeIdentifier);
                var value = string.IsNullOrWhiteSpace(employees)
                ? default(List<RedisTeamDto>)
                : JsonConvert.DeserializeObject<List<RedisTeamDto>>(employees);

                if (employees.IsNull)
                {
                    var employee = await _organizationService.GetTeamMembers(employeeIdentifier);
                    emp = employee.Select(e => e.EmployeeIdentifier).ToArray();
                }
                else
                {
                    emp = value.Select(e => e.EmployeeIdentifier).ToArray();
                }

                var summaryDetail = await _performanceBonus.GetPerformanceEvaluationSummaryDetail(emp);
                return summaryDetail;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("PerformanceWholeTeamSummary")]
        public async Task<ActionResult<List<PerformanceWholeTeamSummaryDto>>> PerformanceWholeTeamSummary()
        {
            try
            {
                List<PerformanceWholeTeamSummaryDto> objList = new List<PerformanceWholeTeamSummaryDto>();
                var salaries = await _annualSalaryPlanning.OrganizationCompensation(CurrentUser.OrganizationIdentifier);
                var summaries = await _performanceBonus.GetPerformanceEvaluationSummary(CurrentUser.OrganizationIdentifier); // linq 
                var businesses = await _organizationService.GetBusinesses(CurrentUser.OrganizationIdentifier);
                var businessUnits = await _organizationService.GetBusinessUnits(CurrentUser.OrganizationIdentifier);
                businessUnits = businessUnits.Where(x => x.isBunit == true);
                foreach (var business in businesses)
                {
                    foreach (var businessUnit in businessUnits)
                    {
                        var emp = businessUnits.Where(x => x.BusinessIdentifier == businessUnit.BUnitIdentifier && x.BusinessIdentifier == business.BusinessIdentifier);
                        PerformanceWholeTeamSummaryDto obj = new PerformanceWholeTeamSummaryDto();
                        obj.title = businessUnit.Title;
                        obj.businessIdentifier = business.BusinessIdentifier;
                        foreach (var e in emp)
                        {
                            var summaryObj = summaries.Find(x => x.employeeIdentifier == e.EmployeeIdentifier);
                            var salaryObj = salaries.ToList().Find(x => x.EmployeeIdentifier == e.EmployeeIdentifier);
                            if(summaryObj == null)
                            {
                                obj.monthlySalaryValue += salaryObj.TotalMonthlyPay;
                                obj.status = false;
                            }
                            else
                            {
                                obj.monthlySalaryValue += (salaryObj.TotalMonthlyPay + summaryObj.finalBonusAmount);
                                obj.status = false;
                            }
                            
                            
                        }
                        objList.Add(obj);
                    }
                }
                return objList;

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        [Route("UpdateBonusFromPerformanceBonus")]
        public async Task<ActionResult<bool>> UpdateBonusFromPerformanceBonus()
        {
            try
            {
                var performanceSummary = await _performanceBonus.GetPerformanceEvaluationSummary(CurrentUser.OrganizationIdentifier);
                if(performanceSummary.Count() == 0)
                {
                    throw new Exception("Record not found!");
                }
                string[] employeeIdentifier = performanceSummary.Select(e => e.employeeIdentifier).ToArray();
                List<UpdateBonusFromPerformanceBonusRequest> requestList = new List<UpdateBonusFromPerformanceBonusRequest>();
                foreach (var item in performanceSummary)
                {
                    UpdateBonusFromPerformanceBonusRequest request = new UpdateBonusFromPerformanceBonusRequest();
                    request.employeeIdentifier = item.employeeIdentifier;
                    request.organizationIdentifier = item.organizationIdentifier;
                    request.businessIdentifier = item.businessIdentifier;
                    request.bonusAmount = item.bonusAmount;
                    requestList.Add(request);
                }

                var result = await _compensationService.UpdateBonusFromPerformanceBonus(JsonConvert.SerializeObject(requestList));
                if (result == true)
                {
                    var updateResult = await _performanceBonus.UpdateAllEmployeeTargetsIsActive(JsonConvert.SerializeObject(employeeIdentifier));
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
