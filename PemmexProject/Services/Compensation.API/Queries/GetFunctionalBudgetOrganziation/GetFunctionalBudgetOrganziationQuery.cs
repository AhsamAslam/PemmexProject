using AutoMapper;
using Compensation.API.Database.Interfaces;
using Compensation.API.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetFunctionalBudgetOrganziation
{
    public class GetFunctionalBudgetOrganziationQuery : IRequest<List<FunctionalBudgetDto>>
    {
        public string organizationIdentifier { get; set; }
    }

    public class GetFunctionalBudgetOrganziationQueryHandeler : IRequestHandler<GetFunctionalBudgetOrganziationQuery, List<FunctionalBudgetDto>>
    {
        private readonly IFunctionalBudgetRepository _context;
        private readonly IMapper _mapper;
        public GetFunctionalBudgetOrganziationQueryHandeler(IFunctionalBudgetRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FunctionalBudgetDto>> Handle(GetFunctionalBudgetOrganziationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var budgets = await _context.GetAll(request.organizationIdentifier);
                return _mapper.Map<List<FunctionalBudgetDto>>(budgets);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
