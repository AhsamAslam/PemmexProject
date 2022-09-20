using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Commands.CreatePerformanceEvaluationSummary
{
    public class CreatePerformanceEvaluationSummaryCommand : IRequest
    {
        public List<CreatePerformanceEvaluationSummaryRequest> performanceEvaluationSummaryRequest { get; set; }
    }

    public class CreatePerformanceEvaluationSummaryRequest
    {
        public string name { get; set; }
        public string title { get; set; }
        public string country { get; set; }
        public string grade { get; set; }
        public string jobFunction { get; set; }
        public double totalSalary { get; set; }
        public double bonusPercentage { get; set; }
        public double bonusAmount { get; set; }
        public double resultedBonusPercentage { get; set; }
        public double resultedBonusAmountBeforeMultiplier { get; set; }
        public double performanceMultiplier { get; set; }
        public double resultedBonusAmountAfterMultiplier { get; set; }
        public double companyProfitabilityMultiplier { get; set; }
        public double finalBonusAmount { get; set; }
        public DateTime bonusPayoutDate { get; set; }
        public string employeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string managerIdentifier { get; set; }
    }

    public class CreatePerformanceEvaluationSummaryCommandHandeler : IRequestHandler<CreatePerformanceEvaluationSummaryCommand>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;
        public CreatePerformanceEvaluationSummaryCommandHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreatePerformanceEvaluationSummaryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var performanceEvaluation = _mapper.Map<List<PerformanceEvaluationSummary>>(request.performanceEvaluationSummaryRequest);
                await _performanceBudget.CreatePerformanceEvaluationSummary(performanceEvaluation);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
