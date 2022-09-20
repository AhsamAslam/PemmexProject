using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Queries.GetPerfromanceBudgetPlanning
{
    public class GetPerfromanceBudgetPlanningQuery : IRequest<PerfromanceBudgetPlanning>
    {
        public string organizationIdentifier { get; set; }
    }
    public class GetPerfromanceBudgetPlanningQueryHandeler : IRequestHandler<GetPerfromanceBudgetPlanningQuery, PerfromanceBudgetPlanning>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;

        public GetPerfromanceBudgetPlanningQueryHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<PerfromanceBudgetPlanning> Handle(GetPerfromanceBudgetPlanningQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var budgetPlanning = await _performanceBudget.GetCreatePerfromanceBudgetPlanning(request.organizationIdentifier);
                return budgetPlanning;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
