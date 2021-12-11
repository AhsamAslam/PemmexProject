using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Database.Entities
{
    public class OrganizationApprovalSettingDetail
    {
        [Key]
        public int Id { get; set; }
        public int rankNo { get; set; }
        public OrganizationApprovalStructure ManagerType { get; set; }
        public string EmployeeIdentifier { get; set; }

        [ForeignKey("OrganizationApprovalSettings")]
        public int OrganizationApprovalSettingsId { get; set; }
    }
}
