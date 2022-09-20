using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Models.Holidays;
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
    public class HolidayController : APIControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly IOrganizationService _organizationService;
        public HolidayController(IHolidayService holidayService, IOrganizationService organizationService)
        {
            _holidayService = holidayService;
            _organizationService = organizationService;
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("GetSubordinatesHolidaySummary/{Id}")]
        public async Task<ActionResult<List<HolidaySummary>>> GetSubordinatesHolidaySummary(string id)
        {
            var employees = await _organizationService.GetTeamMembers(id);
            List<HolidaySummary> holidaySummaries = new List<HolidaySummary>();
            var tasks = employees.Select(async e =>
            {
                HolidaySummary holidaySummary = new HolidaySummary();
                holidaySummary.Emp_Guid = e.Emp_Guid;
                holidaySummary.EmployeeIdentifier = e.EmployeeIdentifier;
                holidaySummary.FirstName = e.FirstName;
                holidaySummary.LastName = e.LastName;
                holidaySummary.Title = e.Title;
                holidaySummary.ContractualOrganization = e.ContractualOrganization;
                holidaySummary.CostCenterIdentifier = e.CostCenterIdentifier;
                holidaySummary.CostCenterName = e.CostCenterName;
                holidaySummary.ManagerIdentifier = e.ManagerIdentifier;
                holidaySummary.ManagerName = e.ManagerName;
                holidaySummary.BusinessIdentifier = e.BusinessIdentifier;
                holidaySummary.OrganizationCountry = e.OrganizationCountry;
                holidaySummary.EarnedHolidays = await _holidayService.GetEarnedHolidays(e.BusinessIdentifier, e.EmployeeIdentifier);
                holidaySummary.LeftHolidays = await _holidayService.GetLeftHolidays(e.BusinessIdentifier, e.EmployeeIdentifier,e.OrganizationCountry);
                holidaySummary.PlannedHolidays = await _holidayService.GetPlannedHolidays(e.BusinessIdentifier, e.EmployeeIdentifier);
                holidaySummary.UsedHolidays = await _holidayService.GetUsedHolidays(e.BusinessIdentifier, e.EmployeeIdentifier,e.OrganizationCountry);
                holidaySummaries.Add(holidaySummary);
            });

            await Task.WhenAll(tasks);

            return holidaySummaries;
        }
        [HttpGet]
        [Route("GetCounter")]
        public async Task<ActionResult<HolidayCounter>> GetCounter()
        {
            
                return new HolidayCounter()
                {
                    EarnedHolidays = await _holidayService.GetEarnedHolidays(CurrentUser.BusinessIdentifier, CurrentUser.EmployeeIdentifier),
                    LeftHolidays = await _holidayService.GetLeftHolidays(CurrentUser.BusinessIdentifier, CurrentUser.EmployeeIdentifier,CurrentUser.OrganizationCountry),
                    PlannedHolidays = await _holidayService.GetPlannedHolidays(CurrentUser.BusinessIdentifier, CurrentUser.EmployeeIdentifier),
                    UsedHolidays = await _holidayService.GetUsedHolidays(CurrentUser.BusinessIdentifier, CurrentUser.EmployeeIdentifier,CurrentUser.OrganizationCountry)
                };
        }
        [HttpGet]
        [Route("GetSiblingsHolidaySummary")]
        public async Task<ActionResult<List<HolidaySummary>>> GetSiblingsHolidaySummary()
        {
            var employees = await _organizationService.GetSiblings(CurrentUser.EmployeeIdentifier);
            List<HolidaySummary> holidaySummaries = new List<HolidaySummary>();
            var tasks = employees.Select(async e =>
            {
                HolidaySummary holidaySummary = new HolidaySummary();
                holidaySummary.EmployeeIdentifier = e.EmployeeIdentifier;
                holidaySummary.Emp_Guid = e.Emp_Guid;
                holidaySummary.FirstName = e.FirstName;
                holidaySummary.LastName = e.LastName;
                holidaySummary.Title = e.Title;
                holidaySummary.ContractualOrganization = e.ContractualOrganization;
                holidaySummary.CostCenterIdentifier = e.CostCenterIdentifier;
                holidaySummary.CostCenterName = e.CostCenterName;
                holidaySummary.ManagerIdentifier = e.ManagerIdentifier;
                holidaySummary.ManagerName = e.ManagerName;
                holidaySummary.OrganizationCountry = e.OrganizationCountry;
                holidaySummary.EarnedHolidays = 0;
                holidaySummary.LeftHolidays = 0;
                holidaySummary.BusinessIdentifier = e.BusinessIdentifier;
                holidaySummary.PlannedHolidays = await _holidayService.GetPlannedHolidays(e.BusinessIdentifier, e.EmployeeIdentifier);
                holidaySummary.UsedHolidays = await _holidayService.GetUsedHolidays(e.BusinessIdentifier, e.EmployeeIdentifier,e.OrganizationCountry);
                holidaySummaries.Add(holidaySummary);
            });
            await Task.WhenAll(tasks);
            return holidaySummaries;
        }
        [Authorize("BuHR")]
        [HttpGet]
        [Route("GetBusinessHolidaySummary/{Id}")]
        public async Task<ActionResult<List<HolidaySummary>>> GetBusinessHolidaySummary(string id)
        {

            var employees = await _organizationService.GetBusinessEmployees(id);
            List<HolidaySummary> holidaySummaries = new List<HolidaySummary>();
            var holidays = await _holidayService.EmployeeHolidayCounter(id);
            foreach (var e in employees)
            {
                var holiday = holidays.Where(h => h.Emp_Guid == e.Emp_Guid).FirstOrDefault();
                if(holiday != null)
                {
                    HolidaySummary holidaySummary = new HolidaySummary
                    {
                        EmployeeIdentifier = e.EmployeeIdentifier,
                        Emp_Guid = e.Emp_Guid,
                        BusinessIdentifier = id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        Title = e.Title,
                        ContractualOrganization = e.ContractualOrganization,
                        CostCenterIdentifier = e.CostCenterIdentifier,
                        CostCenterName = e.CostCenterName,
                        ManagerIdentifier = e.ManagerIdentifier,
                        ManagerName = e.ManagerName,
                        OrganizationCountry = e.OrganizationCountry,
                        EarnedHolidays = holiday.EarnedHolidays,
                        LeftHolidays = holiday.LeftHolidays,
                        PlannedHolidays = holiday.PlannedHolidays,
                        UsedHolidays = holiday.UsedHolidays
                    };

                    holidaySummaries.Add(holidaySummary);
                }
            }
            return holidaySummaries;
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("GetOrganizationHolidaySummary/{Id}")]
        public async Task<ActionResult<List<HolidaySummary>>> GetOrganizationHolidaySummary(string id)
        {
            var businesses = await _organizationService.GetBusinesses(id);
            List<HolidaySummary> holidaySummaries = new List<HolidaySummary>();
            foreach (var b in businesses)
            {

                var employees = await _organizationService.GetBusinessEmployees(b.BusinessIdentifier);
                var holidays = await _holidayService.EmployeeHolidayCounter(b.BusinessIdentifier);
                foreach (var e in employees)
                {
                    var holiday = holidays.Where(h => h.Emp_Guid == e.Emp_Guid).FirstOrDefault();
                    if (holiday != null)
                    {
                        HolidaySummary holidaySummary = new HolidaySummary
                        {
                            EmployeeIdentifier = e.EmployeeIdentifier,
                            Emp_Guid = e.Emp_Guid,
                            BusinessIdentifier = b.BusinessIdentifier,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            Title = e.Title,
                            ContractualOrganization = e.ContractualOrganization,
                            CostCenterIdentifier = e.CostCenterIdentifier,
                            CostCenterName = e.CostCenterName,
                            ManagerIdentifier = e.ManagerIdentifier,
                            ManagerName = e.ManagerName,
                            OrganizationCountry = e.OrganizationCountry,
                            EarnedHolidays = holiday.EarnedHolidays,
                            LeftHolidays = holiday.LeftHolidays,
                            PlannedHolidays = holiday.PlannedHolidays,
                            UsedHolidays = holiday.UsedHolidays
                        };

                        holidaySummaries.Add(holidaySummary);
                    }
                }
            }
            return holidaySummaries;
        }
        [HttpGet]
        [Route("SiblingsHolidays")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> SiblingsHolidays()
        {
            var employees = await _organizationService.GetSiblings(CurrentUser.EmployeeIdentifier);

            List<EmployeeHolidayDto> holidayDtos = new List<EmployeeHolidayDto>();
            if (employees.Count() > 0)
            {
                var emp = employees.Select(e => e.EmployeeIdentifier).ToArray();
                var holidays = await _holidayService.GetSiblingsHolidays(emp);
                foreach (var e in employees)
                {
                    var temp_holidays = holidays.Where(h => h.EmployeeIdentifier == e.EmployeeIdentifier);
                    foreach (var h in temp_holidays)
                    {

                        EmployeeHolidayDto holiday = new EmployeeHolidayDto();
                        var substitute = employees.FirstOrDefault(e => e.EmployeeIdentifier == h.SubsituteIdentifier);
                        holiday.EmployeeIdentifier = e.EmployeeIdentifier;
                        holiday.FirstName = e.FirstName;
                        holiday.LastName = e.LastName;
                        holiday.Title = e.Title;
                        holiday.SubsituteName = substitute == null ? "" : (substitute.FirstName + " " + substitute.LastName);
                        holiday.holidayType = h.holidayType;
                        holiday.HolidayStatus = h.HolidayStatus;
                        holiday.organizationIdentifier = h.organizationIdentifier;
                        holiday.SubsituteIdentifier = h.SubsituteIdentifier;
                        holiday.TotalDays = h.TotalDays;
                        holiday.HolidayEndDate = h.HolidayEndDate;
                        holiday.HolidayStartDate = h.HolidayStartDate;
                        holiday.Description = h.Description;
                        holiday.costcenterIdentifier = h.costcenterIdentifier;
                        holiday.businessIdentifier = h.businessIdentifier;

                        holidayDtos.Add(holiday);

                    }
                }
            }

            return holidayDtos;
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("TeamHolidays/{Id}")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> TeamHolidays(string id)
        {
            var employees = await _organizationService.GetTeamMembers(id);
            string[] emp = employees.Select(e => e.EmployeeIdentifier).ToArray();
            List<EmployeeHolidayDto> holidayDtos = new List<EmployeeHolidayDto>();
            if (employees.Count() > 0)
            {
                var holidays = await _holidayService.GetTeamHeirarchyHolidays(emp);
                foreach (var e in employees)
                {
                    var temp_holidays = holidays.Where(h => h.EmployeeIdentifier == e.EmployeeIdentifier);
                    foreach (var h in temp_holidays)
                    {
                        EmployeeHolidayDto holiday = new EmployeeHolidayDto();
                        var substitute = employees.FirstOrDefault(e => e.EmployeeIdentifier == h.SubsituteIdentifier);
                        holiday.EmployeeIdentifier = e.EmployeeIdentifier;
                        holiday.FirstName = e.FirstName;
                        holiday.LastName = e.LastName;
                        holiday.Title = e.Title;
                        holiday.SubsituteName = substitute == null ? "" : (substitute.FirstName + " " + substitute.LastName);
                        holiday.holidayType = h.holidayType;
                        holiday.HolidayStatus = h.HolidayStatus;
                        holiday.organizationIdentifier = h.organizationIdentifier;
                        holiday.SubsituteIdentifier = h.SubsituteIdentifier;
                        holiday.TotalDays = h.TotalDays;
                        holiday.HolidayEndDate = h.HolidayEndDate;
                        holiday.HolidayStartDate = h.HolidayStartDate;
                        holiday.Description = h.Description;
                        holiday.costcenterIdentifier = h.costcenterIdentifier;
                        holiday.businessIdentifier = h.businessIdentifier;

                        holidayDtos.Add(holiday);

                    }
                }
            }

            return holidayDtos;
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("EmployeesWithSickLeaves")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> EmployeesWithThreeSickLeaves(int numberOfMonths)
        {
            var employees = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
            var emp_array = employees.Select(e => e.EmployeeIdentifier).ToArray();
            List<EmployeeHolidayDto> emp_holidays = new List<EmployeeHolidayDto>();
            if(numberOfMonths == 1)
            {
                emp_holidays = await _holidayService.EmployeeSickLeaves(1, 3,emp_array);
            }
            else
            {
                emp_holidays = await _holidayService.EmployeeSickLeaves(3, 5,emp_array);
            }
            foreach(var h in emp_holidays)
            {
                var e = employees.FirstOrDefault(emp => emp.EmployeeIdentifier == h.EmployeeIdentifier);
                if(e != null)
                {
                    var subsitute = employees.FirstOrDefault(emp => emp.EmployeeIdentifier == h.SubsituteIdentifier);
                    h.FirstName = e.FirstName;
                    h.LastName = e.LastName;
                    h.Title = e.Title;
                    h.holidayType = PemmexCommonLibs.Domain.Enums.HolidayTypes.Sick;
                    h.costcenterIdentifier = e.CostCenterIdentifier;
                }
            }
            return emp_holidays;
        }
        [Authorize("Manager")]
        [HttpGet]
        [Route("EmployeesWithMaternityLeaves")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> EmployeesWithMaternityLeaves(int type)
        {
            var employees = await _organizationService.GetTeamMembers(CurrentUser.EmployeeIdentifier);
            var emp_array = employees.Select(e => e.EmployeeIdentifier).ToArray();
            List<EmployeeHolidayDto> emp_holidays = new List<EmployeeHolidayDto>();
            emp_holidays = await _holidayService.EmployeeMaternityLeaves(1, type, emp_array);  
            foreach (var h in emp_holidays)
            {
                var e = employees.FirstOrDefault(emp => emp.EmployeeIdentifier == h.EmployeeIdentifier);
                if (e != null)
                {
                    var subsitute = employees.FirstOrDefault(emp => emp.EmployeeIdentifier == h.SubsituteIdentifier);
                    h.FirstName = e.FirstName;
                    h.LastName = e.LastName;
                    h.Title = e.Title;
                    h.holidayType = PemmexCommonLibs.Domain.Enums.HolidayTypes.Parental;
                    h.costcenterIdentifier = e.CostCenterIdentifier;
                    h.organizationIdentifier = e.OrganizationIdentifier;
                }
            }
            return emp_holidays;
        }
    }
}
