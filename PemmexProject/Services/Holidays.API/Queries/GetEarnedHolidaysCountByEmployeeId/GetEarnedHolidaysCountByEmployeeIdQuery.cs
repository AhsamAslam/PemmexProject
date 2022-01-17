using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
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

namespace Holidays.API.Queries.GetEarnedHolidaysByEmployeeId
{
    public class GetEarnedHolidaysCountByEmployeeIdQuery : IRequest<int>
    {
        public Guid Id { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class GetEarnedHolidaysCountByEmployeeIdQueryHandeler : IRequestHandler<GetEarnedHolidaysCountByEmployeeIdQuery, int>
    {
        private readonly IDateTime _dateTime;
        private readonly IHolidaySettings _holidaySetting;
        private readonly ICompanyToEmployeeHolidays _companyToEmployeeHolidays;
        private readonly IEmployeeHolidays _employeeHolidays;
        public GetEarnedHolidaysCountByEmployeeIdQueryHandeler(IHolidaySettings holidaySetting, ICompanyToEmployeeHolidays companyToEmployeeHolidays, IEmployeeHolidays employeeHolidays, IDateTime dateTime)
        {
            _dateTime = dateTime;
            _holidaySetting = holidaySetting;
            _companyToEmployeeHolidays = companyToEmployeeHolidays;
            _employeeHolidays = employeeHolidays;
        }
        public async Task<int> Handle(GetEarnedHolidaysCountByEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _holidaySetting.GetHolidaySettingsByBusinessIdentifier(request.businessIdentifier);


                var holidays = await _companyToEmployeeHolidays.GetCompanyToEmployeeHolidaysByEmployeeIdAndHolidaySettingsIdentitfier(request.Id, setting.HolidaySettingsIdentitfier);


                if (holidays == null)
                    return 0;


                var earned = (int)(((decimal)holidays.AnnualHolidaysEntitled / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365))
                    * await GetDaysEmployeeWorkFor(holidays.EmployementStartDate.ToDateTime3(), setting,request.Id));

               
                return earned;
            }
            catch (Exception)
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
                var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdStartAndEndDate(start_calendar, end_calendar, EmployeeId);


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
