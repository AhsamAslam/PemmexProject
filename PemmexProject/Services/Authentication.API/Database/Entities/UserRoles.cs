using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Database.Entities
{
    public class UserRoles:AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public User User { get; set; }

        public ICollection<Roles> Roles { get; set; }
    }
}
