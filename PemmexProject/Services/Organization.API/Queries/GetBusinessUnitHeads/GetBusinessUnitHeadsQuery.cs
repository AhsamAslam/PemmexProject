using AutoMapper;
using MediatR;
using Organization.API.Database.Interfaces;
using Organization.API.Dtos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetBusinessUnitHeads
{
    public class GetBusinessUnitHeadsQuery : IRequest<List<EmployeeResponse>>
    {
        public string organizationIdentifier { get; set; }
    }
    public class GetBusinessUnitHeadsQueryHandler : IRequestHandler<GetBusinessUnitHeadsQuery, List<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _context;
        private readonly IMapper _mapper;

        public GetBusinessUnitHeadsQueryHandler(IEmployeeRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetBusinessUnitHeadsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _context.GetBusinessUnitsHeads(request.organizationIdentifier);
                return _mapper.Map<List<EmployeeResponse>>(employees);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
