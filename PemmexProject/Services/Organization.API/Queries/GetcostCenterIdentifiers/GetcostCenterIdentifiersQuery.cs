using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Context;
using Organization.API.Database.Entities;
using Organization.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetcostCenterIdentifiers
{
    public class GetcostCenterIdentifiersQuery : IRequest<List<CostCenterResponse>>
    {
        public string businessIdentifier { get; set; }
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
            try
            {
                var o = await _context.CostCenters
                        .Where(o => o.businessIdentifier == request.businessIdentifier)
                        .ToListAsync(cancellationToken: cancellationToken);
                return _mapper.Map<List<CostCenter>, List<CostCenterResponse>>(o);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
