using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class HardTargetsDetail
    {
        [Key]
        public int HardTargetsDetailId { get; set; }
        public int HardTargetsId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
    }
}
