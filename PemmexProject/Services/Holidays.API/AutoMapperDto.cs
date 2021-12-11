using System;
using AutoMapper;
using Holidays.API.Commands.SaveHolidays;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;

namespace Holidays.API
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            
            CreateMap<EmployeeHolidays,HolidayEmployeeCounter>().ReverseMap();
            CreateMap<HolidaySettings, HolidaySettingsDto>().ReverseMap();
            CreateMap<EmployeeHolidays, SaveHolidayCommand>().ReverseMap();
            CreateMap<EmployeeHolidays, TakenHolidayDto>().ReverseMap();
            CreateMap<HolidayCalendar, HolidayCalendarDto>().ReverseMap();
        }

    }
}
