using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;

namespace Organization.API.Queries.GetManagersQuery
{
    public class GetManagersQuery : IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }
    public class GetManagersQueryHandler : IRequestHandler<GetManagersQuery, List<EmployeeResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;

        public GetManagersQueryHandler(IApplicationDbContext context, IEmployee employee, IMapper mapper)
        {
            _context = context;
            _employee = employee;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetManagersQuery request, CancellationToken cancellationToken)
        {

            //var list = await _context.Employees
            //    .Where(e => e.IsActive == true && e.Businesses.ParentBusinessId == request.Id)
            //    .Select(e => e.ManagerIdentifier)
            //    .Distinct()
            //    .ToListAsync(cancellationToken);
            var list = await _employee.GetEmployeeManagerIdentifierByParentBusinessId(request.Id);
            var e = await _employee.GetEmployeeCostCenterByEmployeeIdentifier(list.ToArray());
            //var e = await _context.Employees
            //    .Include(e => e.CostCenter)
            //    .Where(x => list.Contains(x.EmployeeIdentifier))
            //    .ToListAsync(cancellationToken);

            return _mapper.Map<List<Entities.Employee>, List<EmployeeResponse>>(e.ToList());
        }
    }
}
