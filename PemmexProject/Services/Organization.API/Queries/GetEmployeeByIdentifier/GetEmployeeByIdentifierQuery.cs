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

namespace Organization.API.Queries.GetEmployeeByIdentifier
{
    public class GetEmployeeByIdentifierQuery : IRequest<EmployeeResponse>
    {
        public string Id { get; set; }
    }

    public class GetEmployeeByIdentifierQueryHandeler : IRequestHandler<GetEmployeeByIdentifierQuery, EmployeeResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeByIdentifierQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeResponse> Handle(GetEmployeeByIdentifierQuery request, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees
                .Where(e => e.EmployeeIdentifier == request.Id && e.IsActive == true)
                .Include(x => x.CostCenter)
                .Include(x => x.employeeContacts)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Employee, EmployeeResponse>(employee);
        }
    }
}
