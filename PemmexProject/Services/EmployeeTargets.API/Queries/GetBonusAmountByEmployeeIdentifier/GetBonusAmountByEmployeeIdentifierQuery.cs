using AutoMapper;
using EmployeeTargets.API.Database.Interfaces;
using MediatR;

namespace EmployeeTargets.API.Queries.GetBonusAmountByEmployeeIdentifier
{
    public class GetBonusAmountByEmployeeIdentifierQuery : IRequest<double>
    {
        public string employeeIdentifier { get; set; }
    }
    public class GetBonusAmountByEmployeeIdentifierQueryHandeler : IRequestHandler<GetBonusAmountByEmployeeIdentifierQuery, double>
    {
        private readonly IPerformanceBudgetRepository _performanceBudget;
        private readonly IMapper _mapper;

        public GetBonusAmountByEmployeeIdentifierQueryHandeler(IPerformanceBudgetRepository performanceBudget, IMapper mapper)
        {
            _performanceBudget = performanceBudget;
            _mapper = mapper;
        }
        public async Task<double> Handle(GetBonusAmountByEmployeeIdentifierQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employeeBonusAmount = await _performanceBudget.GetBonusAmountByEmployeeIdentifier(request.employeeIdentifier);
                return employeeBonusAmount;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
