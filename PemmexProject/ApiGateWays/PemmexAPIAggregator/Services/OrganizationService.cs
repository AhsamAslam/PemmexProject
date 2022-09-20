using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Models;
using PemmexCommonLibs.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface IOrganizationService
    {
        Task<Employee> GetEmployee(string employeeIdentifier);
        Task<IEnumerable<CostCenter>> GetBusinessCostCenters(string businessIdentifier);
        Task<IEnumerable<Employee>> GetSiblings(string employeeIdentifier);
        Task<IEnumerable<Employee>> GetTeamMembers(string employeeIdentifier);
        Task<IEnumerable<Employee>> GetBusinessEmployees(string businessIdentifier);
        Task<IEnumerable<Employee>> GetOrganizationEmployees(string organizationId);
        Task<IEnumerable<Business>> GetBusinesses(string organizationId);
        Task<IEnumerable<BusinessUnit>> GetBusinessUnits(string organizationIdentifier);
        Task<List<Employee>> GetManagers();
    }
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

        public async Task<IEnumerable<Employee>> GetTeamMembers(string employeeIdentifier)
        {
            var response = await _client.GetAsync($"/api/Employees/TeamMembers/{employeeIdentifier}");
            return await response.ReadContentAs<List<Employee>>();
        }
        public async Task<IEnumerable<Employee>> GetSiblings(string employeeIdentifier)
        {
            var response = await _client.GetAsync($"/api/Employees/Siblings/{employeeIdentifier}");
            return await response.ReadContentAs<List<Employee>>();
        }
        public async Task<IEnumerable<BusinessUnit>> GetBusinessUnits(string organizationIdentifier)
        {
            var response = await _client.GetAsync($"/api/Organization/businessUnits?organizationIdentifier={organizationIdentifier}");
            return await response.ReadContentAs<List<BusinessUnit>>();
        }
        public async Task<Employee> GetEmployee(string employeeIdentifier)
        {
            try
            {
                var response = await _client.GetAsync($"/api/Employees/{employeeIdentifier}");
                return await response.ReadContentAs<Employee>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Employee>> GetManagers()
        {
            try
            {
                var response = await _client.GetAsync($"/api/Organization/managers");
                return await response.ReadContentAs<List<Employee>>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
