using AutoMapper;
using Holidays.API.Database.Interfaces;
using Holidays.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetEmployeeWithMaternityLeaves
{
    public class GetEmployeeWithMaternityLeavesQuery : IRequest<List<EmployeeHolidayDto>>
    {
        public int month { get; set; }
        public int type { get; set; }
        public List<string> employeeIdentifiers { get; set; }
        public string organizationIdentifier { get; set; }
    }
    public class GetEmployeeWithMaternityLeavesQueryHandeler : IRequestHandler<GetEmployeeWithMaternityLeavesQuery, List<EmployeeHolidayDto>>
    {
        private readonly IHolidayReportRepository _context;
        private readonly IMapper _mapper;

        public GetEmployeeWithMaternityLeavesQueryHandeler(IHolidayReportRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<EmployeeHolidayDto>> Handle(GetEmployeeWithMaternityLeavesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var emp_holidays = await _context.GetEmployeeWithMaternityLeaves(request.organizationIdentifier, request.month, request.type, request.employeeIdentifiers.ToArray());
                return _mapper.Map<List<EmployeeHolidayDto>>(emp_holidays);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
