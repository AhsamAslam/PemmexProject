using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
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
        
        private readonly ICostCenter _costCenter;
        private readonly IMapper _mapper;

        public GetcostCenterIdentifierQueryHandler(ICostCenter costCenter, IMapper mapper)
        {
            _costCenter = costCenter;
            _mapper = mapper;
        }

        public async Task<CostCenterResponse> Handle(GetcostCenterIdentifierQuery request, CancellationToken cancellationToken)
        {
            //var o = await _context.CostCenters
            //    .Where(o => o.CostCenterIdentifier == request.costCenterIdentifier)
            //    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            var o = await _costCenter.GetCostCenterByCostCenterIdentifier(request.costCenterIdentifier);

            return _mapper.Map<Entities.CostCenter, CostCenterResponse>(o);
        }
    }
}
