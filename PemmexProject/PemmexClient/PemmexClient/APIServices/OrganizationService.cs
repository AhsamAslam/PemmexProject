using Newtonsoft.Json;
using PemmexClient.Interfaces;
using PemmexClient.Models;
using PemmexCommonLibs.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PemmexClient.APIServices
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient httpClient;
        public OrganizationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            httpClient = _httpClientFactory.CreateClient("pemmex_http_client");
        }
        public async Task<ResponseMessage> GetOrganization(int id)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/organization/" + id);
            var response = await httpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseMessage>(content);
        }
    }
}
