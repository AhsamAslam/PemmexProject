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
        public static List<Models.OrganizationalBudgetSummary> GetFunctionalBudgetSummaries(IEnumerable<Employee> employees,
            IEnumerable<Compensation> compensations,Business business, List<Models.OrganizationalBudgetSummary> budgetSummaries = null)
        {
            try
            {
                List<Models.OrganizationalBudgetSummary> functionalBudgetSummaries = new List<OrganizationalBudgetSummary>();
                var emp = employees.Where(e => e.BusinessIdentifier == business.BusinessIdentifier).ToList();
                var employee_dictionary = emp.ToDictionary(x => x.EmployeeIdentifier, x => x);
                var comp = compensations.Where(e => e.businessIdentifier == business.BusinessIdentifier).ToList();

                Models.OrganizationalBudgetSummary ITSalarySummary = new Models.OrganizationalBudgetSummary()
                {
                    businessIdentifier = business.BusinessIdentifier,
                    businessName = business.BusinessName,
                    jobFunction = Enum.GetName(JobFunction.IT),
                    totalSalary = 0.0,
                    budgetPercentage = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.IT)).budgetPercentage) : 0,
                    budgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.IT)).budgetValue) : 0.0,
                    mandatoryBudgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.IT)).mandatoryBudgetValue) : 0.0,
                    TotalbudgetValue = budgetSummaries != null ? (budgetSummaries.Where(p => p.businessIdentifier == business.BusinessIdentifier).Select(p => p.budgetValue).Sum()) : 0.0
                };
                Models.OrganizationalBudgetSummary BusinessSalarySummary = new Models.OrganizationalBudgetSummary()
                {
                    businessIdentifier = business.BusinessIdentifier,
                    businessName = business.BusinessName,
                    jobFunction = Enum.GetName(JobFunction.Business),
                    totalSalary = 0.0,
                    budgetPercentage = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.Business)).budgetPercentage) : 0,
                    budgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.Business)).budgetValue) : 0.0,
                    mandatoryBudgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.Business)).mandatoryBudgetValue) : 0.0,
                    TotalbudgetValue = budgetSummaries != null ? (budgetSummaries.Where(p => p.businessIdentifier == business.BusinessIdentifier).Select(p => p.budgetValue).Sum()) : 0.0
                };
                Models.OrganizationalBudgetSummary GeneralSalarySummary = new Models.OrganizationalBudgetSummary()
                {
                    businessIdentifier = business.BusinessIdentifier,
                    businessName = business.BusinessName,
                    jobFunction = Enum.GetName(JobFunction.General),
                    totalSalary = 0.0,
                    budgetPercentage = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.General)).budgetPercentage) : 0,
                    budgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.General)).budgetValue) : 0.0,
                    mandatoryBudgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.General)).mandatoryBudgetValue) : 0.0,
                    TotalbudgetValue = budgetSummaries != null ? (budgetSummaries.Where(p => p.businessIdentifier == business.BusinessIdentifier).Select(p => p.budgetValue).Sum()) : 0.0
                };
                Models.OrganizationalBudgetSummary SalesSalarySummary = new Models.OrganizationalBudgetSummary()
                {
                    businessIdentifier = business.BusinessIdentifier,
                    businessName = business.BusinessName,
                    jobFunction = Enum.GetName(JobFunction.Sales),
                    totalSalary = 0.0,
                    budgetPercentage = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.Sales)).budgetPercentage) : 0,
                    budgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.Sales)).budgetValue) : 0.0,
                    mandatoryBudgetValue = budgetSummaries != null ? (budgetSummaries
                    .FirstOrDefault(b => b.businessIdentifier == business.BusinessIdentifier && b.jobFunction == Enum.GetName(JobFunction.Sales)).mandatoryBudgetValue) : 0.0,
                    TotalbudgetValue = budgetSummaries != null ? (budgetSummaries.Where(p => p.businessIdentifier == business.BusinessIdentifier).Select(p => p.budgetValue).Sum()) : 0.0
                };
                foreach (var c in comp)
                {
                    var e = employee_dictionary[c.EmployeeIdentifier];
                    if (e != null)
                    {
                        if (e.JobFunction == Enum.GetName(JobFunction.Business))
                        {
                            BusinessSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        }
                        else if (e.JobFunction == Enum.GetName(JobFunction.IT))
                        {
                            ITSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        }
                        else if (e.JobFunction == Enum.GetName(JobFunction.General))
                        {
                            GeneralSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        }
                        else if (e.JobFunction == Enum.GetName(JobFunction.Sales))
                        {
                            SalesSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        }
                    }
                }

                functionalBudgetSummaries.Add(ITSalarySummary);
                functionalBudgetSummaries.Add(BusinessSalarySummary);
                functionalBudgetSummaries.Add(GeneralSalarySummary);
                functionalBudgetSummaries.Add(SalesSalarySummary);
                return functionalBudgetSummaries;
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        public static List<JobBasedBonusSummary> 
            GetFunctionalBonusSummaries(IEnumerable<Employee> employees,
            IEnumerable<CompensationSalary> compensations, Business business,List<JobCatalogue> jobCatalogues)
        {
            List<JobBasedBonusSummary> functionalBonusSummaries = 
                new List<JobBasedBonusSummary>();
            var emp = employees.Where(e => e.BusinessIdentifier == business.BusinessIdentifier).ToList();
            var employee_dictionary = emp.ToDictionary(x => x.EmployeeIdentifier, x => x);
            var comp = compensations.Where(e => e.businessIdentifier == business.BusinessIdentifier).ToList();

            JobBasedBonusSummary ITSalarySummary = new JobBasedBonusSummary()
            {
                businessIdentifier = business.BusinessIdentifier,
                businessName = business.BusinessName,
                jobFunction = Enum.GetName(JobFunction.IT),
                totalSalary = 0.0,
                bonusValue = 0.0
            };
            JobBasedBonusSummary BusinessSalarySummary = new JobBasedBonusSummary()
            {
                businessIdentifier = business.BusinessIdentifier,
                businessName = business.BusinessName,
                jobFunction = Enum.GetName(JobFunction.Business),
                totalSalary = 0.0,
                bonusValue = 0.0
            };
            JobBasedBonusSummary GeneralSalarySummary = new JobBasedBonusSummary()
            {
                businessIdentifier = business.BusinessIdentifier,
                businessName = business.BusinessName,
                jobFunction = Enum.GetName(JobFunction.General),
                totalSalary = 0.0,
                bonusValue = 0.0
            };
            JobBasedBonusSummary SalesSalarySummary = new JobBasedBonusSummary()
            {
                businessIdentifier = business.BusinessIdentifier,
                businessName = business.BusinessName,
                jobFunction = Enum.GetName(JobFunction.Sales),
                totalSalary = 0.0,
                bonusValue = 0.0
            };
            foreach (var c in comp)
            {
                if(employee_dictionary.ContainsKey(c.EmployeeIdentifier))
                {
                    var e = employee_dictionary[c.EmployeeIdentifier];
                    var bonus = jobCatalogues.FirstOrDefault(j => j.businessIdentifier == e.BusinessIdentifier
                        && j.grade == e.Grade && j.jobFunction == e.JobFunction);
                    if (e.JobFunction == Enum.GetName(JobFunction.Business))
                    {
                        BusinessSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        BusinessSalarySummary.bonusValue += (((c.BaseSalary + c.AdditionalAgreedPart) / 100) * bonus.acv_bonus_percentage);
                    }
                    else if (e.JobFunction == Enum.GetName(JobFunction.IT))
                    {
                        ITSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        ITSalarySummary.bonusValue += (((c.BaseSalary + c.AdditionalAgreedPart) / 100) * bonus.acv_bonus_percentage);
                    }
                    else if (e.JobFunction == Enum.GetName(JobFunction.General))
                    {
                        GeneralSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        GeneralSalarySummary.bonusValue += (((c.BaseSalary + c.AdditionalAgreedPart) / 100) * bonus.acv_bonus_percentage);
                    }
                    else if (e.JobFunction == Enum.GetName(JobFunction.Sales))
                    {
                        SalesSalarySummary.totalSalary += (c.BaseSalary + c.AdditionalAgreedPart);
                        SalesSalarySummary.bonusValue += (((c.BaseSalary + c.AdditionalAgreedPart) / 100) * bonus.acv_bonus_percentage);
                    }
                }
            }

            functionalBonusSummaries.Add(ITSalarySummary);
            functionalBonusSummaries.Add(BusinessSalarySummary);
            functionalBonusSummaries.Add(GeneralSalarySummary);
            functionalBonusSummaries.Add(SalesSalarySummary);
            return functionalBonusSummaries;
        }
        public static string ConvertArrayToQueryString(this string[] values,string key)
        {
            string response = "";
            for(int v =0;v<values.Length;v++)
            {
                response = response +  ((v== 0)? $"{key}={values[v]}" :  $"&{key}={values[v]}");
            }
            return response;
        }
    }
}
