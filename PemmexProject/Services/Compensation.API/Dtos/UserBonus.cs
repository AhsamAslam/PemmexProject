using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Dtos
{
    public class UserBonus
    {
        public DateTime bonusDateTime { get; set; }
        public double bonusAmount { get; set; }
        public string EmployeeIdentifier { get; set; }
    }
}
