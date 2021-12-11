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

namespace Organization.API.Queries.GetAllOrganizationEmployees
{
    public class GetAllOrganizationEmployeesQuery : IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }

    public class GetAllOrganizationEmployeesQueryHandeler : IRequestHandler<GetAllOrganizationEmployeesQuery, List<EmployeeResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllOrganizationEmployeesQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<EmployeeResponse>> Handle(GetAllOrganizationEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees
                .Include(b => b.Businesses)
                .Where(e => e.Businesses.ParentBusinessId == request.Id && e.IsActive == true)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(employees);
        }
    }
}
