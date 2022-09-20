using Newtonsoft.Json;
using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Helpers;
using PemmexAPIAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface IAnnualSalaryPlanning
    {
        Task<IEnumerable<Compensation>> OrganizationCompensation(string organizationIdentifier);
        Task<IEnumerable<CompensationSalary>> OrganizationSalaries(string organizationIdentifier, DateTime start, DateTime end);
        Task<double> GetFunctionalSalaryCount(string[] employees);
        Task<IEnumerable<OrganizationalBudgetSummary>> GetFunctionalBudgetCount(string json);
        Task<IEnumerable<OrganizationalBudgetSummary>> GetOrganizationalBudget(string organizationIdentifier);
        Task<IEnumerable<FunctionalBudgetDto>> GetFunctionalBudgetDetails(string employees);
        Task<IEnumerable<FunctionalBudgetDto>> GetOrganizationFunctionalBudgetDetails(string organizationalIdentifier);
    }
    public class AnnualSalaryPlanning : IAnnualSalaryPlanning
    {
        private readonly HttpClient _client;
        public AnnualSalaryPlanning(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<OrganizationalBudgetSummary>> GetOrganizationalBudget(string organizationIdentifier)
        {
            var response = await _client.GetAsync($"api/Budget?organizationIdentifier={organizationIdentifier}");
            return await response.ReadContentAs<IEnumerable<OrganizationalBudgetSummary>>();
        }

        public async Task<IEnumerable<OrganizationalBudgetSummary>> GetFunctionalBudgetCount(string json)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress + "api/Salary/FunctionalBudgetCount"),
                Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };


            var response = await _client.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var summary =  JsonConvert.
                DeserializeObject<IEnumerable<OrganizationalBudgetSummary>>
                (response.Content.ReadAsStringAsync().Result);
            return summary;
        }

        public async Task<double> GetFunctionalSalaryCount(string[] employees)
        {
            var response = await _client.GetAsync($"api/Salary/FunctionalSalaryCount?{employees.ConvertArrayToQueryString("Identifiers")}");
            return await response.ReadContentAs<double>();
        }

        public async Task<IEnumerable<Compensation>> OrganizationCompensation(string organizationIdentifier)
        {
            var response = await _client.GetAsync($"api/Compensation/OrganizationCompensations?organizationIdentifier={organizationIdentifier}");
            return await response.ReadContentAs<IEnumerable<Compensation>>();
        }
        public async Task<IEnumerable<CompensationSalary>>OrganizationSalaries(
            string organizationIdentifier,
            DateTime start,DateTime end)
        {
            var response = await _client.GetAsync($"api/Salary/OrganizationSalaries?organizationIdentifier={organizationIdentifier}&startDate={start.ToString("O")}&endDate={end.ToString("O")}");
            return await response.ReadContentAs<IEnumerable<CompensationSalary>>();
        }

        public async Task<IEnumerable<FunctionalBudgetDto>> GetFunctionalBudgetDetails(string employees)
        {

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress + "api/Budget/Team?employees="),
                Content = new StringContent(employees, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };
            var response = await _client.SendAsync(request).ConfigureAwait(false);
            return await response.ReadContentAs<IEnumerable<FunctionalBudgetDto>>();
        }
        public async Task<IEnumerable<FunctionalBudgetDto>> GetOrganizationFunctionalBudgetDetails(string organizationIdentifier)
        {
            var response = await _client.GetAsync($"api/Budget/Organziation?organizationIdentifier={organizationIdentifier}");
            return await response.ReadContentAs<IEnumerable<FunctionalBudgetDto>>();
        }
    }
}
