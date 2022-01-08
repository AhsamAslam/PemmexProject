using AutoMapper;
using Holidays.API.Common;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Database.Repositories.Interface;
using Holidays.API.Dtos;
using Holidays.API.Enumerations;
using Holidays.API.Repositories.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries
{
    public class GetHolidayCounterQuery:IRequest<HolidayEmployeeCounter> 
    {
        public Guid Id { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class GetHolidayCounterQueryHandeler : IRequestHandler<GetHolidayCounterQuery, HolidayEmployeeCounter>
    {
        private readonly ICommonHoliday _commonHoliday;
        private readonly IHolidaySettings _holidaySetting;
        private readonly ICompanyToEmployeeHolidays _companyToEmployeeHolidays;
        private readonly IEmployeeHolidays _employeeHolidays;
        public GetHolidayCounterQueryHandeler(ICommonHoliday commonHoliday, IHolidaySettings holidaySetting, ICompanyToEmployeeHolidays companyToEmployeeHolidays, IEmployeeHolidays employeeHolidays)
        {
            _commonHoliday = commonHoliday;
            _holidaySetting = holidaySetting;
            _companyToEmployeeHolidays = companyToEmployeeHolidays;
            _employeeHolidays = employeeHolidays;
        }
        public async Task<HolidayEmployeeCounter> Handle(GetHolidayCounterQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _holidaySetting.GetHolidaySettingsByBusinessIdentifier(request.businessIdentifier);

                var holidays = await _companyToEmployeeHolidays.GetCompanyToEmployeeHolidaysByEmployeeIdAndHolidaySettingsIdentitfier(request.Id, setting.HolidaySettingsIdentitfier);


                if (holidays == null)
                    throw new Exception("No Holidays Found");


                HolidayEmployeeCounter counter = new HolidayEmployeeCounter();
                counter.AccruedHolidayForCurrentYear = holidays.AccruedHolidaysPreviousYear;
                counter.UserIdentifier = holidays.EmployeeIdentifier;
                counter.AccruedHolidayForNextYear = (int)(((decimal)holidays.AnnualHolidaysEntitled / (DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365))
                    * await GetDaysEmployeeWorkFor(holidays.EmployementStartDate.ToDateTime3(),setting, request.Id));

                counter.UsedHolidaysCurrentYear = await GetUsedHolidaysEmployee(holidays.EmployementStartDate.ToDateTime3(),setting,request.Id);

                counter.LeftHolidaysCurrentYear = counter.AccruedHolidayForCurrentYear - counter.UsedHolidaysCurrentYear;



                return counter;
            }
            catch(Exception)
            {
                throw;
            }
        }
        private async Task<int> GetDaysEmployeeWorkFor(DateTime startedDate, HolidaySettings setting,Guid EmployeeId)
        {
            try
            {
                DateTime start_calendar =  startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                //var holidays = await _context.EmployeeHolidays
                //.Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                //.Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                //.Where(h => h.EmployeeId == EmployeeId)
                //.ToListAsync();

                var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdStartAndEndDate(start_calendar, end_calendar, EmployeeId);

                var Holidays = holidays.Where(h => (h.holidayType == HolidayTypes.Parental || h.holidayType == HolidayTypes.Sick || h.holidayType == HolidayTypes.TimeOffWithoutSalary))
                        .Where(h => EF.Functions.DateDiffDay(h.HolidayStartDate, h.HolidayEndDate) > 30).ToList();

                int leaves = 0;

                Holidays.ForEach(p => {
                    var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate;
                    var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate);
                    
                    int? days = (int?)(end - start)?.TotalDays;
                    leaves = leaves + (days ?? 0);                   
                });
                var days =(int)(DateTime.Now - start_calendar).TotalDays - leaves;
                return days;
            }
            catch (Exception)
            {
                throw;

            }
        }
        private async Task<int> GetUsedHolidaysEmployee(DateTime startedDate,HolidaySettings setting, Guid EmployeeId)
        {
            try
            {
                DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                //var holidays = await _context.EmployeeHolidays
                //.Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                //.Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                //.Where(h => h.holidayType == HolidayTypes.AnnualHoliday)
                //.Where(h=> h.EmployeeId == EmployeeId)
                //.ToListAsync();
                var holidays = await _employeeHolidays.GetEmployeeHolidaysByEmployeeIdHolidayTypeStartAndEndDate(start_calendar, end_calendar, EmployeeId, (int)HolidayTypes.AnnualHoliday);


                int leaves = 0;

                holidays.ToList().ForEach(async p => {
                    var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate.ToDateTime3();
                    var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate.ToDateTime3());
                    //int? days = (int?)((end - start)?.TotalDays == 0 ? 1 : (end - start)?.TotalDays);
                    //days = days - await _commonHolidayDAL.CountPublicHolidays(start,end);
                    leaves = leaves + 0;
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
