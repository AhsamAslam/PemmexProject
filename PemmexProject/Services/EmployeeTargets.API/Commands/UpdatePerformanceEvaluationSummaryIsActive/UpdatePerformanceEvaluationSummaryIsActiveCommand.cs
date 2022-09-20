using AutoMapper;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Commands.UpdatePerformanceEvaluationSummaryIsActive
{
    public class UpdatePerformanceEvaluationSummaryIsActiveCommand : IRequest
    {
        public List<string> employeeIdentifier { get; set; }
    }

    public class UpdatePerformanceEvaluationSummaryIsActiveCommandHandeler : IRequestHandler<UpdatePerformanceEvaluationSummaryIsActiveCommand>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        public UpdatePerformanceEvaluationSummaryIsActiveCommandHandeler(IPerformanceBudgetRepository performanceBudget)
        {
            _performanceBudget = performanceBudget;
        }
        public async Task<Unit> Handle(UpdatePerformanceEvaluationSummaryIsActiveCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _performanceBudget.UpdatePerformanceEvaluationSummaryIsActive(request.employeeIdentifier.ToArray());
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
