using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Common.Dtos
{
    public class TaskEntity
    {
        public int TaskId { get; set; }
        public string RequestedByIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
    }
    public class CompensationTaskEntity
    {
        public int CompensationTaskId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public double AppliedSalary { get; set; }
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
    public class GradeEntity
    {
        public int GradeTaskId { get; set; }
        public string oldGrade { get; set; }
        public string newGrade { get; set; }
        public string EmployeeIdentifier { get; set; }
    }
    public class ManagerEntity
    {
        public int ManagerTaskId { get; set; }
        public string oldManagerIdentifier { get; set; }
        public string newManagerIdentifier { get; set; }
        public string oldCostCenterIdentifier { get; set; }
        public string newCostCenterIdentifier { get; set; }
        public string EmployeeIdentifier { get; set; }

    }
    public class TitleEntity
    {
        public int TitleTaskId { get; set; }
        public string oldTitle { get; set; }
        public string NewTitle { get; set; }
        public string EmployeeIdentifier { get; set; }


    }
}
