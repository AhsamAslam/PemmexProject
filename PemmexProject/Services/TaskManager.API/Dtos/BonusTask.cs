using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Dtos
{
    public class BonusTask
    {
        public int BonusTaskId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public double one_time_bonus { get; set; }
        public double salary { get; set; }
    }
}
