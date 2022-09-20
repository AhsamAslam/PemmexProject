using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Queries.GetPerformanceEvaluationSettings
{
    public class GetPerformanceEvaluationSettingsQuery : IRequest<List<PerformanceEvaluationSettings>>
    {
        public string organizationIdentifier { get; set; }
    }
    public class GetPerformanceEvaluationSettingsQueryHandeler : IRequestHandler<GetPerformanceEvaluationSettingsQuery, List<PerformanceEvaluationSettings>>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;

        public GetPerformanceEvaluationSettingsQueryHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<List<PerformanceEvaluationSettings>> Handle(GetPerformanceEvaluationSettingsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var performanceEvaluationsSettings = await _performanceBudget.ShowPerformanceEvaluationSetting(request.organizationIdentifier);
                return performanceEvaluationsSettings.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
