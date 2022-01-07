using AutoMapper;
using Compensation.API.Database.context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetOrganizationTotalSalaryCount
{
    public class GetOrganizationTotalSalaryCountQuery : IRequest<double>
    {
        public string organizationIdentifier { get; set; }
    }

    public class GetOrganizationTotalSalaryCountQueryHandeler : IRequestHandler<GetOrganizationTotalSalaryCountQuery, double>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetOrganizationTotalSalaryCountQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<double> Handle(GetOrganizationTotalSalaryCountQuery request, CancellationToken cancellationToken)
        {
            var salary = await _context.Compensation
            .Where(c => c.organizationIdentifier == request.organizationIdentifier)
            .Select(o => o.TotalMonthlyPay)
            .ToListAsync();

            return salary.Sum();
        }
    }
}
