using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.API.Commands.SaveBonusSetting;
using TaskManager.API.Controllers;

namespace PemmexUnitTest.TaskManagerTest.API
{
    [TestClass]
    public class BonusSettingsControllerTestCase
    {
        private readonly ILogService _logService;
        BonusSettingsController bonusSettingsController;
        public BonusSettingsControllerTestCase()
        {
            bonusSettingsController = new BonusSettingsController(_logService);
        }

        #region Get Test Case
        [TestMethod]
        public void Get_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = bonusSettingsController.Get("2233") as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion

        #region Post Test Case
        [TestMethod]
        public void Post_Test1()
        {
            SaveBonusSettingCommand saveBonusSettingCommand = new SaveBonusSettingCommand();
            saveBonusSettingCommand.businessIdentifier = "2233";
            saveBonusSettingCommand.organizationIdentifier = "1122";
            saveBonusSettingCommand.limit_percentage = 23.334;

            Task<ActionResult<ResponseMessage>> result = bonusSettingsController.Post(saveBonusSettingCommand) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion


        #region Delete Test Case
        [TestMethod]
        public void Delete_Test1()
        {

            Task<ActionResult<ResponseMessage>> result = bonusSettingsController.Delete("2233") as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
