using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace TaskManager.API.Dtos
{
    public class UpdateCompensationTask
    {
        public double AppliedSalary { get; set; }
        public double NewAdditionalAgreedPart { get; set; }
        public double NewTotalMonthlyPay { get; set; }
    }
    public class UpdateHolidayTask
    {
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
    }
    public class UpdateTitleTask
    {
        public string NewTitle { get; set; }
    }
    public class UpdateGradeTask
    {
        public string newGrade { get; set; }

    }
    public class UpdateBonusTask
    {
        public double one_time_bonus { get; set; }
    }
    public class UpdateBudgetPromotionTask
    {
        public List<UpdateBudgetPromotionTaskDetail> BudgetPromotionTaskDetail { get; set; }
    }
    public class UpdateBudgetPromotionTaskDetail
    {
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentTitle { get; set; }
        public string NewTitle { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public string CurrentGrade { get; set; }
        public string NewGrade { get; set; }
        public string JobFunction { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double TotalCurrentSalary { get; set; }
        public double mandatoryPercentage { get; set; }
        public double IncreaseInPercentage { get; set; }
        public double NewBaseSalary { get; set; }
        public double NewTotalSalary { get; set; }
        public string currencyCode { get; set; }
        public double IncreaseInCurrency { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }
}
