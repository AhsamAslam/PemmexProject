using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Queries.GetFunctionalBudget
{
    public class GetFunctionalBudgetQuery : IRequest<List<FunctionalBudgetDto>>
    {
        public string organizationIdentifier { get; set; }
        public DateTime budgetDate { get; set; }
    }

    public class GetFunctionalBudgetQueryHandeler : IRequestHandler<GetFunctionalBudgetQuery, List<FunctionalBudgetDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFunctionalBudgetQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<FunctionalBudgetDto>> Handle(GetFunctionalBudgetQuery request, CancellationToken cancellationToken)
        {
            List<FunctionalBudgetDto> budgets = new List<FunctionalBudgetDto>();
           var budget = await _context.OrganizationBudgets
                .Where(o => o.organizationIdentifier == request.organizationIdentifier
                && o.startDate.Date == request.budgetDate.Date)
                .ToListAsync(cancellationToken);

            foreach(var b in budget)
            {
                FunctionalBudgetDto dto = new FunctionalBudgetDto();
                dto.budgetPercentage = b.budgetPercentage;
                dto.budgetValue = b.budgetValue;
                dto.businessIdentifier = b.businessIdentifier;
                dto.jobFunction = b.jobFunction;
                budgets.Add(dto);
            }
            return budgets;
        }
    }
}
