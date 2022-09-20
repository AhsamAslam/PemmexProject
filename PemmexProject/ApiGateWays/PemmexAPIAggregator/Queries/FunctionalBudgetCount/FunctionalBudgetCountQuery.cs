using MediatR;
using Newtonsoft.Json;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Services;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Queries.FunctionalBudgetCount
{
    public class FunctionalBudgetCountQuery : IRequest<List<OrganizationalBudgetSummary>>
    {
        public IEnumerable<Employee> employees { get; set; }
        public string organizationIdentifier { get; set; }
    }
    public class FunctionalSalaryRequest
    {
        public string employeeIdentifiers { get; set; }
        public JobFunction jobFunction { get; set; }
    }

    public class FunctionalBudgetCountQueryHandeler : IRequestHandler<FunctionalBudgetCountQuery, List<OrganizationalBudgetSummary>>
    {
        private readonly IAnnualSalaryPlanning _annualSalaryPlanning;

        public FunctionalBudgetCountQueryHandeler(IAnnualSalaryPlanning annualSalaryPlanning,
            HttpClient client)
        {
            _annualSalaryPlanning = annualSalaryPlanning;
        }
        public async Task<List<OrganizationalBudgetSummary>> Handle(FunctionalBudgetCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<FunctionalSalaryRequest> salaryRequests = new List<FunctionalSalaryRequest>();
                foreach(var e in request.employees)
                {
                    salaryRequests.Add(new FunctionalSalaryRequest()
                    {
                        employeeIdentifiers = e.EmployeeIdentifier,
                        jobFunction = e.JobFunction.ToEnum<JobFunction>()
                    });
                }
                dynamic json = new ExpandoObject();
                json.functionalSalaryRequests = salaryRequests;
                json.organizationIdentifier = request.organizationIdentifier;

                return await _annualSalaryPlanning.GetFunctionalBudgetCount(JsonConvert.SerializeObject(json));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
