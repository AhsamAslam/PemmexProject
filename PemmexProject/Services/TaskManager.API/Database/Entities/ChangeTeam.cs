using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class ChangeTeam
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int TeamTaskId { get; set; }
        public string managerIdentifier { get; set; }
        public string managerName { get; set; }
        public string oldCostCenterIdentifier { get; set; }
        public string oldCostCenterName { get; set; }
        public string newCostCenterIdentifier { get; set; }
        public string newCostCenterName { get; set; }
        public string organizationIdentifier { get; set; }
        public string oldbusinessIdentifier { get; set; }
        public string newbusinessIdentifier { get; set; }

        public virtual BaseTask BaseTask { get; set; }
    }
}
