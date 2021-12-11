using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Infrastructure.Services
{
    public static class DefaultDataService
    {

        public static List<RolesEntity> GetDefaultRolesAndScreen(string organizationIdentifier)
        {
            List<RolesEntity> rolesEntities = new List<RolesEntity>()
            {
                new RolesEntity()
                {

                    roleName = Roles.admin.EnumDescription(),
                    Id = Roles.admin.EnumDescription(),
                    label = Roles.admin.EnumDescription(),
                    color = "hsl(35, 70%, 50%)",
                    OrganizationIdentifier = organizationIdentifier,
                    screenEntities = GetAdminScreens()
                },
                new RolesEntity()
                {
                    roleName = Roles.bu_hr.EnumDescription(),
                    Id = Roles.bu_hr.EnumDescription(),
                    label = Roles.bu_hr.EnumDescription(),
                    color = "hsl(354, 70%, 50%)",
                    OrganizationIdentifier = organizationIdentifier,
                    screenEntities = Get_BU_HR_Screens()
                },
                new RolesEntity()
                {
                    roleName = Roles.group_hr.EnumDescription(),
                    Id = Roles.group_hr.EnumDescription(),
                    label = Roles.group_hr.EnumDescription(),
                    color = "hsl(136, 70%, 50%)",
                    OrganizationIdentifier = organizationIdentifier,
                    screenEntities = GetGroupHR_Screens()
                },
                new RolesEntity()
                {
                    roleName = Roles.manager.EnumDescription(),
                    Id = Roles.manager.EnumDescription(),
                    label = Roles.manager.EnumDescription(),
                    color = "hsl(103, 70%, 50%)",
                    OrganizationIdentifier = organizationIdentifier,
                    screenEntities = GetManagerScreens()
                },
                new RolesEntity()
                {
                    roleName = Roles.user.EnumDescription(),
                    Id = Roles.user.EnumDescription(),
                    color = "hsl(199, 70%, 50%)",
                    label = Roles.user.EnumDescription(),
                    OrganizationIdentifier = organizationIdentifier,
                    screenEntities = GetUserScreens()
                },
            };

            return rolesEntities;
        }
        public static List<ScreenEntity> GetUserScreens()
        {
            List<ScreenEntity> screenEntities = new List<ScreenEntity>()
            {
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_User.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Apply_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Trainings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Attrition.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Candidate_Apply.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Login.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Organization_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Targets.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Request_For_Training.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Succession_Planning.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Talent_Management.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Tasks_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Time_Reporting.EnumDescription()
                }
            };
            return screenEntities;
        }
        public static List<ScreenEntity> GetManagerScreens()
        {
            List<ScreenEntity> screenEntities = new List<ScreenEntity>()
            {
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_Manager.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Apply_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Trainings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Attrition.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Candidate_Apply.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Organization.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Salary.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Login.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Organization_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Targets.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Request_For_Training.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Succession_Planning.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Talent_Management.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Tasks_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Time_Reporting.EnumDescription()
                }
            };
            return screenEntities;
        }
        public static List<ScreenEntity> GetGroupHR_Screens()
        {
            List<ScreenEntity> screenEntities = new List<ScreenEntity>()
            {
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_HR.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Apply_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Trainings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Attrition.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Candidate_Apply.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Organization.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Salary.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Login.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Organization_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Targets.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Request_For_Training.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Settings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Succession_Planning.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Talent_Management.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Tasks_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Time_Reporting.EnumDescription()
                }
            };
            return screenEntities;
        }
        public static List<ScreenEntity> GetAdminScreens()
        {
            List<ScreenEntity> screenEntities = new List<ScreenEntity>()
            {
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_BU_HR.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_HR.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_Manager.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_User.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Apply_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Trainings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Attrition.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Candidate_Apply.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Organization.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Salary.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Login.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Organization_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Targets.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Request_For_Training.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Settings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Succession_Planning.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Talent_Management.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Tasks_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Time_Reporting.EnumDescription()
                }
            };
            return screenEntities;
        }
        public static List<ScreenEntity> Get_BU_HR_Screens()
        {
            List<ScreenEntity> screenEntities = new List<ScreenEntity>()
            {
                new ScreenEntity()
                {
                    screenName = Screens.Analytics_BU_HR.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Apply_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Approve_Trainings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Attrition.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Candidate_Apply.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Organization.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Change_Salary.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Login.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Organization_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Overtime_Holidays.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Approval.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Performance_Targets.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Request_For_Training.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Settings.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Succession_Planning.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Talent_Management.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Tasks_View.EnumDescription()
                },
                new ScreenEntity()
                {
                    screenName = Screens.Time_Reporting.EnumDescription()
                }
            };
            return screenEntities;
        }
    }
}
