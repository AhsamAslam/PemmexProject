using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class ChangeGrade 
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int GradeTaskId { get; set; }
        public string oldGrade { get; set; }
        public string newGrade { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
        public virtual BaseTask BaseTask { get; set; }
    }
}
