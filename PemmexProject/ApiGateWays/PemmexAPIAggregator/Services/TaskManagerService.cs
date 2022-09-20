using PemmexAPIAggregator.Extensions;
using PemmexAPIAggregator.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface ITaskManagerService
    {
        //Task<IEnumerable<HolidayDto>> GetTeamHeirarchyHolidays(string[] employeeIdentifier);
        Task<IEnumerable<BudgetPromotionTask>> BudgetPromotionPendingTasks();
    }
    public class TaskManagerService: ITaskManagerService
    {
        private readonly HttpClient _client;
        public TaskManagerService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<BudgetPromotionTask>> BudgetPromotionPendingTasks()
        {
            try
            {
                var response = await _client.GetAsync($"/api/BudgetPromotionPendingTasks");
                return await response.ReadContentAs<List<BudgetPromotionTask>>();
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}
