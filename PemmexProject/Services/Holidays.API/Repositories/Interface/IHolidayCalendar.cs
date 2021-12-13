using Holidays.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Interface
{
    public interface IHolidayCalendar
    {
        Task<IEnumerable<HolidayCalendar>> GetHolidayCalendar();
        Task<IEnumerable<HolidayCalendar>> GetHolidayCalendarById(int Id);
        Task<HolidayCalendar> AddHolidayCalendar(HolidayCalendar HolidayCalendar);
        Task<HolidayCalendar> UpdateHolidayCalendar(HolidayCalendar HolidayCalendar);
        Task<int> DeleteHolidayCalendar(int Id);
    }
}
