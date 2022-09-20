using PemmexCommonLibs.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.API.Database.Entities
{
    public class OrganizationSurvey : AuditableEntity
    {
       
        [Key]
        public int organizationSurveyId { get; set; }
        public string organizationIdentifier { get; set; }
        public DateTime organizationSurveyDate { get; set; }
        public virtual ICollection<OrganizationSurveyQuestion> OrganizationSurveyQuestion { get; set; }
    }
}
