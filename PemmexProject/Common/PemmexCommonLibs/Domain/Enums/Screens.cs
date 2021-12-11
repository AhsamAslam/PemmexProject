using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Enums
{
    public enum Screens
    {
        [Description("Login")]
        Login,
        [Description("Settings")]
        Settings,
        [Description("Holidays")]
        Holidays,
        [Description("Apply Holidays")]
        Apply_Holidays,
        [Description("Approve Holidays")]
        Approve_Holidays,
        [Description("Organization View")]
        Organization_View,
        [Description("Analytics User")]
        Analytics_User,
        [Description("Analytics Manager")]
        Analytics_Manager,
        [Description("Analytics HR")]
        Analytics_HR,
        [Description("Analytics BU HR")]
        Analytics_BU_HR,
        [Description("Time Reporting")]
        Time_Reporting,
        [Description("Overtime Approval")]
        Overtime_Approval,
        [Description("Overtime Holidays")]
        Overtime_Holidays,
        [Description("Change Organization")]
        Change_Organization,
        [Description("Change Salary")]
        Change_Salary,
        [Description("Performance Targets")]
        Performance_Targets,
        [Description("Performance Approval")]
        Performance_Approval,
        [Description("Tasks View")]
        Tasks_View,
        [Description("Request For Training")]
        Request_For_Training,
        [Description("Approve Trainings")]
        Approve_Trainings,
        [Description("Talent Management")]
        Talent_Management,
        [Description("Attrition")]
        Attrition,
        [Description("Succession Planning")]
        Succession_Planning,
        [Description("Candidate Apply")]
        Candidate_Apply,
    }
}
