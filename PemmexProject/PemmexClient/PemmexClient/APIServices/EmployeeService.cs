using Newtonsoft.Json;
using PemmexClient.Interfaces;
using PemmexClient.Models;
using PemmexCommonLibs.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PemmexClient.APIServices
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient httpClient;
        public EmployeeService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            httpClient = _httpClientFactory.CreateClient("pemmex_mvc_client");
            //var authheader = new AuthenticationHeaderValue("Bearer", accessToken);
            //httpClient.DefaultRequestHeaders.Authorization = authheader;

        }

        public async Task<ResponseMessage> CreateEmployee(Employee employee)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "/organization/employees");
            var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage>(content);             
        }

        public Task<ResponseMessage> GetAllEmployee(int organizationId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> GetEmployee(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> GetManagers(int organizationId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseMessage> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
