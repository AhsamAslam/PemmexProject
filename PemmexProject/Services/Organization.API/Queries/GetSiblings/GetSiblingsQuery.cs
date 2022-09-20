using AutoMapper;
using MediatR;
using Organization.API.Database.Interfaces;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetSiblings
{
    public class GetSiblingsQuery : IRequest<List<EmployeeResponse>>
    {
        public string employeeIdentifier { get; set; }
    }
    public class GetSiblingsQueryHandler : IRequestHandler<GetSiblingsQuery, List<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _context;
        private readonly IMapper _mapper;

        public GetSiblingsQueryHandler(IEmployeeRepository context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetSiblingsQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.GetSiblings(request.employeeIdentifier);
            return _mapper.Map<List<EmployeeResponse>>(employees);
        }
    }
}
