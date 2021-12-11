using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Database.Entities
{
    public class OrganizationApprovalSettings:AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string OrganizationIdentifier { get; set; }
        public TaskType taskType { get; set; }
        public ICollection<OrganizationApprovalSettingDetail> organizationApprovalSettingDetails { get; set; }

    }
}
