using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Enums
{
    public enum EPerformanceCriteria
    {
        [Description("Below")]
        Below,
        [Description("Extremely Below")]
        ExtremelyBelow,
        [Description("Meets")]
        Meets,
        [Description("Exceeds")]
        Exceeds,
        [Description("Consistently Exceeds")]
        ConsistentlyExceeds,
    }
}
