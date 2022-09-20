using MediatR;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Queries.BUnitSummaryByOrganizationIdentifier
{
    public class BUnitSummaryByOrganizationIdentifierQuery : IRequest<List<BusinessUnitBudgetSummary>>
    {
        public string organizationIdentifier
        {
            get; set;
        }
    }
    public class BUnitSummaryByOrganizationIdentifierQueryHandeler : IRequestHandler<BUnitSummaryByOrganizationIdentifierQuery, List<BusinessUnitBudgetSummary>>
    {
        private readonly IAnnualSalaryPlanning _annualSalaryPlanning;
        private readonly IOrganizationService _organizationService;
        public BUnitSummaryByOrganizationIdentifierQueryHandeler(IAnnualSalaryPlanning annualSalaryPlanning,
            IOrganizationService organizationService)
        {
            _annualSalaryPlanning = annualSalaryPlanning;
            _organizationService = organizationService;
        }
        public async Task<List<BusinessUnitBudgetSummary>> Handle(BUnitSummaryByOrganizationIdentifierQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var b_us = await _organizationService.GetBusinessUnits(request.organizationIdentifier);
                var business_units = b_us.ToList();
                var salaries = await _annualSalaryPlanning.OrganizationCompensation(request.organizationIdentifier);
                var emp_salaries = salaries.ToDictionary(x => x.EmployeeIdentifier, x => x);
                var budget = await _annualSalaryPlanning.GetOrganizationalBudget(request.organizationIdentifier);
                List<BusinessUnitBudgetSummary> businessUnitBudgetSummaries
                        = new List<BusinessUnitBudgetSummary>();

                foreach (var j in budget)
                {
                    var b_units = business_units.Where(b => b.BusinessIdentifier == j.businessIdentifier
                    &&  j.jobFunction == Enum.GetName(b.JobFunction));
                    foreach (var b in b_units)
                    {
                        var b_title = business_units.FirstOrDefault(p => p.EmployeeIdentifier == b.BUnitIdentifier)?.CostCenterName;
                        var summary = businessUnitBudgetSummaries.FirstOrDefault(s => s.title == b_title
                        && s.businessIdentifier == j.businessIdentifier);
                        var salary = emp_salaries[b.EmployeeIdentifier].BaseSalary +
                                emp_salaries[b.EmployeeIdentifier].AdditionalAgreedPart;
                        if (summary == null)
                        {
                            BusinessUnitBudgetSummary businessUnitBudgetSummary
                                                      = new BusinessUnitBudgetSummary();

                            businessUnitBudgetSummary.businessIdentifier = j.businessIdentifier;
                            businessUnitBudgetSummary.businessName = j.businessName;
                            businessUnitBudgetSummary.BUnitEmployeeIdentifier = b.BUnitIdentifier;
                            businessUnitBudgetSummary.title = b_title;
                            businessUnitBudgetSummary.budgetValue = j.budgetValue;
                            businessUnitBudgetSummary.monthlySalaryValue = businessUnitBudgetSummary.monthlySalaryValue
                                + salary;
                            businessUnitBudgetSummaries.Add(businessUnitBudgetSummary);
                        }
                        else
                        {
                            summary.monthlySalaryValue = summary.monthlySalaryValue + salary;
                        }
                    }

                }
                return businessUnitBudgetSummaries;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
