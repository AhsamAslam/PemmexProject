using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Commands.CreatePerfromanceBudgetPlanning
{
    public class CreatePerfromanceBudgetPlanningCommand:IRequest
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double companyProfitabilityMultiplier { get; set; }
        public DateTime bonusPayoutDate { get; set; }
        public string organizationIdentifier { get; set; }

    }
    public class CreatePerfromanceBudgetPlanningCommandHandeler : IRequestHandler<CreatePerfromanceBudgetPlanningCommand>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;
        public CreatePerfromanceBudgetPlanningCommandHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreatePerfromanceBudgetPlanningCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var budgetPlanning = _mapper.Map<PerfromanceBudgetPlanning>(request);
                await _performanceBudget.CreatePerfromanceBudgetPlanning(budgetPlanning);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
