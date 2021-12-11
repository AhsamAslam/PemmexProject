using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Database.Entities
{
    public class Compensation : AuditableEntity
    {
        [Key]
        public int CompensationId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public double TotalMonthlyPay { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
}
