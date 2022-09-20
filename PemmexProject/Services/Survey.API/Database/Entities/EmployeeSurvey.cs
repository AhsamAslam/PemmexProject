using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.API.Database.Entities
{
    public class EmployeeSurvey : AuditableEntity
    {
        [Key]
        public int employeeSurveyId { get; set; }
        public string employeeIdentifier { get; set; }
        public string employeeName { get; set; }
        public string managerIdentifier { get; set; }
        public string managerName { get; set; }
        public string businessIdentifier { get; set; }
        public bool isSurveySubmitted { get; set; }
        public int organizationSurveyId { get; set; }
        [NotMapped]
        public Guid segmentId { get; set; }
        [NotMapped]
        public string segmentName { get; set; }
        [NotMapped]
        public Guid questionId { get; set; }
        [NotMapped]
        public string questionName { get; set; }
    }
}
