using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetEmployeesSalaryDetails
{
    public class GetEmployeesSalaryDetailsQuery : IRequest<List<CompensationTotalMonthlyPayDto>>
    {
        public string[] Identifiers { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public class GetEmployeesSalaryDetailsQueryHandeler : IRequestHandler<GetEmployeesSalaryDetailsQuery, List<CompensationTotalMonthlyPayDto>>
    {
        private readonly ICompensationSalaryRepository _compensationSalary;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeesSalaryDetailsQueryHandeler(ICompensationSalaryRepository compensationSalary, IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _compensationSalary = compensationSalary;
        }
        public async Task<List<CompensationTotalMonthlyPayDto>> Handle(GetEmployeesSalaryDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var totalMonths = -1 * (request.startDate.Subtract(request.endDate).Days / (365 / 12));

                var employees = await _compensationSalary.GetCurrentCompensation(request.Identifiers, totalMonths);

                return employees.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
