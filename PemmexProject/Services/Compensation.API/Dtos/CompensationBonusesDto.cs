using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Dtos
{
    public class CompensationBonusesDto
    {
        public int compensationBonusId { get; set; }
        public double bonusValue { get; set; }
        public DateTime effectiveDate { get; set; }
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string employeeIdentifier { get; set; }
    }
}
