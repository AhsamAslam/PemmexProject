using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PemmexCommonLibs.Domain.Common;

namespace Organization.API.Entities
{
    public class Business : AuditableEntity
    {
        [Key]
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessIdentifier { get; set; }
        public bool IsActive { get; set; }  
        public string ParentBusinessId { get; set; }
        public string OrganizationCountry { get; set; }
        public string FileName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
