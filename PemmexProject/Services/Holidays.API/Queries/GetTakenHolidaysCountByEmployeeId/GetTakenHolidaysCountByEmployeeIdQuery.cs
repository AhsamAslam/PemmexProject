using AutoMapper;
using Holidays.API.Common;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetCountTakenHolidaysByEmployeeId
{
    public class GetTakenHolidaysCountByEmployeeIdQuery : IRequest<int>
    {
        public string EmployeeIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string country { get; set; }
    }
    public class GetTakenHolidaysCountByEmployeeIdQueryHandeler : IRequestHandler<GetTakenHolidaysCountByEmployeeIdQuery, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICommonHolidayDAL _commonHolidayDAL;
        public GetTakenHolidaysCountByEmployeeIdQueryHandeler(IApplicationDbContext context, ICommonHolidayDAL commonHolidayDAL)
        {
            _context = context;
            _commonHolidayDAL = commonHolidayDAL;
        }
        public async Task<int> Handle(GetTakenHolidaysCountByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _context.HolidaySettings
                    .Where(s => s.BusinessIdentifier == request.businessIdentifier)
                    .OrderByDescending(d => d.HolidayCalendarYear).FirstOrDefaultAsync();

                var holidays = await _context.CompanyToEmployeeHolidays
                    .Where(e => e.EmployeeIdentifier == request.EmployeeIdentifier && e.HolidaySettingsIdentitfier == setting.HolidaySettingsIdentitfier)
                    .FirstOrDefaultAsync(cancellationToken);


                if (holidays == null)
                    return 0;


                var used = await GetUsedHolidaysEmployee(request.country, holidays.EmployementStartDate.ToDateTime3(), setting,request.EmployeeIdentifier);


                return used;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<int> GetUsedHolidaysEmployee(string countryName, DateTime startedDate, HolidaySettings setting,string EmployeeId)
        {
            try
            {
                DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                var holidays = await _context.EmployeeHolidays
                .Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                .Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                .Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                .Where(h => h.HolidayStatus == HolidayStatus.Availed)
                .Where(h => h.EmployeeIdentifier == EmployeeId)
                .ToListAsync();

                int leaves = 0;

                var tasks = holidays.Select(async p => {
                    var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate.GetValueOrDefault();
                    var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate.GetValueOrDefault());
                    int days = _commonHolidayDAL.GetBusinessDays(start, end);
                    var ph = await _commonHolidayDAL.CountPublicHolidays(start, end,_commonHolidayDAL.GetCountryCodeByName(countryName));
                    leaves = leaves + (days - ph);
                });
                await Task.WhenAll(tasks);
                return leaves;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
