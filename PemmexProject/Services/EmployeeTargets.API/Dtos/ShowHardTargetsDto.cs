using PemmexCommonLibs.Domain.Enums;

namespace EmployeeTargets.API.Dtos
{
    public class ShowHardTargetsDto
    {
        public int HardTargetsId { get; set; }
        public string HardTargetsName { get; set; }
        public string HardTargetsDescription { get; set; }
        public EMeasurementCriteria MeasurementCriteria { get; set; }
        public double MeasurementCriteriaResult { get; set; }
        public double Weightage { get; set; }
        public string OrganizationIdentifier { get; set; }
        public DateTime EvaluationDateTime { get; set; }
        public TargetAssignType TargetType { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
    }
}
