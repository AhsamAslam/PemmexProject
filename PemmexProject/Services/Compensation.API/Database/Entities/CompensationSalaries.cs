using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Entities
{
    public class CompensationSalaries : AuditableEntity
    {
        [Key]
        public int CompensationSalaryId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public double TotalMonthlyPay { get; set; }
        public double one_time_bonus { get; set; }
        public double annual_bonus { get; set; }
        public DateTime IssuedDate { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }

    }
}
