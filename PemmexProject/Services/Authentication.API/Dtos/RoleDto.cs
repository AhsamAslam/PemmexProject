using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Dtos
{
    public class RoleDto
    {
        public Roles role { get; set; }
        public Guid UserId { get; set; }
    }
}
