using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Repositories.Interface
{
    public interface IChangeHoliday
    {
        Task<IEnumerable<ChangeHoliday>> GetChangeHoliday();
        Task<IEnumerable<ChangeHoliday>> GetChangeHolidayById(int Id);
        Task<ChangeHoliday> AddChangeHoliday(ChangeHoliday ChangeHoliday);
        Task<ChangeHoliday> UpdateChangeHoliday(ChangeHoliday ChangeHoliday);
        Task<int> DeleteChangeHoliday(int Id);
    }
}
