using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Database.Entities
{
    public class ChangeCompensation
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int CompensationTaskId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public double AppliedSalary { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double NewAdditionalAgreedPart { get; set; }
        public double TotalMonthlyPay { get; set; }
        public double NewTotalMonthlyPay { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public virtual BaseTask BaseTask { get; set; }


    }
}
