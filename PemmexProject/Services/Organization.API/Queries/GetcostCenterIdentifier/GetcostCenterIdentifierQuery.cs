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
            try
            {
                var o = await _context.CostCenters
                       .Where(o => o.CostCenterIdentifier == request.costCenterIdentifier)
                       .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return _mapper.Map<CostCenter, CostCenterResponse>(o);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
