using Holidays.API.Database.Entities;
using Holidays.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Repositories.Repository
{
    public class HolidaySettingsRepository : IHolidaySettings
    {
        public Task<HolidaySettings> AddHolidaySettings(HolidaySettings HolidaySettings)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteHolidaySettings(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HolidaySettings>> GetHolidaySettings()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HolidaySettings>> GetHolidaySettingsById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<HolidaySettings> UpdateHolidaySettings(HolidaySettings HolidaySettings)
        {
            throw new NotImplementedException();
        }
    }
}
