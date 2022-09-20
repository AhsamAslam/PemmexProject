using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Helpers;
using PemmexAPIAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface ICompensationService
    {
        //
        Task<IEnumerable<JobCatalogue>> GetOrganizationJobCatalogues(string organizationIdentifier);
        Task<IEnumerable<EmployeeBonus>> GetBonus(string[] employeeIdentifiers);
        Task<List<Compensation>> GetEmployeesSalaryDetails(string[] employees);
        Task<List<CompensationTotalMonthlyPay>> GetEmployeesSalaryDetails(string[] employees, DateTime startDate, DateTime endDate);
        Task<bool> UpdateBonusFromPerformanceBonus(string command);
    }
    public class CompensationService : ICompensationService
    {
        private readonly HttpClient _client;
        public CompensationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<EmployeeBonus>> GetBonus(string[] employeeIdentifiers)
        {
            try
            {
                var response = await _client.GetAsync($"api/Bonus?{employeeIdentifiers.ConvertArrayToQueryString("employeeIdentifiers")}");
                return await response.ReadContentAs<IEnumerable<EmployeeBonus>>();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<List<Compensation>> GetEmployeesSalaryDetails(string[] employees)
        {
            var response = await _client.GetAsync($"api/Compensation?{employees.ConvertArrayToQueryString("Identifiers")}");
            return await response.ReadContentAs<List<Compensation>>();
        }

        public async Task<List<CompensationTotalMonthlyPay>> GetEmployeesSalaryDetails(string[] employees, DateTime startDate, DateTime endDate)
        {
            var response = await _client.GetAsync($"api/Compensation/GetEmployeeTotalMonthlySalary?{employees.ConvertArrayToQueryString("Identifiers")}&&startDate={startDate.ToString("O")}&&endDate={endDate.ToString("O")}");
            return await response.ReadContentAs<List<CompensationTotalMonthlyPay>>();
        }
        public async Task<IEnumerable<JobCatalogue>> GetOrganizationJobCatalogues(string organizationIdentifier)
        {
            var response = await _client.GetAsync($"api/JobCatalogue/OrganizationJobCatalogues?organizationIdentifier={organizationIdentifier}");
            return await response.ReadContentAs<IEnumerable<JobCatalogue>>();
        }

        public async Task<bool> UpdateBonusFromPerformanceBonus(string command)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/Bonus/UpdateBonusFromPerformanceBonus"),
                    Content = new StringContent(command, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                bool data = JsonSerializer.Deserialize<bool>(result);
                return data;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
