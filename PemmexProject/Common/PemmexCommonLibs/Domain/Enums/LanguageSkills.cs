using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Enums
{
    public enum LanguageSkills
    {
        [Description("Basic")]
        Basic,
        [Description("Medium")]
        Medium,
        [Description("Proficient")]
        Proficient,
        [Description("Native")]
        Native
    }
}
