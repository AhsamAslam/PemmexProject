using AutoMapper;
using Holidays.API.Common;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Database.Repositories.Interface;
using Holidays.API.Enumerations;
using Holidays.API.Repositories.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetLeftHolidaysByEmployeeId
{
    public class GetLeftHolidaysCountByEmployeeIdQuery : IRequest<int>
    {
        public Guid Id { get; set; }
        public string businessIdentifier { get; set; }
        public string country { get; set; }
    }
    public class GetLeftHolidaysCountByEmployeeIdQueryHandeler : IRequestHandler<GetLeftHolidaysCountByEmployeeIdQuery, int>
    {
        private readonly ICommonHoliday _commonHoliday;
        private readonly IHolidaySettings _holidaySetting;
        private readonly ICompanyToEmployeeHolidays _companyToEmployeeHolidays;
        private readonly IEmployeeHolidays _employeeHolidays;
        public GetLeftHolidaysCountByEmployeeIdQueryHandeler(ICommonHoliday commonHoliday,  IHolidaySettings holidaySetting, ICompanyToEmployeeHolidays companyToEmployeeHolidays, IEmployeeHolidays employeeHolidays)
        {
            _commonHoliday = commonHoliday;
            _holidaySetting = holidaySetting;
            _companyToEmployeeHolidays = companyToEmployeeHolidays;
            _employeeHolidays = employeeHolidays;
        }
        public async Task<int> Handle(GetLeftHolidaysCountByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _holidaySetting.GetHolidaySettingsByBusinessIdentifier(request.businessIdentifier);

                var holidays = await _companyToEmployeeHolidays.GetCompanyToEmployeeHolidaysByEmployeeIdAndHolidaySettingsIdentitfier(request.Id, setting.HolidaySettingsIdentitfier);


                if (holidays == null)
                    return 0;

                
                var AccruedHolidayForCurrentYear = holidays.AccruedHolidaysPreviousYear;
                
                var UsedHolidaysCurrentYear = await GetUsedHolidaysEmployee(holidays.EmployementStartDate.ToDateTime3(), setting,request.Id,request.country);

                var LeftHolidaysCurrentYear = AccruedHolidayForCurrentYear - UsedHolidaysCurrentYear;


                return LeftHolidaysCurrentYear;
            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<int> GetUsedHolidaysEmployee(DateTime startedDate, HolidaySettings setting,Guid EmployeeId,string country)
        {
            try
            {
                DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                //var holidays = await _context.EmployeeHolidays
                //.Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                //.Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                //.Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                //.Where(h => h.EmployeeId == EmployeeId)
                //.ToListAsync();
                var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdHolidayTypeStartAndEndDate(start_calendar, end_calendar, EmployeeId, (int)HolidayTypes.AnnualHoliday);



                int leaves = 0;

                var tasks = holidays.Select(async p => {
                    var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate.GetValueOrDefault();
                    var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate.GetValueOrDefault());
                    int days =  _commonHoliday.GetBusinessDays(start, end);
                    var ph = await _commonHoliday.CountPublicHolidays(start, end,_commonHoliday.GetCountryCodeByName(country));
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
