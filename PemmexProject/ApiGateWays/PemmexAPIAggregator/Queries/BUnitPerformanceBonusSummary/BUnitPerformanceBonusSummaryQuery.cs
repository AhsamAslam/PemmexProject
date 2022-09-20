using MediatR;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Queries.BUnitPerformanceBonusSummary
{
    public class BUnitPerformanceBonusSummaryQuery : IRequest<List<BusinessUnitBonusSummary>>
    {
        public string organizationIdentifier
        {
            get; set;
        }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
    public class BUnitPerformanceBonusSummaryQueryHandeler : IRequestHandler<BUnitPerformanceBonusSummaryQuery, List<BusinessUnitBonusSummary>>
    {
        private readonly IAnnualSalaryPlanning _annualSalaryPlanning;
        private readonly IOrganizationService _organizationService;
        private readonly ICompensationService _compensationService;
        public BUnitPerformanceBonusSummaryQueryHandeler(IAnnualSalaryPlanning annualSalaryPlanning,
            IOrganizationService organizationService,ICompensationService compensationService)
        {
            _annualSalaryPlanning = annualSalaryPlanning;
            _organizationService = organizationService;
            _compensationService = compensationService;
        }
        public async Task<List<BusinessUnitBonusSummary>> Handle(BUnitPerformanceBonusSummaryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var b_us = await _organizationService.GetBusinessUnits(request.organizationIdentifier);
                var businesses = await _organizationService.GetBusinesses(request.organizationIdentifier);
                var business_units = b_us.Where(b => b.isBunit == true).ToList();
                var salaries = await _annualSalaryPlanning.OrganizationSalaries(request.organizationIdentifier,request.startDate,request.endDate);
                var salaries_dict = salaries.ToDictionary(x => x.EmployeeIdentifier, x => x);
                var jobcatalogues = await _compensationService.GetOrganizationJobCatalogues(request.organizationIdentifier);
                List<BusinessUnitBonusSummary> businessUnitBudgetSummaries
                        = new List<BusinessUnitBonusSummary>();

                foreach (var j in business_units)
                {
                    var b_units = b_us.Where(b => b.BUnitIdentifier == j.BUnitIdentifier && b.EmployeeIdentifier != b.BUnitIdentifier);
                    
                    foreach (var b in businesses)
                    {
                        var employees = b_units.Where(b => b.BusinessIdentifier == b.BusinessIdentifier)
                            .ToList();
                        double total_salary = 0.0;
                        double total_bonus = 0.0;
                        foreach(var e in employees)
                        {
                            if (salaries_dict.ContainsKey(e.EmployeeIdentifier)) {
                                var s = salaries_dict[e.EmployeeIdentifier];
                                var j_b = jobcatalogues.FirstOrDefault(j =>
                                j.businessIdentifier == b.BusinessIdentifier
                                && j.jobFunction == Enum.GetName(e.JobFunction)
                                && j.grade == e.Grade);

                                total_salary += (s.BaseSalary + s.AdditionalAgreedPart);
                                total_bonus += (((s.BaseSalary + s.AdditionalAgreedPart) / 100) * j_b.acv_bonus_percentage);
                            }
                        }
                        

                        BusinessUnitBonusSummary businessUnitBudgetSummary
                                                      = new BusinessUnitBonusSummary();

                        businessUnitBudgetSummary.businessIdentifier = b.BusinessIdentifier;
                        businessUnitBudgetSummary.businessName = b.BusinessName;
                        businessUnitBudgetSummary.title = j.Title;
                        businessUnitBudgetSummary.bonusValue = total_bonus;
                        businessUnitBudgetSummary.monthlySalaryValue = total_salary;
                        businessUnitBudgetSummaries.Add(businessUnitBudgetSummary);

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
