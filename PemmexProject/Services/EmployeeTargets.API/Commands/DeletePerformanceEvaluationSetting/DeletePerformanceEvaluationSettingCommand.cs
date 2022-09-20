using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Commands.DeletePerformanceEvaluationSetting
{
    public class DeletePerformanceEvaluationSettingCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }
    public class DeletePerformanceEvaluationSettingCommandHandeler : IRequestHandler<DeletePerformanceEvaluationSettingCommand,bool>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        public DeletePerformanceEvaluationSettingCommandHandeler(IPerformanceBudgetRepository performanceBudget)
        {
            _performanceBudget = performanceBudget;
        }
        public async Task<bool> Handle(DeletePerformanceEvaluationSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _performanceBudget.DeletePerformanceEvaluationSetting(request.Id);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
