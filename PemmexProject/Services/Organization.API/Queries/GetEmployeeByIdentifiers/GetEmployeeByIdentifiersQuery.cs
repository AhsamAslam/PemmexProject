using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetEmployeeByIdentifiers
{
    public class GetEmployeeByIdentifiersQuery : IRequest<List<EmployeeResponse>>
    {
        public string[] Identifiers { get; set; }
    }

    public class GetEmployeeByIdentifiersQueryHandeler : IRequestHandler<GetEmployeeByIdentifiersQuery, List<EmployeeResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdentifiersQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<EmployeeResponse>> Handle(GetEmployeeByIdentifiersQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Where(e => request.Identifiers.Contains(e.EmployeeIdentifier) && e.IsActive == true)
                .Include(x => x.CostCenter)
                .Include(x => x.employeeContacts)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(employee);
        }
    }
}
