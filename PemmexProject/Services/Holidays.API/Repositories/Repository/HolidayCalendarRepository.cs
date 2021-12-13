using Holidays.API.Database.Entities;
using Holidays.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Repository
{
    public class HolidayCalendarRepository : IHolidayCalendar
    {
        public Task<HolidayCalendar> AddHolidayCalendar(HolidayCalendar HolidayCalendar)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteHolidayCalendar(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HolidayCalendar>> GetHolidayCalendar()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HolidayCalendar>> GetHolidayCalendarById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<HolidayCalendar> UpdateHolidayCalendar(HolidayCalendar HolidayCalendar)
        {
            throw new NotImplementedException();
        }
    }
}
