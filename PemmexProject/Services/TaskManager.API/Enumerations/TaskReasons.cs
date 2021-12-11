using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Enumerations
{
    public enum TaskReasons
    {
        PromotionWithoutCompensation,
        PromotionWithCompensation,
        PromotionWithGrade,
        OrganizationOrJobChange,
        TitleChangeOnly,
        Other
    }
}
