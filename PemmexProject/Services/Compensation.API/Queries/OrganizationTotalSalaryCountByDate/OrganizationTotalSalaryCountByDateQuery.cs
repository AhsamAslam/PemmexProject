using AutoMapper;
using Compensation.API.Database.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.OrganizationTotalSalaryCountByDate
{
    public class OrganizationTotalSalaryCountByDateQuery : IRequest<double>
    {
        public string organizationIdentifier { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public class OrganizationTotalSalaryCountByDateQueryHandeler : IRequestHandler<OrganizationTotalSalaryCountByDateQuery, double>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IMapper _mapper;

        public OrganizationTotalSalaryCountByDateQueryHandeler(ICompensationSalaryRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<double> Handle(OrganizationTotalSalaryCountByDateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salary = await _context.GetOrganizationTotalSalaryCountByDate(request.organizationIdentifier, request.startDate, request.endDate);
                return salary;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
