using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Queries.GetPerformanceEvaluationSummary
{
    public class GetPerformanceEvaluationSummaryQuery : IRequest<List<PerformanceEvaluationSummary>>
    {
        public string organizationIdentifier { get; set; }
    }
    public class GetPerformanceEvaluationSummaryQueryHandeler : IRequestHandler<GetPerformanceEvaluationSummaryQuery, List<PerformanceEvaluationSummary>>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;

        public GetPerformanceEvaluationSummaryQueryHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<List<PerformanceEvaluationSummary>> Handle(GetPerformanceEvaluationSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var performanceEvaluations = await _performanceBudget.GetPerformanceEvaluationSummary(request.organizationIdentifier);
                return performanceEvaluations.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
