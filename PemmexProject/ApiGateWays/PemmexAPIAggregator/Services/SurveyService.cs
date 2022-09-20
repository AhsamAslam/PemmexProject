using PemmexAPIAggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface ISurveyService
    {
        Task<int> CreateOrganizationSurvey(string survey);
        Task<int> GenerateEmployeeSurvey(string employeeSurvey);

        Task<List<SurveySummaryDto>> GetSurveyAverage(string employeeIdentifiers);
        Task<List<SurveySummaryDto>> GetOrganizationSurveyAverage(string organizationIdentifier);

    }
    public class SurveyService: ISurveyService
    {
        private readonly HttpClient _client;
        public SurveyService(HttpClient client)
        {
            _client = client;
        }

        public async Task<int> CreateOrganizationSurvey(string survey)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/Survey"),
                    Content = new StringContent(survey, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                int data = JsonSerializer.Deserialize<int>(result);
                return data;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }

        public async Task<int> GenerateEmployeeSurvey(string employeeSurvey)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/Survey/GenerateEmployeeSurvey"),
                    Content = new StringContent(employeeSurvey, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                int data = JsonSerializer.Deserialize<int>(result);
                return data;
            }
            catch (System.Exception e)
            {

                throw;
            }
        }

        public async Task<List<SurveySummaryDto>> GetOrganizationSurveyAverage(string organizationIdentifier)
        {
            try
            {
                var response = await _client.GetAsync($"api/Survey/GetOrganizationSurveyAverage?organizationIdentifier={organizationIdentifier}");
                var result = await response.Content.ReadAsStringAsync();
                if (result == "")
                {
                    return null;
                }
                var data = JsonSerializer.Deserialize<List<SurveySummaryDto>>(result);
                //return await response.ReadContentAs<PerfromanceBudgetPlanningDto>();
                return data;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public async Task<List<SurveySummaryDto>> GetSurveyAverage(string employeeIdentifiers)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(_client.BaseAddress + "api/Survey/GetSurveyAverage"),
                    Content = new StringContent(employeeIdentifiers, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
                };
                var response = await _client.SendAsync(request).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<SurveySummaryDto>>(result);
                return data;
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
