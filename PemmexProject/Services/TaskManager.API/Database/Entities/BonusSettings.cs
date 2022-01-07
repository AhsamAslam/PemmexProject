using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Database.Entities
{
    public class BonusSettings : AuditableEntity
    {
        [Key]
        public int BonusSettingsId { get; set; }
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public double limit_percentage { get; set; }
    }
}
