using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Models;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
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

        public async Task<int> GetEarnedHolidays(string BusinessIdentifier, Guid EmployeeId)
        {
            var response = await _client.GetAsync($"api/Holiday/UserEarnedHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeId={EmployeeId}");
            return await response.ReadContentAs<int>();
        }

        public async Task<int> GetLeftHolidays(string BusinessIdentifier, Guid EmployeeId, string country)
        {
            var response = await _client.GetAsync($"api/Holiday/UserRemainingHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeId={EmployeeId}&country={country}");
            return await response.ReadContentAs<int>();
        }

        public async Task<int> GetPlannedHolidays(string BusinessIdentifier, Guid EmployeeId)
        {
            var response = await _client.GetAsync($"api/Holiday/UserPlannedHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeId={EmployeeId}");
            return await response.ReadContentAs<int>();
        }

        public async Task<IEnumerable<HolidayDto>> GetTeamHolidays(string BusinessIdentifier, string CostcenterIdentifier)
        {
            var response = await _client.GetAsync($"api/Holiday/TeamHolidays?BusinessIdentifier={BusinessIdentifier}&costcenterIdentifier={CostcenterIdentifier}");
            return await response.ReadContentAs<List<HolidayDto>>();
        }

        public async Task<int> GetUsedHolidays(string BusinessIdentifier, Guid EmployeeId,string country)
        {
            var response = await _client.GetAsync($"api/Holiday/UserAvailedHolidays?BusinessIdentifier={BusinessIdentifier}&EmployeeId={EmployeeId}&country={country}");
            return await response.ReadContentAs<int>();
        }
    }
}
