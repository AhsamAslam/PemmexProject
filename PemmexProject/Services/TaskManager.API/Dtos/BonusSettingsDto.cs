using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Dtos
{
    public class BonusSettingsDto
    {
        public int BonusSettingsId { get; set; }
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public double limit_percentage { get; set; }
    }
}
