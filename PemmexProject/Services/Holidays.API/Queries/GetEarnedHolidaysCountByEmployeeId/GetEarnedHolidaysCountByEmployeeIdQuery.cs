using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Enumerations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetEarnedHolidaysByEmployeeId
{
    public class GetEarnedHolidaysCountByEmployeeIdQuery : IRequest<int>
    {
        public Guid Id { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class GetEarnedHolidaysCountByEmployeeIdQueryHandeler : IRequestHandler<GetEarnedHolidaysCountByEmployeeIdQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        public GetEarnedHolidaysCountByEmployeeIdQueryHandeler(IApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        public async Task<int> Handle(GetEarnedHolidaysCountByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _context.HolidaySettings
                    .Where(s => s.BusinessIdentifier == request.businessIdentifier)
                    .OrderByDescending(d => d.HolidayCalendarYear).FirstOrDefaultAsync();

                var holidays = await _context.CompanyToEmployeeHolidays
                    .Where(e => e.EmployeeId == request.Id && e.HolidaySettingsIdentitfier == setting.HolidaySettingsIdentitfier)
                    .FirstOrDefaultAsync(cancellationToken);


                if (holidays == null)
                    return 0;


                var earned = (int)(((decimal)holidays.AnnualHolidaysEntitled / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365))
                    * await GetDaysEmployeeWorkFor(holidays.EmployementStartDate.ToDateTime3(), setting,request.Id));

               
                return earned;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private async Task<int> GetDaysEmployeeWorkFor(DateTime startedDate, HolidaySettings setting,Guid EmployeeId)
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

                Holidays.ForEach(p => {
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
    }
}
