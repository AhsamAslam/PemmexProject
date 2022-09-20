using Compensation.API.Controllers;
using Compensation.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexUnitTest.Compensation.API
{
    [TestClass]
    public class BonusControllerTestCase
    {
        private ILogService _logService;
        BonusController bonusController;
        public BonusControllerTestCase()
        {
            bonusController = new BonusController(_logService);
        }

        #region GetUser Test Case
        [TestMethod]
        public void GetUser_Test1()
        {
            string[] employeeIdentifiers = new string[3];

            Task<ActionResult<List<UserBonus>>> result = bonusController.Get(employeeIdentifiers) as Task<ActionResult<List<UserBonus>>>;

            Assert.IsNotNull(result);
        }
        #endregion

        

        #region OrgnaizationBonuses Test Case
        [TestMethod]
        public void OrgnaizationBonuses_Test1()
        {
            string orgnaizationIdentifier = "1234";

            Task<ActionResult<ResponseMessage>> result = bonusController.OrgnaizationBonuses(orgnaizationIdentifier) as Task<ActionResult<ResponseMessage>>;

            Assert.IsNotNull(result);
        }
        #endregion
    }
}
