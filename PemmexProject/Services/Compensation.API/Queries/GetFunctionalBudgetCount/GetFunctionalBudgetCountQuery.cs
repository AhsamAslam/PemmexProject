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

namespace Compensation.API.Queries.GetFunctionalBudgetCount
{
    public class GetFunctionalBudgetCountQuery : IRequest<List<OrganizationBudgetDto>>
    {
        public List<FunctionalSalaryRequest> functionalSalaryRequests { get; set; }
        public string organizationIdentifier { get; set; }
    }
    public class FunctionalSalaryRequest
    {
        public string employeeIdentifiers { get; set; }
        public JobFunction jobFunction { get; set; }
    }

    public class GetFunctionalBudgetCountQueryHandeler : IRequestHandler<GetFunctionalBudgetCountQuery, List<OrganizationBudgetDto>>
    {
        private readonly ICompensationSalaryRepository _context;
        private readonly IFunctionalBudgetRepository _functionalBudgetRepository;
        public GetFunctionalBudgetCountQueryHandeler(
            ICompensationSalaryRepository context,IFunctionalBudgetRepository functionalBudgetRepository)
        {
            _context = context;
            _functionalBudgetRepository = functionalBudgetRepository;
        }
        public async Task<List<OrganizationBudgetDto>> Handle(GetFunctionalBudgetCountQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                var budgets = await _functionalBudgetRepository.GetOrganizationBudget(request.organizationIdentifier);
                List<OrganizationBudgetDto> functionalBudgetDtos = new List<OrganizationBudgetDto>();
                List<string> employeeIdentifiers = new List<string>();
                foreach (var r in request.functionalSalaryRequests)
                {
                    employeeIdentifiers.Add(r.employeeIdentifiers);
                }
                var salaries = await _context.GetCurrentCompensation(employeeIdentifiers.ToArray());
                var totalSalaries = 0.0;
                foreach (var s in salaries)
                {
                    var jobFunction = request.functionalSalaryRequests.FirstOrDefault(f => f.employeeIdentifiers == s.EmployeeIdentifier);
                    var budget = budgets.FirstOrDefault(b => b.jobFunction == jobFunction.jobFunction
                    && b.businessIdentifier == s.businessIdentifier);
                    var dto = functionalBudgetDtos.FirstOrDefault(f => f.businessIdentifier == s.businessIdentifier);
                    double mandatory_budget = (((s.BaseSalary + s.AdditionalAgreedPart) / 100) * budget?.compulsoryPercentage ?? 0.0);
                    double allocated_budget = (((s.BaseSalary + s.AdditionalAgreedPart) / 100) * budget?.budgetPercentage ?? 0.0);
                    totalSalaries += (s.BaseSalary + s.AdditionalAgreedPart);
                    if (dto == null)
                    {
                        functionalBudgetDtos.Add(new OrganizationBudgetDto()
                        {
                            businessIdentifier = s.businessIdentifier,
                            mandatoryBudgetValue = mandatory_budget,
                            budgetValue = allocated_budget,
                            TotalbudgetValue = mandatory_budget + allocated_budget,
                            jobFunction = Enum.GetName(jobFunction.jobFunction),
                            totalSalary = (s.BaseSalary + s.AdditionalAgreedPart)
                        });
                    }
                    else
                    {
                        dto.budgetValue += allocated_budget;
                        dto.mandatoryBudgetValue += mandatory_budget;
                        dto.TotalbudgetValue += allocated_budget + mandatory_budget;
                        dto.totalSalary += (s.BaseSalary + s.AdditionalAgreedPart);
                    }
                }
                foreach (var b in functionalBudgetDtos)
                {
                    b.budgetPercentage = ((b.budgetValue / b.totalSalary) * 100);
                    b.TotalbudgetPercentageValue = ((b.TotalbudgetValue / b.totalSalary) * 100);
                    b.mandatoryBudgetPercentage = ((b.mandatoryBudgetValue / b.totalSalary) * 100);
                }
                return functionalBudgetDtos;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}

