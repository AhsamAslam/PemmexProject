using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Helpers;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Models.PerformanceBonus;
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
    public interface IPerformanceBonus
    {
        Task<bool> CreateSoftTargets(string SoftTargets);
        Task<List<SoftTargetsDto>> GetSoftTargets(string employeeIdentifiers);
        Task<bool> CreateHardTargets(string HardTargets);
        Task<List<HardTargetsDto>> GetHardTargets(string employeeIdentifiers);
        Task<PerfromanceBudgetPlanningDto> GetPerfromanceBudgetPlanning(string organizationIdentifier);
        Task<List<PerformanceEvaluationSummaryDto>> GetPerformanceEvaluationSummary(string organizationIdentifier);
        Task<List<PerformanceEvaluationSummaryDto>> GetPerformanceEvaluationSummaryDetail(string[] employeeIdentifier);
        Task<bool> UpdateAllEmployeeTargetsIsActive(string employeeIdentifiers);
    }
    public class PerformanceBonus : IPerformanceBonus
    {
        private readonly HttpClient _client;
        public PerformanceBonus(HttpClient client)
        {
            _client = client;
        }
        public async Task<bool> CreateSoftTargets(string SoftTargets)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/SoftTargets"),
                    Content = new StringContent(SoftTargets, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
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

        public async Task<List<SoftTargetsDto>> GetSoftTargets(string employeeIdentifiers)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/SoftTargets/ShowSoftTarget"),
                    Content = new StringContent(employeeIdentifiers, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<SoftTargetsDto>>(result);
                return data;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<bool> CreateHardTargets(string HardTargets)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/HardTargets"),
                    Content = new StringContent(HardTargets, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                bool data = JsonSerializer.Deserialize<bool>(result);
                return data;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        public async Task<List<HardTargetsDto>> GetHardTargets(string employeeIdentifiers)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/HardTargets/ShowHardTarget"),
                    Content = new StringContent(employeeIdentifiers, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<HardTargetsDto>>(result);
                return data;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        public async Task<PerfromanceBudgetPlanningDto> GetPerfromanceBudgetPlanning(string organizationIdentifier)
        {
            try
            {
                var response = await _client.GetAsync($"api/PerformanceBudget/GetPerfromanceBudgetPlanning?organizationIdentifier={organizationIdentifier}");
                var result = await response.Content.ReadAsStringAsync();
                if(result == "")
                {
                    return null;
                }
                var data = JsonSerializer.Deserialize<PerfromanceBudgetPlanningDto>(result);
                //return await response.ReadContentAs<PerfromanceBudgetPlanningDto>();
                return data;
            }
            catch (System.Exception)
            {

                throw;
            }
            
        }
        public async Task<List<PerformanceEvaluationSummaryDto>> GetPerformanceEvaluationSummary(string organizationIdentifier)
        {
            try
            {
                var response = await _client.GetAsync($"api/PerformanceBudget/GetPerformanceEvaluationSummary?organizationIdentifier={organizationIdentifier}");
                return await response.ReadContentAs<List<PerformanceEvaluationSummaryDto>>();
            }
            catch (System.Exception)
            {

                throw;
            }

        }
        public async Task<List<PerformanceEvaluationSummaryDto>> GetPerformanceEvaluationSummaryDetail(string[] employeeIdentifier)
        {
            try
            {
                var response = await _client.GetAsync($"api/PerformanceBudget/GetPerformanceEvaluationSummaryDetail?{employeeIdentifier.ConvertArrayToQueryString("Identifiers")}");
                return await response.ReadContentAs<List<PerformanceEvaluationSummaryDto>>();
            }
            catch (System.Exception)
            {

                throw;
            }

        }

        public async Task<bool> UpdateAllEmployeeTargetsIsActive(string employeeIdentifiers)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/PerformanceBudget/UpdateAllEmployeeTargetsIsActive"),
                    Content = new StringContent(employeeIdentifiers, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                return await response.ReadContentAs<bool>();
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
