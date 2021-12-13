using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class ChangeHolidayRepository : IChangeHoliday
    {
        public Task<ChangeHoliday> AddChangeHoliday(ChangeHoliday ChangeHoliday)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteChangeHoliday(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeHoliday>> GetChangeHoliday()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ChangeHoliday>> GetChangeHolidayById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ChangeHoliday> UpdateChangeHoliday(ChangeHoliday ChangeHoliday)
        {
            throw new NotImplementedException();
        }
    }
}
