using AutoMapper;
using MediatR;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Services;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Queries.FunctionalBudgetCount
{
    public class BUnitSummaryQuery : IRequest<List<BusinessUnitBudgetSummary>>
    {
        public List<JobFunctionSummary> jobFunctionSummaries { get; set; }
        public string organizationIdentifier
        {
            get; set;
        }
        public class JobFunctionSummary
        {
            public JobFunction JobFunction { get; set; }
            public string BusinessIdentifier { get; set; }
            public string BusinessName { get; set; }
            public double budgetPercentage { get; set; }
            public double mandatoryBudgetPercentage { get; set; }
        }

        public class BUnitSummaryQueryHandeler : IRequestHandler<BUnitSummaryQuery, List<BusinessUnitBudgetSummary>>
        {
            private readonly IAnnualSalaryPlanning _annualSalaryPlanning;
            private readonly IOrganizationService _organizationService;
            public BUnitSummaryQueryHandeler(IAnnualSalaryPlanning annualSalaryPlanning,
                IOrganizationService organizationService)
            {
                _annualSalaryPlanning = annualSalaryPlanning;
                _organizationService = organizationService;
            }
            public async Task<List<BusinessUnitBudgetSummary>> Handle(BUnitSummaryQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var b_us = await _organizationService.GetBusinessUnits(request.organizationIdentifier);
                    var business_units = b_us.ToList();
                    var salaries = await _annualSalaryPlanning.OrganizationCompensation(request.organizationIdentifier);
                    var emp_salaries = salaries.ToDictionary(x => x.EmployeeIdentifier, x => x);
                    List<BusinessUnitBudgetSummary> businessUnitBudgetSummaries
                            = new List<BusinessUnitBudgetSummary>();

                    foreach(var j in request.jobFunctionSummaries)
                    {
                        var b_units = business_units.Where(b => b.BusinessIdentifier == j.BusinessIdentifier
                        && b.JobFunction == j.JobFunction);
                        foreach(var b in b_units)
                        {
                            var b_title = business_units.FirstOrDefault(p => p.EmployeeIdentifier == b.BUnitIdentifier)?.CostCenterName;
                            var summary = businessUnitBudgetSummaries.FirstOrDefault(s => s.title == b_title
                            && s.businessIdentifier == j.BusinessIdentifier);
                            var salary = emp_salaries[b.EmployeeIdentifier].BaseSalary +
                                    emp_salaries[b.EmployeeIdentifier].AdditionalAgreedPart;
                            if (summary == null)
                            {
                                BusinessUnitBudgetSummary businessUnitBudgetSummary
                                                          = new BusinessUnitBudgetSummary();
                                
                                businessUnitBudgetSummary.businessIdentifier = j.BusinessIdentifier;
                                businessUnitBudgetSummary.businessName = j.BusinessName;
                                businessUnitBudgetSummary.title = b_title;
                                businessUnitBudgetSummary.budgetValue = businessUnitBudgetSummary.budgetValue +
                                            salary + ((salary / 100) * (j.budgetPercentage + j.mandatoryBudgetPercentage));
                                businessUnitBudgetSummary.monthlySalaryValue = businessUnitBudgetSummary.monthlySalaryValue 
                                    + salary; 
                                businessUnitBudgetSummaries.Add(businessUnitBudgetSummary);
                            }
                            else
                            {
                                summary.budgetValue = summary.budgetValue +
                                            salary + ((salary / 100) * (j.budgetPercentage + j.mandatoryBudgetPercentage));
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
}