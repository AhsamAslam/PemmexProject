using PemmexAPIAggregator.Models;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface IHolidayService
    {
        Task<IEnumerable<HolidayDto>> GetTeamHolidays(string BusinessIdentifier, string CostcenterIdentifier);
        Task<int> GetPlannedHolidays(string BusinessIdentifier,Guid EmployeeId);
        Task<int> GetEarnedHolidays(string BusinessIdentifier, Guid EmployeeId);
        Task<int> GetUsedHolidays(string BusinessIdentifier, Guid EmployeeId,string country);
        Task<int> GetLeftHolidays(string BusinessIdentifier, Guid EmployeeId, string country);
        Task<List<EmployeeHolidaysCounter>> EmployeeHolidayCounter (string BusinessIdentifier);

    }
}
