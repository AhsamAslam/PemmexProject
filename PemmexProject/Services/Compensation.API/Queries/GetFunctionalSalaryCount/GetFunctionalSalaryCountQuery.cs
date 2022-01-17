using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetFunctionalSalaryCount
{
    public class GetFunctionalSalaryCountQuery : IRequest<double>
    {
        public string[] employeeIdentifiers { get; set; }
    }

    public class GetFunctionalSalaryCountQueryHandeler : IRequestHandler<GetFunctionalSalaryCountQuery, double>
    {
        private ICompensationSalaryRepository _compensation;
        private readonly IMapper _mapper;

        public GetFunctionalSalaryCountQueryHandeler(ICompensationSalaryRepository compensation, IMapper mapper)
        {
            _compensation = compensation;
            _mapper = mapper;
        }
        public async Task<double> Handle(GetFunctionalSalaryCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //var salary = await _context.Compensation
                //   .Where(e => request.employeeIdentifiers.Contains(e.EmployeeIdentifier))
                //   .ToListAsync(cancellationToken);
                var salary = await _compensation.GetFunctionalSalaryCount(request.employeeIdentifiers);
                return salary.Sum(s => s.TotalMonthlyPay);
            }
            catch (System.Exception)
            {

                throw;
            }

           
        }
    }
}
