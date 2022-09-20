using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.API.Controllers;
using TaskManager.API.Dtos;

namespace PemmexUnitTest.TaskManagerTest.API
{
    [TestClass]
    public class ApprovalSettingsControllerTestCase
    {
        private readonly ILogService _logService;
        ApprovalSettingsController approvalSettingsController;
        public ApprovalSettingsControllerTestCase()
        {
            approvalSettingsController = new ApprovalSettingsController(_logService);
        }

        #region GetAsync Test Case
        [TestMethod]
        public void GetAsync_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = approvalSettingsController.GetAsync() as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion
        #region PostAsync Test Case
        [TestMethod]
        public void PostAsync_Test1()
        {
            List<ApprovalSettingDetailsDto> approvalSettingDetailsDtos = new List<ApprovalSettingDetailsDto>();
            for (int i = 0; i < 3; i++)
            {
                ApprovalSettingDetailsDto approvalSettingDetailsDto = new ApprovalSettingDetailsDto();
                approvalSettingDetailsDto.rankNo = i;
                approvalSettingDetailsDto.EmployeeIdentifier = "22" + i.ToString();
                approvalSettingDetailsDtos.Add(approvalSettingDetailsDto);
            }
            ApprovalSettingDto approvalSettingDto = new ApprovalSettingDto();
            approvalSettingDto.OrganizationIdentifier = "2233";
            approvalSettingDto.approvalSettingDetails = approvalSettingDetailsDtos;
            Task<ActionResult<ResponseMessage>> result = approvalSettingsController.PostAsync(approvalSettingDto) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
