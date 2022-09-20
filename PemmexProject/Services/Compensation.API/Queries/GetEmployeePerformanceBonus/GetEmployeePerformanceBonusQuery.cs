using AutoMapper;
using Compensation.API.Database.context;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Compensation.API.Queries.GetEmployeePerformanceBonus
{
    public class GetEmployeePerformanceBonusQuery : IRequest<double>
    {
        public string employeeIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string grade { get; set; }
        public string jobFunction { get; set; }
    }

    public class GetEmployeePerformanceBonusQueryHandeler : IRequestHandler<GetEmployeePerformanceBonusQuery, double>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeePerformanceBonusQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<double> Handle(GetEmployeePerformanceBonusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var bonus = _context.JobCatalogues.FirstOrDefault(x=>x.businessIdentifier == request.businessIdentifier
                && x.grade == request.grade && x.jobFunction == request.jobFunction);
                if (bonus == null)
                {
                    throw new Exception(bonus.ToString());
                }
                var baseSalary = _context.Compensation.Where(x => x.EmployeeIdentifier == request.employeeIdentifier)
                    .OrderByDescending(y=>y.EffectiveDate)
                    .Select(b=>b.BaseSalary)
                    .FirstOrDefault();
                var additionalAgreedPart = _context.Compensation.Where(x => x.EmployeeIdentifier == request.employeeIdentifier)
                    .OrderByDescending(y => y.EffectiveDate)
                    .Select(a => a.AdditionalAgreedPart)
                    .FirstOrDefault();
                

                var bonusValue = (((baseSalary + additionalAgreedPart) / 100) * bonus.annual_bonus);
                return bonusValue;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
