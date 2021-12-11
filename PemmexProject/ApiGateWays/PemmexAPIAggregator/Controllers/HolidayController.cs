using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class HolidayController : APIControllerBase
    {
        private readonly IHolidayService _holidayService;
        private readonly IOrganizationService _organizationService;
        public HolidayController(IHolidayService holidayService, IOrganizationService organizationService)
        {
            _holidayService = holidayService;
            _organizationService = organizationService;
        }
        [HttpGet]
        [Route("GetSubordinatesHolidaySummary/{Id}")]
        public async Task<ActionResult<List<HolidaySummary>>> GetSubordinatesHolidaySummary(string id)
        {
            var employees = await _organizationService.GetSuboridnates(id);
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
                holidaySummary.BusinessIdentifier = e.BusinessIdentifier;
                holidaySummary.OrganizationCountry = e.OrganizationCountry;
                holidaySummary.EarnedHolidays = await _holidayService.GetEarnedHolidays(e.BusinessIdentifier, e.Emp_Guid);
                holidaySummary.LeftHolidays = await _holidayService.GetLeftHolidays(e.BusinessIdentifier, e.Emp_Guid,e.OrganizationCountry);
                holidaySummary.PlannedHolidays = await _holidayService.GetPlannedHolidays(e.BusinessIdentifier, e.Emp_Guid);
                holidaySummary.UsedHolidays = await _holidayService.GetUsedHolidays(e.BusinessIdentifier, e.Emp_Guid,e.OrganizationCountry);
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
                    EarnedHolidays = await _holidayService.GetEarnedHolidays(CurrentUser.BusinessIdentifier, CurrentUser.Id),
                    LeftHolidays = await _holidayService.GetLeftHolidays(CurrentUser.BusinessIdentifier, CurrentUser.Id,CurrentUser.OrganizationCountry),
                    PlannedHolidays = await _holidayService.GetPlannedHolidays(CurrentUser.BusinessIdentifier, CurrentUser.Id),
                    UsedHolidays = await _holidayService.GetUsedHolidays(CurrentUser.BusinessIdentifier, CurrentUser.Id,CurrentUser.OrganizationCountry)
                };
        }
        [HttpGet]
        [Route("GetTeamMemberHolidaySummary/{Id}")]
        public async Task<ActionResult<List<HolidaySummary>>> GetTeamMemberHolidaySummary(string id)
        {
            var employees = await _organizationService.GetTeamMembers(id);
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
                holidaySummary.PlannedHolidays = await _holidayService.GetPlannedHolidays(e.BusinessIdentifier, e.Emp_Guid);
                holidaySummary.UsedHolidays = await _holidayService.GetUsedHolidays(e.BusinessIdentifier, e.Emp_Guid,e.OrganizationCountry);
                holidaySummaries.Add(holidaySummary);
            });
            await Task.WhenAll(tasks);
            return holidaySummaries;
        }
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
                        BusinessIdentifier = id,
                        Emp_Guid = e.Emp_Guid,
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
                            BusinessIdentifier = b.BusinessIdentifier,
                            Emp_Guid = e.Emp_Guid,
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
        [Route("TeamHolidays/{Id}")]
        public async Task<ActionResult<List<HolidayDto>>> TeamHolidays(string id)
        {
            var employees = await _organizationService.GetTeamMembers(id);
            List<HolidayDto> holidayDtos = new List<HolidayDto>();
            if (employees.Count() > 0)
            {
                var holidays = await _holidayService.GetTeamHolidays(employees.FirstOrDefault().BusinessIdentifier, id);
                foreach(var e in employees)
                {
                    var temp_holidays = holidays.Where(h => h.EmployeeId == e.Emp_Guid);
                    foreach(var h in temp_holidays)
                    {
                        
                        HolidayDto holiday = new HolidayDto();
                        var substitute = employees.FirstOrDefault(e => e.Emp_Guid == h.SubsituteId);
                        holiday.EmployeeIdentifier = e.EmployeeIdentifier;
                        holiday.EmployeeId = e.Emp_Guid;
                        holiday.FirstName = e.FirstName;
                        holiday.LastName = e.LastName;
                        holiday.Title = e.Title;
                        holiday.SubsituteName = substitute == null ? "" :  (substitute.FirstName + " " + substitute.LastName);
                        holiday.holidayType = h.holidayType;
                        holiday.HolidayStatus = h.HolidayStatus;
                        holiday.organizationIdentifier = h.organizationIdentifier;
                        holiday.SubsituteId = h.SubsituteId;
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
    }
}
