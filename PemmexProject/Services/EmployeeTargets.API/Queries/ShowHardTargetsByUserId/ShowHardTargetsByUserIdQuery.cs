using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using MediatR;

namespace EmployeeTargets.API.Queries.ShowHardTargetsByUserId
{
    public class ShowHardTargetsByUserIdQuery : IRequest<List<ShowHardTargetsDto>>
    {
        public string userId { get; set; }
        public string employeeIdentifier { get; set; }
    }
    public class ShowHardTargetsByUserIdQueryHandeler : IRequestHandler<ShowHardTargetsByUserIdQuery, List<ShowHardTargetsDto>>
    {
        private readonly IHardTargetsRepository _targets;
        private readonly IMapper _mapper;

        public ShowHardTargetsByUserIdQueryHandeler(IHardTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<List<ShowHardTargetsDto>> Handle(ShowHardTargetsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var targets = await _targets.ShowHardTargetsByUserId(request.userId, request.employeeIdentifier);
                return targets.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
