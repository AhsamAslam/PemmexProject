using PemmexAPIAggregator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Services
{
    public interface IAnnualSalaryPlanning
    {
        Task<IEnumerable<Compensation>> BusinessJobTypeBasedSalaries(string BusinessIdentifier);
    }
}
