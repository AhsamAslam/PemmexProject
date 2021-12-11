using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetCompensation
{
    public class GetCompensationQuery : IRequest<CompensationDto>
    {
        public string employeeIdentifier { get; set; }
    }

    public class GetCompensationQueryHandeler : IRequestHandler<GetCompensationQuery, CompensationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetCompensationQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<CompensationDto> Handle(GetCompensationQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Compensation
                .Where(e => e.EmployeeIdentifier == request.employeeIdentifier)
                .OrderByDescending(c => c.EffectiveDate).Take(1)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Database.Entities.Compensation, CompensationDto>(employee);
        }
    }
}
