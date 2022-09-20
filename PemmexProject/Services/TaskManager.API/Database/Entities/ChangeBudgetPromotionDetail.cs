using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class ChangeBudgetPromotionDetail
    {
        [Key]
        public int ChangeBudgetPromotionDetailId {get;set;}
        [ForeignKey("ChangeBudgetPromotion")]
        public int BudgetTaskId { get; set; }
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentTitle { get; set; }
        public string NewTitle { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public string CurrentGrade { get; set; }
        public string NewGrade { get; set; }
        public string JobFunction { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double TotalCurrentSalary { get; set; }
        public double mandatoryPercentage { get; set; }
        public double IncreaseInPercentage { get; set; }
        public double NewBaseSalary { get; set; }
        public double NewTotalSalary { get; set; }
        public string currencyCode { get; set; }
        public double IncreaseInCurrency { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public virtual ChangeBudgetPromotion ChangeBudgetPromotion { get; set; }

    }
}
