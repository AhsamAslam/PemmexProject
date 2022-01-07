using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class BonusTask
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int BonusTaskId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public double one_time_bonus { get; set; }
        public double salary { get; set; }
        public virtual BaseTask BaseTask { get; set; }

    }
}
