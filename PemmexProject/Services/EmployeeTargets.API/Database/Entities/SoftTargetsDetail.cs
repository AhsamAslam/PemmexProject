using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class SoftTargetsDetail
    {
        [Key]
        public int SoftTargetsDetailId { get; set; }
        public int SoftTargetsId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
    }
}
