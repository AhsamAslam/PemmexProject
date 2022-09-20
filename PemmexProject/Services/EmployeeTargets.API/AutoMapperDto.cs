using AutoMapper;
using EmployeeTargets.API.Commands.CreatePerformanceEvaluationSetting;
using EmployeeTargets.API.Commands.CreatePerformanceEvaluationSummary;
using EmployeeTargets.API.Commands.CreatePerfromanceBudgetPlanning;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Dtos;

namespace EmployeeTargets.API
{
    public class AutoMapperDto : Profile
    {
        public AutoMapperDto()
        {
            CreateMap<SoftTargets, SoftTargetsDto>().ReverseMap();
            CreateMap<HardTargets, HardTargetsDto>().ReverseMap();
            CreateMap<CreatePerfromanceBudgetPlanningCommand, PerfromanceBudgetPlanning>().ReverseMap();
            CreateMap<CreatePerformanceEvaluationSummaryRequest, PerformanceEvaluationSummary>().ReverseMap();
            CreateMap<CreatePerformanceEvaluationSettingCommand, PerformanceEvaluationSettings>().ReverseMap();

        }
    }
}
