using AutoMapper;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Database.Interfaces;
using EmployeeTargets.API.Dtos;
using MediatR;

namespace EmployeeTargets.API.Queries.ShowHardTargets
{
    public class ShowHardTargetsQuery : IRequest<List<ShowHardTargetsDto>>
    {
        public string employeeIdentifier { get; set; }
    }
    public class ShowHardTargetsQueryHandeler : IRequestHandler<ShowHardTargetsQuery, List<ShowHardTargetsDto>>
    {
        private readonly IHardTargetsRepository _targets;
        private readonly IMapper _mapper;

        public ShowHardTargetsQueryHandeler(IHardTargetsRepository targets, IMapper mapper)
        {
            _targets = targets;
            _mapper = mapper;
        }
        public async Task<List<ShowHardTargetsDto>> Handle(ShowHardTargetsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var targets = await _targets.ShowHardTargets(request.employeeIdentifier);
                //return _mapper.Map<List<HardTargets>, List<HardTargetsDto>>(targets.ToList());
                return targets.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
