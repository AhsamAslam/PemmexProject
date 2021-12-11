using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Database.Entities
{
    public class HolidaySettings:AuditableEntity
    {
        [Key]
        public int HolidaySettingsId { get; set; }
        public Guid HolidaySettingsIdentitfier { get; set; }

        public string OrganizationIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public DateTime HolidayCalendarYear { get; set; }
        public int MaximumLimitHolidayToNextYear { get; set; }
    }
}
