using AutoMapper;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Commands.UpdateHardTargetsMeasurementCriteriaResult
{
    public class UpdateHardTargetsMeasurementCriteriaResultCommand : IRequest
    {
        public int Id { get; set; }
        public double MeasurementCriteriaResult { get; set; }
    }

    public class UpdateHardTargetsMeasurementCriteriaResultCommandHandeler : IRequestHandler<UpdateHardTargetsMeasurementCriteriaResultCommand>
    {
        private readonly IHardTargetsRepository _targets;
        private readonly IMapper _mapper;
        public UpdateHardTargetsMeasurementCriteriaResultCommandHandeler(IHardTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateHardTargetsMeasurementCriteriaResultCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _targets.UpdateHardTargetsMeasurementCriteriaResult(request.Id, request.MeasurementCriteriaResult);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
