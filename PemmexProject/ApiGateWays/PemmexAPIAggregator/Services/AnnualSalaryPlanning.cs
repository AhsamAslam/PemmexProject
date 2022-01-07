using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public class AnnualSalaryPlanning : IAnnualSalaryPlanning
    {
        private readonly HttpClient _client;
        public AnnualSalaryPlanning(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Compensation>> BusinessJobTypeBasedSalaries(string BusinessIdentifier)
        {
            var response = await _client.GetAsync($"api/Salary/BusinessSalaries?BusinessIdentifier={BusinessIdentifier}");
            return await response.ReadContentAs<List<Compensation>>();
        }
    }
}
