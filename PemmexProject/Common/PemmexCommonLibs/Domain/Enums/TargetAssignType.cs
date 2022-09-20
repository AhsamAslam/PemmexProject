using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Enums
{
    public enum TargetAssignType
    {
        [Description("OrganizationBasedTarget")]
        OrganizationBasedTarget,
        [Description("BusinessUnitBasedTarget")]
        BusinessUnitBasedTarget,
        [Description("TeamBasedTarget")]
        TeamBasedTarget,
        [Description("EmployeeBasedTarget")]
        EmployeeBasedTarget,

    }
}
