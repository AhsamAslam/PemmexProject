using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using MediatR;
using PemmexCommonLibs.Domain.Enums;

namespace EmployeeTargets.API.Commands.CreateSoftTarget
{
    public class CreateSoftTargetCommand : IRequest
    {
        //public List<SoftDto> SoftDto { get; set; }
        public SoftTargetsDto SoftTargets { get; set; }
       

    }
    public class CreateSoftTargetCommandHandeler : IRequestHandler<CreateSoftTargetCommand>
    {
        private readonly ISoftTargetsRepository _targets;
        private readonly IMapper _mapper;
        public CreateSoftTargetCommandHandeler(ISoftTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateSoftTargetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var target = _mapper.Map<List<SoftTargets>>(request.SoftTargets);
                await _targets.CreateSoftTargets(request.SoftTargets);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
