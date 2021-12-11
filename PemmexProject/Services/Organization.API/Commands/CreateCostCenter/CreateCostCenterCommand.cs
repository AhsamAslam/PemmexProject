using AutoMapper;
using MediatR;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Commands.CreateCostCenter
{
    public class CreateCostCenterCommand : IRequest<int>
    {
        public CostCenterRequest costCenterRequest { get; set; }
    }

    public class CreateCostCenterCommandHandeler : IRequestHandler<CreateCostCenterCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CreateCostCenterCommandHandeler(IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateCostCenterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var e = _mapper.Map<CostCenterRequest, CostCenter>(request.costCenterRequest);
                _context.CostCenters.Add(e);
                await _context.SaveChangesAsync(cancellationToken);
                return e.CostCenterId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
