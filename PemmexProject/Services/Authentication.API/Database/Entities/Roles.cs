using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Database.Entities
{
    public class Roles: AuditableEntity
    {
        [Key]
        public int roleId { get; set; }
        public string roleName { get; set; }
        public string OrganizationIdentifier { get;set; }
        public ICollection<Screens> Screens { get; set; }
    }
}
