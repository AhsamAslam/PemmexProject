using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Enums
{
    public enum HolidayStatus
    {
        [Description("Planned")]
        Planned,
        [Description("Availed")]
        Availed,
        [Description("Approved")]
        Approved
            
    }
}
