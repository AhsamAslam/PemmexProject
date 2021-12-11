using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class ChangeManager
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int ManagerTaskId { get; set; }
        public string oldManagerIdentifier { get; set; }
        public string newManagerIdentifier { get; set; }
        public string oldCostCenterIdentifier { get; set; }
        public string newCostCenterIdentifier { get; set; }
        public string EmployeeIdentifier { get; set; }

        public virtual BaseTask BaseTask { get; set; }

    }
}
