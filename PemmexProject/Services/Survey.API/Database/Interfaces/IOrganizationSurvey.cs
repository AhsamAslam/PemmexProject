using Survey.API.Database.Entities;
using Survey.API.Dto;

namespace Survey.API.Database.Interfaces
{
    public interface IOrganizationSurvey
    {
        Task<int> AddOrganizationSurvey(OrganizationSurveyDto organizationSurveys);
    }
}
