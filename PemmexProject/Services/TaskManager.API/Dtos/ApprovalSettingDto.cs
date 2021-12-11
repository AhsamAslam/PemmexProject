using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Dtos
{
    public class ApprovalSettingDto
    {
        public string OrganizationIdentifier { get; set; }
        public TaskType taskType { get; set; }
        public List<ApprovalSettingDetailsDto> approvalSettingDetails { get; set; }
    }
}
