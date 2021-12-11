using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetcostCenterIdentifiers
{
    public class GetcostCenterIdentifiersQuery : IRequest<List<CostCenterResponse>>
    {
        public string organizationIdentifier { get; set; }
    }
    public class GetcostCenterIdentifiersQueryHandler : IRequestHandler<GetcostCenterIdentifiersQuery, List<CostCenterResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetcostCenterIdentifiersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CostCenterResponse>> Handle(GetcostCenterIdentifiersQuery request, CancellationToken cancellationToken)
        {
            var o = await _context.CostCenters
                .Where(o => o.businessIdentifier == request.organizationIdentifier)
                .ToListAsync(cancellationToken: cancellationToken);

            return _mapper.Map<List<Entities.CostCenter>, List<CostCenterResponse>>(o);
        }
    }
}
