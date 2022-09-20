using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Queries.GetPerformanceEvaluationSummaryDetail
{
    public class GetPerformanceEvaluationSummaryDetailQuery : IRequest<List<PerformanceEvaluationSummary>>
    {
        public string[] employeeIdentifiers { get; set; }
    }

    public class GetPerformanceEvaluationSummaryDetailQueryHandeler : IRequestHandler<GetPerformanceEvaluationSummaryDetailQuery, List<PerformanceEvaluationSummary>>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;

        public GetPerformanceEvaluationSummaryDetailQueryHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<List<PerformanceEvaluationSummary>> Handle(GetPerformanceEvaluationSummaryDetailQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var performanceEvaluationsDetail = await _performanceBudget.GetPerformanceEvaluationSummaryDetail(request.employeeIdentifiers);
                return performanceEvaluationsDetail.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
