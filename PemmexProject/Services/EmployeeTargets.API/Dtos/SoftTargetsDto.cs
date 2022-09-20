using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;

namespace EmployeeTargets.API.Dtos
{
    public class SoftTargetsDto
    {
        public int SoftTargetsId { get; set; }
        public string SoftTargetsName { get; set; }
        public string SoftTargetsDescription { get; set; }
        public EPerformanceCriteria PerformanceCriteria { get; set; }
        public string OrganizationIdentifier { get; set; }
        public DateTime EvaluationDateTime { get; set; }
        public List<SoftTargetsDetailDto> SoftTargetsDetail { get; set; }
        public TargetAssignType TargetType { get; set; }

    }
    public class SoftTargetsDetailDto
    {
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
    }
}
