using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class ChangeTitle
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int TitleTaskId { get; set; }
        public string oldTitle { get; set; }
        public string NewTitle { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
        public virtual BaseTask BaseTask { get; set; }
    }
}
