using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class JobCatalogue
    {
        public string grade { get; set; }
        public double minimum_salary { get; set; }
        public double median_salary { get; set; }
        public double maximum_salary { get; set; }
        public double annual_bonus { get; set; }
        public string country { get; set; }
        public string currency { get; set; }
        public string jobFunction { get; set; }
        public double acv_bonus_percentage { get; set; }
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
    }
}
