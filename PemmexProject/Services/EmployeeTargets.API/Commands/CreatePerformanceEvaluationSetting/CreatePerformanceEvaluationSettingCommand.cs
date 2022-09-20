using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;
using System.Text.Json.Serialization;

namespace EmployeeTargets.API.Commands.CreatePerformanceEvaluationSetting
{
    public class CreatePerformanceEvaluationSettingCommand: IRequest
    {
        
        public double minimumPercentage { get; set; }
        public double targetPercentage { get; set; }
        public double maximumPercentage { get; set; }

        //[JsonIgnore]
        public string organizationIdentifier { get; set; }
    }
    public class CreatePerformanceEvaluationSettingCommandHandeler : IRequestHandler<CreatePerformanceEvaluationSettingCommand>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;
        public CreatePerformanceEvaluationSettingCommandHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreatePerformanceEvaluationSettingCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var performanceEvaluationSetting = _mapper.Map<PerformanceEvaluationSettings>(request);
                await _performanceBudget.CreatePerformanceEvaluationSetting(performanceEvaluationSetting);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
