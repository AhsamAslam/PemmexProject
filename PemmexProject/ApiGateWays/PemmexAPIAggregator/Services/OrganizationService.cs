using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly HttpClient _client;
        public OrganizationService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CostCenter>> GetBusinessCostCenters(string businessIdentifier)
        {
            var response = await _client.GetAsync($"/api/CostCenters?businessIdentifier={businessIdentifier}");
            return await response.ReadContentAs<List<CostCenter>>();
        }

        public async Task<IEnumerable<Employee>> GetBusinessEmployees(string businessIdentifier)
        {
            var response = await _client.GetAsync($"/api/Organization/BusinessEmployees?businessIdentifier={businessIdentifier}");
            return await response.ReadContentAs<List<Employee>>();
        }

        public async Task<IEnumerable<Business>> GetBusinesses(string organizationId)
        {
            var response = await _client.GetAsync($"/api/Organization/Businesses?organizationIdentifier={organizationId}");
            return await response.ReadContentAs<List<Business>>();
        }

        public async Task<IEnumerable<Employee>> GetOrganizationEmployees(string organizationId)
        {
            var response = await _client.GetAsync($"/api/Organization/OrganizationEmployees?organizationIdentifier={organizationId}");
            return await response.ReadContentAs<List<Employee>>();
        }

        public async Task<IEnumerable<Employee>> GetSuboridnates(string EmployeeIdentifier)
        {
            var response = await _client.GetAsync($"/api/Organization/Subordinates/{EmployeeIdentifier}");
            return await response.ReadContentAs<List<Employee>>();
        }

        public async Task<IEnumerable<Employee>> GetTeamMembers(string costcenterIdentifier)
        {
            var response = await _client.GetAsync($"/api/Organization/teamMembers/{costcenterIdentifier}");
            return await response.ReadContentAs<List<Employee>>();
        }
    }
}
