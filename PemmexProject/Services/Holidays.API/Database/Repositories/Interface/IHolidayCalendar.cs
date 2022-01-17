using Holidays.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Interface
{
    public interface IHolidayCalendar
    {
        Task<IEnumerable<HolidayCalendar>> GetHolidayCalendar(string CountrCode, int year);
        Task<IEnumerable<HolidayCalendar>> GetHolidayCalendarById(int Id);
        Task<IEnumerable<HolidayCalendar>> AddHolidayCalendar(List<HolidayCalendar> HolidayCalendar);
        Task<HolidayCalendar> UpdateHolidayCalendar(HolidayCalendar HolidayCalendar);
        Task<int> DeleteHolidayCalendar(int Id);
        Task DeleteHolidayCalendarList(List<HolidayCalendar> HolidayCalendar);
    }
}
