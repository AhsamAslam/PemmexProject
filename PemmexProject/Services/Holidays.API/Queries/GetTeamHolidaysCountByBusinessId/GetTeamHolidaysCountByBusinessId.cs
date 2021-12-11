using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using Holidays.API.Enumerations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.TeamHolidays
{
    public class GetTeamHolidaysCountByBusinessId : IRequest<List<HolidayEmployeeCounter>>
    {
        public string businessIdentifier { get; set; }
    }
    public class TeamHolidaysQueryHandeler : IRequestHandler<GetTeamHolidaysCountByBusinessId, List<HolidayEmployeeCounter>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        public TeamHolidaysQueryHandeler(IApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        public async Task<List<HolidayEmployeeCounter>> Handle(GetTeamHolidaysCountByBusinessId request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _context.HolidaySettings
                    .Where(s => s.BusinessIdentifier == request.businessIdentifier)
                    .OrderByDescending(d => d.HolidayCalendarYear).FirstOrDefaultAsync();

                var holidays = await _context.CompanyToEmployeeHolidays
                    .Where(e => e.HolidaySettingsIdentitfier == setting.HolidaySettingsIdentitfier)
                    .ToListAsync(cancellationToken);


                if (holidays == null)
                    throw new Exception("No Holidays Found");


                List<HolidayEmployeeCounter> counter = new List<HolidayEmployeeCounter>();
                foreach (var h in holidays)
                {
                    HolidayEmployeeCounter holidayEmployeeCounter = new HolidayEmployeeCounter();
                    holidayEmployeeCounter.AccruedHolidayForCurrentYear = h.AccruedHolidaysPreviousYear;
                    holidayEmployeeCounter.AccruedHolidayForNextYear = (int)(((decimal)h.AnnualHolidaysEntitled / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365))
                        * await GetDaysEmployeeWorkFor(h.EmployementStartDate.ToDateTime3(), setting,h.EmployeeId));
                    holidayEmployeeCounter.UsedHolidaysCurrentYear = await GetUsedHolidaysEmployee(h.EmployementStartDate.ToDateTime3(), setting,h.EmployeeId);

                    holidayEmployeeCounter.LeftHolidaysCurrentYear = holidayEmployeeCounter.AccruedHolidayForCurrentYear - holidayEmployeeCounter.UsedHolidaysCurrentYear;
                    counter.Add(holidayEmployeeCounter);
                }
                //counter.AccruedHolidayForNextYear = (int)(((decimal)holidays.AnnualHolidaysEntitled / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365))
                //    * await GetDaysEmployeeWorkFor(holidays.EmployementStartDate.ToDateTime3(), setting));

                //counter.UsedHolidaysCurrentYear = await GetUsedHolidaysEmployee(holidays.EmployementStartDate.ToDateTime3(), setting);

                //counter.LeftHolidaysCurrentYear = counter.AccruedHolidayForCurrentYear - counter.UsedHolidaysCurrentYear;



                return counter;
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
                DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                var holidays = await _context.EmployeeHolidays
                .Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                .Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                 .Where(h => h.EmployeeId == EmployeeId)
                .ToListAsync();

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
            catch (Exception)
            {
                throw;

            }
        }
        private async Task<int> GetUsedHolidaysEmployee(DateTime startedDate, HolidaySettings setting, Guid EmployeeId)
        {
            try
            {
                DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                var holidays = await _context.EmployeeHolidays
                .Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                .Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                .Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                .Where(h => h.EmployeeId == EmployeeId)
                .ToListAsync();

                int leaves = 0;

                holidays.ForEach(p =>
                {
                    var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate;
                    var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate);
                    int? days = (int?)((end - start)?.TotalDays == 0 ? 1 : (end - start)?.TotalDays);
                    leaves = leaves + (days ?? 0);
                });


                return leaves;
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}
