using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Dtos
{
    public class TeamTask
    {
        public int TeamTaskId { get; set; }
        public string managerIdentifier { get; set; }
        public string oldCostCenterIdentifier { get; set; }
        public string newCostCenterIdentifier { get; set; }
    }
}
