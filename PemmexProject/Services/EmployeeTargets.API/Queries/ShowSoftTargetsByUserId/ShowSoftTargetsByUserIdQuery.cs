using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using MediatR;

namespace EmployeeTargets.API.Queries.ShowSoftTargetsByUserId
{
    public class ShowSoftTargetsByUserIdQuery : IRequest<List<ShowSoftTargetsDto>>
    {
        public string userId { get; set; }
        public string employeeIdentifier { get; set; }
    }
    public class ShowSoftTargetsByUserIdQueryHandeler : IRequestHandler<ShowSoftTargetsByUserIdQuery, List<ShowSoftTargetsDto>>
    {
        private readonly ISoftTargetsRepository _targets;
        private readonly IMapper _mapper;

        public ShowSoftTargetsByUserIdQueryHandeler(ISoftTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<List<ShowSoftTargetsDto>> Handle(ShowSoftTargetsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var targets = await _targets.ShowSoftTargetsByUserId(request.userId, request.employeeIdentifier);
                return targets.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
