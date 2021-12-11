using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Entities
{
    public class CompensationBonuses
    {
        [Key]
        public int compensationBonusId { get; set; }
        public double bonusValue { get; set; }
        public DateTime effectiveDate { get; set; }
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string employeeIdentifier { get; set; }
    }
}
