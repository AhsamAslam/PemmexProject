using AutoMapper;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetFunctionalBudgetByManagerId
{
    public class GetFunctionalBudgetTeamQuery : IRequest<List<FunctionalBudgetDto>>
    {
        public string[] employees { get; set; }
    }

    public class GetFunctionalBudgetTeamQueryHandeler : IRequestHandler<GetFunctionalBudgetTeamQuery, List<FunctionalBudgetDto>>
    {
        private readonly IFunctionalBudgetRepository _context;
        private readonly IMapper _mapper;
        public GetFunctionalBudgetTeamQueryHandeler(IFunctionalBudgetRepository context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FunctionalBudgetDto>> Handle(GetFunctionalBudgetTeamQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var budgets = await _context.Get(request.employees);
                return _mapper.Map<List<FunctionalBudgetDto>>(budgets);
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
