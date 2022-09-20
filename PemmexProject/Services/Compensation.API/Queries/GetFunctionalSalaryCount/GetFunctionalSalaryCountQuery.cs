using AutoMapper;
using Compensation.API.Database.context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Domain.Enums;
using System;
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
        private readonly IApplicationDbContext _context;
        
        public GetFunctionalSalaryCountQueryHandeler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<double> Handle(GetFunctionalSalaryCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var salary = await _context.Compensation
                .Where(e => request.employeeIdentifiers.Contains(e.EmployeeIdentifier))
                .ToListAsync(cancellationToken);
                return salary.Sum(s => s.TotalMonthlyPay);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
