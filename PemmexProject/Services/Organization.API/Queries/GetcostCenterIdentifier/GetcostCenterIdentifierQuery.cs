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

namespace Organization.API.Queries.GetcostCenterIdentifier
{
    public class GetcostCenterIdentifierQuery : IRequest<CostCenterResponse>
    {
        public string costCenterIdentifier { get; set; }
    }
    public class GetcostCenterIdentifierQueryHandler : IRequestHandler<GetcostCenterIdentifierQuery, CostCenterResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetcostCenterIdentifierQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CostCenterResponse> Handle(GetcostCenterIdentifierQuery request, CancellationToken cancellationToken)
        {
            var o = await _context.CostCenters
                .Where(o => o.CostCenterIdentifier == request.costCenterIdentifier)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return _mapper.Map<Entities.CostCenter, CostCenterResponse>(o);
        }
    }
}
