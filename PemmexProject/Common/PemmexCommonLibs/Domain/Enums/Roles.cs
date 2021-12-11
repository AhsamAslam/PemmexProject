using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Enums
{
    public enum Roles
    {
        [Description("User")]
        user,
        [Description("Manager")]
        manager,
        [Description("Group HR")]
        group_hr,
        [Description("Admin")]
        admin,
        [Description("BU HR")]
        bu_hr
    }
}
