using Holidays.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Interface
{
    public interface IHolidaySettings
    {
        Task<IEnumerable<HolidaySettings>> GetHolidaySettings();
        Task<IEnumerable<HolidaySettings>> GetHolidaySettingsById(string Id);
        Task<HolidaySettings> AddHolidaySetting(HolidaySettings HolidaySettings);
        Task<IEnumerable<HolidaySettings>> AddHolidaySettings(List<HolidaySettings> HolidaySettings);
        Task<HolidaySettings> UpdateHolidaySetting(HolidaySettings HolidaySettings);
        Task<IEnumerable<HolidaySettings>> UpdateHolidaySettings(List<HolidaySettings> HolidaySettings);
        Task<int> DeleteHolidaySettings(int Id);
    }
}
