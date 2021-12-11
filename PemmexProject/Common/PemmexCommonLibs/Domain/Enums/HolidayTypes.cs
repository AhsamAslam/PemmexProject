using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Enumerations
{
    public enum HolidayTypes
    {
        [Description("Parental")]
        Parental,
        [Description("Sick")]
        Sick,
        [Description("AgreedAnnual")]
        AgreedAnnual,
        [Description("TimeOffWithoutSalary")]
        TimeOffWithoutSalary,
        [Description("OtherTypeHoliday")]
        OtherTypeHoliday,
        [Description("AnnualHoliday")]
        AnnualHoliday

    }
}
