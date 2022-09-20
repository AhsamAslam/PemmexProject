using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Database.Entities
{
    public class EmployeeBonuses
    {
        [Key]
        public int EmployeeBonusId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public double BonusValue { get; set; }
        public DateTime EffectiveDate { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
