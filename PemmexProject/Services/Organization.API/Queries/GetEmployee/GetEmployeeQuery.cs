using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;

namespace Organization.API.Queries.GetEmployee
{
    public class GetEmployeeQuery:IRequest<EmployeeResponse>
    {
        public string Id { get; set; }
    }

    public class GetEmployeeQueryHandeler : IRequestHandler<GetEmployeeQuery, EmployeeResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            var guid = Guid.Parse(request.Id);
            var employee = await _context.Employees
                .Where(e => e.Emp_Guid == guid && e.IsActive == true)
                .Include(x => x.CostCenter)
                .Include(x => x.employeeContacts)
                .FirstOrDefaultAsync(cancellationToken);

            return _mapper.Map<Employee,EmployeeResponse>(employee);
        }
    }
}
