using PemmexCommonLibs.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.API.Database.Entities
{
    public class ChangeEmployeeHardTargets
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int ChangeEmployeeHardTargetsTaskId { get; set; }
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string BusinessName { get; set; }
        public DateTime EmployeeDob { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        public string ContractualOrganization { get; set; }
        public string Grade { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public string JobFunction { get; set; }
        public string OrganizationIdentifier { get; set; }


        public int HardTargetsId { get; set; }
        public string HardTargetsName { get; set; }
        public string HardTargetsDescription { get; set; }
        public EMeasurementCriteria MeasurementCriteria { get; set; }
        public EPerformanceCriteria PerformanceCriteria { get; set; }
        public double Weightage { get; set; }
        public DateTime EvaluationDateTime { get; set; }
        public bool isActive { get; set; }

        public virtual BaseTask BaseTask { get; set; }
    }
}
