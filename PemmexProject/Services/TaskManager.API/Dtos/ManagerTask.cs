using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Dtos
{
    public class ManagerTask
    {
        public string oldManagerIdentifier { get; set; }
        public string newManagerIdentifier { get; set; }
        public string oldCostCenterIdentifier { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string newCostCenterIdentifier { get; set; }

    }
}
