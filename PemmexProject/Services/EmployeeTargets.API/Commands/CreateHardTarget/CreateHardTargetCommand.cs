using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using MediatR;

namespace EmployeeTargets.API.Commands.CreateHardTarget
{
    public class CreateHardTargetCommand : IRequest
    {
        public HardTargetsDto HardTargets { get; set; }
    }

    public class CreateHardTargetCommandHandeler : IRequestHandler<CreateHardTargetCommand>
    {
        private readonly IHardTargetsRepository _targets;
        private readonly IMapper _mapper;
        public CreateHardTargetCommandHandeler(IHardTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateHardTargetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ///*var target = _mapper.Map<List<HardTargets>>(*/request.HardTargets);
               
                await _targets.CreateHardTargets(request.HardTargets);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
