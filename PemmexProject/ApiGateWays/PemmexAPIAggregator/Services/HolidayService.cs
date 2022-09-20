using Newtonsoft.Json;
using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Helpers;
using PemmexAPIAggregator.Models.Holidays;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface IHolidayService
    {
        Task<IEnumerable<EmployeeHolidayDto>> GetTeamHeirarchyHolidays(string[] employeeIdentifier);
        Task<IEnumerable<EmployeeHolidayDto>> GetSiblingsHolidays(string[] employeeIdentifiers);
        Task<int> GetPlannedHolidays(string BusinessIdentifier, string EmployeeIdentifier);
        Task<int> GetEarnedHolidays(string BusinessIdentifier, string EmployeeIdentifier);
        Task<int> GetUsedHolidays(string BusinessIdentifier, string EmployeeIdentifier, string country);
        Task<int> GetLeftHolidays(string BusinessIdentifier, string EmployeeIdentifier, string country);
        Task<List<EmployeeHolidaysCounter>> EmployeeHolidayCounter(string BusinessIdentifier);
        Task<List<EmployeeHolidayDto>> EmployeeSickLeaves(int month,int days,string[] employeeIdentifiers);
        Task<List<EmployeeHolidayDto>> EmployeeMaternityLeaves(int month, int type, string[] employeeIdentifiers);
        
    }
    public class HolidayService : IHolidayService
    {
        private readonly HttpClient _client;
        public HolidayService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<EmployeeHolidaysCounter>> EmployeeHolidayCounter(string BusinessIdentifier)
        {
            var response = await _client.GetAsync($"api/Holiday/BusinessEmployeesHolidays?BusinessIdentifier={BusinessIdentifier}");
            return await response.ReadContentAs<List<EmployeeHolidaysCounter>>();
        }

        public async Task<int> GetEarnedHolidays(string BusinessIdentifier, string EmployeeIdentifier)
        {
            var response = await _client.GetAsync($"api/Holiday/UserEarnedHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeIdentifier={EmployeeIdentifier}");
            return await response.ReadContentAs<int>();
        }

        public async Task<int> GetLeftHolidays(string BusinessIdentifier, string EmployeeIdentifier, string country)
        {
            var response = await _client.GetAsync($"api/Holiday/UserRemainingHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeIdentifier={EmployeeIdentifier}&country={country}");
            return await response.ReadContentAs<int>();
        }

        public async Task<int> GetPlannedHolidays(string BusinessIdentifier, string EmployeeIdentifier)
        {
            var response = await _client.GetAsync($"api/Holiday/UserPlannedHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeIdentifier={EmployeeIdentifier}");
            return await response.ReadContentAs<int>();
        }

        public async Task<IEnumerable<EmployeeHolidayDto>> GetTeamHeirarchyHolidays(string[] employeeIdentifier)
        {
            var response = await _client.GetAsync($"api/Holiday/TeamHeirarchyHolidays?employeeIdentifier={employeeIdentifier.ConvertArrayToQueryString("employeeIdentifiers")}");
            return await response.ReadContentAs<List<EmployeeHolidayDto>>();
        }

        public async Task<int> GetUsedHolidays(string BusinessIdentifier, string EmployeeIdentifier, string country)
        {
            var response = await _client.GetAsync($"api/Holiday/UserAvailedHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeIdentifier={EmployeeIdentifier}&country={country}");
            return await response.ReadContentAs<int>();
        }
        public async Task<IEnumerable<EmployeeHolidayDto>> GetSiblingsHolidays(string[] employeeIdentifiers)
        {
            var response = await _client.GetAsync($"/api/Holiday/SiblingsHolidays?employeeIdentifiers={employeeIdentifiers.ConvertArrayToQueryString("employeeIdentifiers")}");
            return await response.ReadContentAs<List<EmployeeHolidayDto>>();
        }

        public async Task<List<EmployeeHolidayDto>> EmployeeSickLeaves(int month, int days,string[] employeeIdentifiers)
        {
            dynamic json = new ExpandoObject();
            json.month = month;
            json.days = days;
            json.employeeIdentifiers = employeeIdentifiers;

            var method = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress + "api/HolidayReport/EmployeeWithSickLeaves"),
                Content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };

            var response = await _client.SendAsync(method).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var summary = JsonConvert.
                DeserializeObject<List<EmployeeHolidayDto>>
                (response.Content.ReadAsStringAsync().Result);
            return summary;
        }
        public async Task<List<EmployeeHolidayDto>> EmployeeMaternityLeaves(int month, int type, string[] employeeIdentifiers)
        {
            dynamic json = new ExpandoObject();
            json.month = month;
            json.type = type;
            json.employeeIdentifiers = employeeIdentifiers;

            var method = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_client.BaseAddress + "api/HolidayReport/EmployeeWithMaternityLeaves"),
                Content = new StringContent(JsonConvert.SerializeObject(json), Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */),
            };

            var response = await _client.SendAsync(method).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var summary = JsonConvert.
                DeserializeObject<List<EmployeeHolidayDto>>
                (response.Content.ReadAsStringAsync().Result);
            return summary;
        }
    }
}
