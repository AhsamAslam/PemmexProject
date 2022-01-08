using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Enumerations;
using Holidays.API.Repositories.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetLeftHolidaysByBusinessId
{
    public class GetHolidaysByBusinessIdQuery : IRequest<List<EmployeeHolidaysCounter>>
    {
        public string businessIdentifier { get; set; }
    }
    public class GetHolidaysByBusinessIdQueryHandeler : IRequestHandler<GetHolidaysByBusinessIdQuery, List<EmployeeHolidaysCounter>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly DbContextOptionsBuilder<HolidaysContext> optionsBuilder = null;
        private readonly IHolidaySettings _holidaySetting;
        private readonly ICompanyToEmployeeHolidays _companyToEmployeeHolidays;
        private readonly IEmployeeHolidays _employeeHolidays;

        public GetHolidaysByBusinessIdQueryHandeler(IApplicationDbContext context, IHolidaySettings holidaySetting, ICompanyToEmployeeHolidays companyToEmployeeHolidays, IEmployeeHolidays employeeHolidays, IDateTime dateTime, IConfiguration configuration)
        {
            _context = context;
            _dateTime = dateTime;
            this.optionsBuilder = new DbContextOptionsBuilder<HolidaysContext>()
            .UseSqlServer(configuration.GetConnectionString("HolidaysConnection"));
            _holidaySetting = holidaySetting;
            _companyToEmployeeHolidays = companyToEmployeeHolidays;
            _employeeHolidays = employeeHolidays;

        }
        public async Task<List<EmployeeHolidaysCounter>> Handle(GetHolidaysByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _holidaySetting.GetHolidaySettingsByBusinessIdentifier(request.businessIdentifier);


                var holidays = await _companyToEmployeeHolidays.GetCompanyToEmployeeHolidaysByHolidaySettingsIdentitfier(setting.HolidaySettingsIdentitfier);



                if (holidays.ToList().Count <= 0)
                    throw new Exception("No Holidays Found");

                List<EmployeeHolidaysCounter> employeeHolidaysCounters = new List<EmployeeHolidaysCounter>();
                var tasks = holidays.Select(async e =>
                {
                    EmployeeHolidaysCounter counter = new EmployeeHolidaysCounter();
                    counter.EmployeeIdentifier = e.EmployeeIdentifier;
                    counter.Emp_Guid = e.EmployeeId;

                    var AccruedHolidayForCurrentYear = e.AccruedHolidaysPreviousYear;
                    var UsedHolidaysCurrentYear = await GetUsedHolidaysEmployee(e.EmployementStartDate.ToDateTime3(), setting, e.EmployeeId);
                    counter.EarnedHolidays = (int)(((decimal)e.AnnualHolidaysEntitled / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365))
                    * await GetDaysEmployeeWorkFor(e.EmployementStartDate.ToDateTime3(), setting, e.EmployeeId));
                    counter.LeftHolidays = AccruedHolidayForCurrentYear - UsedHolidaysCurrentYear; ;
                    counter.PlannedHolidays = await GetPlannedHolidaysEmployee(request.businessIdentifier, e.EmployementStartDate.ToDateTime3(), setting, e.EmployeeId);
                    counter.UsedHolidays = await GetUsedHolidaysEmployee(request.businessIdentifier, e.EmployementStartDate.ToDateTime3(), setting, e.EmployeeId);
                    employeeHolidaysCounters.Add(counter);
                });
                await Task.WhenAll(tasks);
                return employeeHolidaysCounters;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<int> GetDaysEmployeeWorkFor(DateTime startedDate, HolidaySettings setting, Guid EmployeeId)
        {
            try
            {
                using (var db = new HolidaysContext(this.optionsBuilder.Options,_dateTime))
                {
                    DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                    DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                    //var holidays = await db.EmployeeHolidays
                    //.Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                    //.Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                    //.Where(h => h.EmployeeId == EmployeeId)
                    //.ToListAsync();
                    var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdStartAndEndDate(start_calendar, end_calendar, EmployeeId);


                    var Holidays = holidays.Where(h => (h.holidayType == HolidayTypes.Parental || h.holidayType == HolidayTypes.Sick || h.holidayType == HolidayTypes.TimeOffWithoutSalary))
                            .Where(h => EF.Functions.DateDiffDay(h.HolidayStartDate, h.HolidayEndDate) > 30).ToList();

                    int leaves = 0;

                    Holidays.ForEach(p =>
                    {
                        var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate;
                        var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate);
                        int? days = (int?)((end - start)?.TotalDays == 0 ? 1 : (end - start)?.TotalDays);
                        leaves = leaves + (days ?? 0);
                    });
                    var days = (int)(DateTime.Now - start_calendar).TotalDays - leaves;
                    return days;
                }


            }
            catch (Exception)
            {
                throw;

            }
        }
        private async Task<int> GetUsedHolidaysEmployee(DateTime startedDate, HolidaySettings setting, Guid EmployeeId)
        {
            try
            {
                using (var db = new HolidaysContext(this.optionsBuilder.Options,_dateTime))
                {
                    DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                    DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                    //var holidays = await db.EmployeeHolidays
                    //.Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                    //.Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                    //.Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                    //.Where(h => h.EmployeeId == EmployeeId)
                    //.ToListAsync();
                    var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdHolidayTypeStartAndEndDate(start_calendar, end_calendar, EmployeeId, (int)HolidayTypes.AnnualHoliday);


                    int leaves = 0;

                    holidays.ToList().ForEach(p =>
                    {
                        var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate;
                        var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate);
                        int? days = (int?)((end - start)?.TotalDays == 0 ? 1 : (end - start)?.TotalDays);
                        leaves = leaves + (days ?? 0);
                    });


                    return leaves;
                }
            }
            catch (Exception)
            {
                throw;

            }
        }
        private async Task<int> GetPlannedHolidaysEmployee(string businessIdentifier, DateTime startedDate, HolidaySettings setting, Guid EmployeeId)
        {
            try
            {
                using (var db = new HolidaysContext(this.optionsBuilder.Options,_dateTime))
                {
                    DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                    DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                    //var holidays = await db.EmployeeHolidays
                    //.Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                    //.Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                    //.Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                    //.Where(h => h.HolidayStatus == HolidayStatus.Planned)
                    //.Where(h => h.EmployeeId == EmployeeId)
                    //.ToListAsync();
                    var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdHolidayTypeHolidayStatusStartAndEndDate(start_calendar, end_calendar, EmployeeId, (int)HolidayTypes.AnnualHoliday, (int)HolidayStatus.Planned);


                    int leaves = 0;

                    holidays.ToList().ForEach(p =>
                    {
                        var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate;
                        var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate);
                        int? days = (int?)((end - start)?.TotalDays == 0 ? 1 : (end - start)?.TotalDays);
                        leaves = leaves + (days ?? 0);
                    });


                    return leaves;
                }

            }
            catch (Exception)
            {
                throw;

            }
        }
        private async Task<int> GetUsedHolidaysEmployee(string businessIdentifier, DateTime startedDate, HolidaySettings setting, Guid EmployeeId)
        {
            try
            {
                using (var db = new HolidaysContext(this.optionsBuilder.Options,_dateTime))
                {
                    DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                    DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                    //var holidays = await db.EmployeeHolidays
                    //.Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                    //.Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                    //.Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                    //.Where(h => h.HolidayStatus == HolidayStatus.Availed)
                    //.Where(h => h.EmployeeId == EmployeeId)
                    //.ToListAsync();
                    var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdHolidayTypeHolidayStatusStartAndEndDate(start_calendar, end_calendar, EmployeeId, (int)HolidayTypes.AnnualHoliday, (int)HolidayStatus.Availed);


                    int leaves = 0;

                    holidays.ToList().ForEach(p =>
                    {
                        var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate;
                        var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate);
                        int? days = (int?)((end - start)?.TotalDays == 0 ? 1 : (end - start)?.TotalDays);
                        leaves = leaves + (days ?? 0);
                    });


                    return leaves;
                }
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}