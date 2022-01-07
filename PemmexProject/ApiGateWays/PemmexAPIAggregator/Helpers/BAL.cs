using PemmexAPIAggregator.Models;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Helpers
{
    public static class BAL
    {
        public static List<Models.FunctionalBudgetSummary> GetFunctionalBudgetSummaries(IEnumerable<Employee> employees,
            IEnumerable<Compensation> compensations,Business business)
        {
            List<Models.FunctionalBudgetSummary> functionalBudgetSummaries = new List<FunctionalBudgetSummary>();
            var employee_dictionary = employees.ToDictionary(x => x.EmployeeIdentifier, x => x);

            Models.FunctionalBudgetSummary ITSalarySummary = new Models.FunctionalBudgetSummary()
            {
                BusinessIdentifier = business.BusinessIdentifier,
                BusinessName = business.BusinessName,
                JobFunction = Enum.GetName(JobFunction.IT),
                MonthlySalaryValue = 0.0
            };
            Models.FunctionalBudgetSummary BusinessSalarySummary = new Models.FunctionalBudgetSummary()
            {
                BusinessIdentifier = business.BusinessIdentifier,
                BusinessName = business.BusinessName,
                JobFunction = Enum.GetName(JobFunction.Business),
                MonthlySalaryValue = 0.0
            };
            Models.FunctionalBudgetSummary GeneralSalarySummary = new Models.FunctionalBudgetSummary()
            {
                BusinessIdentifier = business.BusinessIdentifier,
                BusinessName = business.BusinessName,
                JobFunction = Enum.GetName(JobFunction.General),
                MonthlySalaryValue = 0.0
            };
            Models.FunctionalBudgetSummary SalesSalarySummary = new Models.FunctionalBudgetSummary()
            {
                BusinessIdentifier = business.BusinessIdentifier,
                BusinessName = business.BusinessName,
                JobFunction = Enum.GetName(JobFunction.Sales),
                MonthlySalaryValue = 0.0
            };
            foreach (var c in compensations)
            {
                var e = employee_dictionary[c.EmployeeIdentifier];
                if(e != null)
                {
                    if(e.JobFunction == Enum.GetName(JobFunction.Business))
                    {
                        BusinessSalarySummary.MonthlySalaryValue += (c.BaseSalary + c.AdditionalAgreedPart);
                    }
                    else if (e.JobFunction == Enum.GetName(JobFunction.IT))
                    {
                        ITSalarySummary.MonthlySalaryValue += (c.BaseSalary + c.AdditionalAgreedPart);
                    }
                    else if (e.JobFunction == Enum.GetName(JobFunction.General))
                    {
                        GeneralSalarySummary.MonthlySalaryValue += (c.BaseSalary + c.AdditionalAgreedPart);
                    }
                    else if (e.JobFunction == Enum.GetName(JobFunction.Sales))
                    {
                        SalesSalarySummary.MonthlySalaryValue += (c.BaseSalary + c.AdditionalAgreedPart);
                    }
                }
            }

            functionalBudgetSummaries.Add(ITSalarySummary);
            functionalBudgetSummaries.Add(BusinessSalarySummary);
            functionalBudgetSummaries.Add(GeneralSalarySummary);
            functionalBudgetSummaries.Add(SalesSalarySummary);
            return functionalBudgetSummaries;
        }

        //public static List<BusinessUnitBudgetSummary> GetBusinessUnit(
        //    IEnumerable<Compensation> compensations,
        //    IEnumerable<Employee> employees)
        //{

        //    var ceo = employees.FirstOrDefault(e => e.Title == "CEO");
        //    List<BusinessUnitBudgetSummary> businessUnitBudgetSummaries 
        //        = new List<BusinessUnitBudgetSummary>();
        //    if(ceo != null)
        //    {
        //        var employee_dictionary = employees.ToDictionary(x => x.EmployeeIdentifier, x => x);
        //        var bunits = employees.Where(m => m.ManagerIdentifier == ceo.EmployeeIdentifier);
        //        foreach(var b in bunits)
        //        {
        //            var b_unit = GetEmployeesUnderOneUnit(b, employees);
        //            BusinessUnitBudgetSummary businessUnitBudgetSummary =
        //                new BusinessUnitBudgetSummary()
        //                {
        //                    BusinessIdentifier = b.BusinessIdentifier,
        //                    title = b.Title
        //                };
                    
        //            foreach (var e in b_unit)
        //            {
        //                businessUnitBudgetSummary.MonthlySalaryValue += 
        //            }
        //        }
        //    }
        //}
        private static List<Employee> GetEmployeesUnderOneUnit(Employee manager,IEnumerable<Employee> employees)
        {
            var result = new List<Employee>();

            var emp = employees.Where(e => e.ManagerIdentifier == manager.EmployeeIdentifier)
                                       .ToList();

            foreach (var employee in emp)
            {
                result.Add(employee);
                result.AddRange(GetEmployeesUnderOneUnit(employee,employees));
            }
            return result;
        }
    }
}
