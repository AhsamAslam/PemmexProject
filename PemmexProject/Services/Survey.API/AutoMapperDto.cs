using AutoMapper;
using Survey.API.Database.Entities;
using Survey.API.Dto;

namespace Survey.API
{
    public class AutoMapperDto : Profile
    {
        public AutoMapperDto()
        {
            //CreateMap<SurveyQuestion, SegmentQuestionsWeightageDto>().ReverseMap();
            CreateMap<OrganizationSurvey, OrganizationSurveyDto>().ReverseMap();
            CreateMap<Database.Entities.EmployeeSurvey, EmployeeSurveyDto>().ReverseMap();

        }
    }
}
