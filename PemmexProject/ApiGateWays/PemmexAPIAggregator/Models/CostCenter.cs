using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class CostCenter
    {
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string ParentCostCenterIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }
}
