using Holidays.API.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holidays.API.Database.Interfaces
{
    public interface IHolidaySettingRepository
    {
        public Task<IEnumerable<HolidaySettings>> GetOrganizationHolidaySettings(string organizationIdentifier);

    }
}
