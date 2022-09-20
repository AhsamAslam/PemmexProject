using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Database.Entities
{
    public class CostCenter : AuditableEntity
    {
        [Key]
        public int CostCenterId { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string ParentCostCenterIdentifier { get; set; }
        public string businessIdentifier { get; set; }

    }
}
