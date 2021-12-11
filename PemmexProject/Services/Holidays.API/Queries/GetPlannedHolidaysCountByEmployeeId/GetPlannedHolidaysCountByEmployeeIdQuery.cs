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

namespace Holidays.API.Queries.GetPlannedHolidaysByEmployeeId
{
    public class GetPlannedHolidaysCountByEmployeeIdQuery : IRequest<int>
    {
        public Guid Id { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class GetPlannedHolidaysCountByEmployeeIdQueryHandeler : IRequestHandler<GetPlannedHolidaysCountByEmployeeIdQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        public GetPlannedHolidaysCountByEmployeeIdQueryHandeler(IApplicationDbContext context, IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        public async Task<int> Handle(GetPlannedHolidaysCountByEmployeeIdQuery request, CancellationToken cancellationToken)
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


                var planned = await GetPlannedHolidaysEmployee(request.businessIdentifier, holidays.EmployementStartDate.ToDateTime3(), setting,request.Id);


                return planned;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<int> GetPlannedHolidaysEmployee(string businessIdentifier, DateTime startedDate, HolidaySettings setting,Guid EmployeeId)
        {
            try
            {
                DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                var holidays = await _context.EmployeeHolidays
                .Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                .Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                .Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                .Where(h => h.HolidayStatus == HolidayStatus.Planned)
                .Where(h => h.EmployeeId == EmployeeId)
                .ToListAsync();

                int leaves = 0;

                holidays.ForEach(p => {
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
