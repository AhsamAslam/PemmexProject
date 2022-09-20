using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using MediatR;

namespace EmployeeTargets.API.Queries.ShowSoftTargets
{
    public class ShowSoftTargetQuery : IRequest<List<ShowSoftTargetsDto>>
    {
        public string employeeIdentifier { get; set; }
    }
    public class ShowSoftTargetQueryHandeler : IRequestHandler<ShowSoftTargetQuery, List<ShowSoftTargetsDto>>
    {
        private readonly ISoftTargetsRepository _targets;
        private readonly IMapper _mapper;

        public ShowSoftTargetQueryHandeler(ISoftTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<List<ShowSoftTargetsDto>> Handle(ShowSoftTargetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var targets = await _targets.ShowSoftTargets(request.employeeIdentifier);
                return targets.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
