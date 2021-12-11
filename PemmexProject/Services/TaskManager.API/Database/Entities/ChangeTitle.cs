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
        public virtual BaseTask BaseTask { get; set; }
    }
}
