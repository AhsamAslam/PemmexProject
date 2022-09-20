using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Interfaces;
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
    public class GetOrganizationalBudgetQuery : IRequest<List<OrganizationBudgetDto>>
    {
        public string organizationIdentifier { get; set; }
    }

    public class GetOrganizationalBudgetQueryHandeler : IRequestHandler<GetOrganizationalBudgetQuery, List<OrganizationBudgetDto>>
    {
        private readonly IFunctionalBudgetRepository _context;

        public GetOrganizationalBudgetQueryHandeler(IFunctionalBudgetRepository context)
        {
            _context = context;
        }
        public async Task<List<OrganizationBudgetDto>> Handle(GetOrganizationalBudgetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<OrganizationBudgetDto> budgets = new List<OrganizationBudgetDto>();
                var budget = await _context.GetOrganizationBudget(request.organizationIdentifier);
                foreach (var b in budget)
                {
                    OrganizationBudgetDto dto = new OrganizationBudgetDto();
                    dto.budgetPercentage = b.budgetPercentage;
                    dto.budgetValue = b.budgetValue;
                    dto.businessIdentifier = b.businessIdentifier;
                    dto.jobFunction = Enum.GetName(typeof(JobFunction), b.jobFunction);
                    dto.mandatoryBudgetValue = b.compulsoryPercentage;
                    budgets.Add(dto);
                }
                return budgets;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
